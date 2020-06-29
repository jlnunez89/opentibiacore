﻿// -----------------------------------------------------------------
// <copyright file="AutoAttackOperation.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Mechanics.Operations
{
    using System;
    using System.Collections.Generic;
    using Fibula.Common.Contracts.Enumerations;
    using Fibula.Common.Utilities;
    using Fibula.Communications.Contracts.Abstractions;
    using Fibula.Communications.Packets.Outgoing;
    using Fibula.Creatures.Contracts.Abstractions;
    using Fibula.Creatures.Contracts.Enumerations;
    using Fibula.Items.Contracts.Constants;
    using Fibula.Items.Contracts.Enumerations;
    using Fibula.Map.Contracts.Extensions;
    using Fibula.Mechanics.Contracts.Abstractions;
    using Fibula.Mechanics.Contracts.Combat.Enumerations;
    using Fibula.Mechanics.Contracts.Constants;
    using Fibula.Mechanics.Contracts.Enumerations;
    using Fibula.Mechanics.Contracts.Structs;
    using Fibula.Mechanics.Operations.Arguments;
    using Fibula.Notifications;
    using Fibula.Notifications.Arguments;

    /// <summary>
    /// Class that represents an auto attack operation.
    /// </summary>
    public class AutoAttackOperation : Operation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoAttackOperation"/> class.
        /// </summary>
        /// <param name="attacker">The combatant that is attacking.</param>
        /// <param name="target">The combatant that is the target.</param>
        /// <param name="exhaustionCost">Optional. The exhaustion cost of this operation.</param>
        public AutoAttackOperation(ICombatant attacker, ICombatant target, TimeSpan exhaustionCost)
            : base(attacker?.Id ?? 0)
        {
            attacker.ThrowIfNull(nameof(attacker));
            target.ThrowIfNull(nameof(target));

            this.Target = target;
            this.Attacker = attacker;

            this.RepeatAfter = TimeSpan.Zero;

            this.ExhaustionCost = exhaustionCost;
            this.TargetIdAtScheduleTime = attacker?.AutoAttackTarget?.Id ?? 0;
        }

        /// <summary>
        /// Gets the combatant that is attacking on this operation.
        /// </summary>
        public ICombatant Attacker { get; }

        /// <summary>
        /// Gets the combatant that is the target on this operation.
        /// </summary>
        public ICombatant Target { get; }

        ///// <summary>
        ///// Gets the combat operation's attack type.
        ///// </summary>
        // public override AttackType AttackType => AttackType.Physical;

        /// <summary>
        /// Gets the type of exhaustion that this operation produces.
        /// </summary>
        public override ExhaustionType ExhaustionType => ExhaustionType.PhysicalCombat;

        /// <summary>
        /// Gets or sets the exhaustion cost time of this operation.
        /// </summary>
        public override TimeSpan ExhaustionCost { get; protected set; }

        /// <summary>
        /// Gets the id of the target at schedule time.
        /// </summary>
        public uint TargetIdAtScheduleTime { get; }

        /// <summary>
        /// Gets the absolute minimum damage that the combat operation can result in.
        /// </summary>
        public int MinimumDamage => 0;

        /// <summary>
        /// Gets the absolute maximum damage that the combat operation can result in.
        /// </summary>
        public int MaximumDamage { get; }

        /// <summary>
        /// Executes the operation's logic.
        /// </summary>
        /// <param name="context">A reference to the operation context.</param>
        protected override void Execute(IOperationContext context)
        {
            // We should try to stop any pending attack operation before carrying this one out.
            if (this.Attacker.PendingAutoAttackOperation != null && this.Attacker.PendingAutoAttackOperation != this)
            {
                // Attempt to cancel it first, and remove the pointer to it.
                if (this.Attacker.PendingAutoAttackOperation.Cancel())
                {
                    this.Attacker.PendingAutoAttackOperation = null;
                }
            }

            var distanceBetweenCombatants = (this.Attacker?.Location ?? this.Target.Location) - this.Target.Location;

            // Pre-checks.
            var nullAttacker = this.Attacker == null;
            var isCorrectTarget = nullAttacker || this.Attacker?.AutoAttackTarget?.Id == this.TargetIdAtScheduleTime;
            var enoughCredits = nullAttacker || this.Attacker?.AutoAttackCredits >= 1;
            var inRange = nullAttacker || (distanceBetweenCombatants.MaxValueIn2D <= this.Attacker.AutoAttackRange && distanceBetweenCombatants.Z == 0);

            var attackPerformed = false;

            try
            {
                if (!isCorrectTarget)
                {
                    // We're not attacking the correct target, so stop right here.
                    return;
                }

                if (!inRange)
                {
                    if (!nullAttacker)
                    {
                        // Set the pending attack operation as this operation.
                        this.Attacker.PendingAutoAttackOperation = this;

                        // And set this operation to repeat after some time (we chose it to be 2x the normalized attack speed), so that it can actually
                        // be expedited (or else it's just processed as usual).
                        this.RepeatAfter = TimeSpan.FromMilliseconds((int)Math.Ceiling(CombatConstants.DefaultCombatRoundTimeInMs / this.Attacker.AttackSpeed) * 2);

                        /*
                        context.EventRulesApi.ClearAllFor(this.GetPartitionKey());

                        // Setup as a movement rule, so that it gets expedited when the combatant is in range from it's target.
                        var conditionsForExpedition = new Func<IEventRuleContext, bool>[]
                        {
                            (context) =>
                            {
                                if (!(context.Arguments is MovementEventRuleArguments movementEventRuleArguments) ||
                                    !(movementEventRuleArguments.ThingMoving is ICombatant attacker) ||
                                    !(attacker.AutoAttackTarget is ICombatant target))
                                {
                                    return false;
                                }

                                return (target.Location - attacker.Location).MaxValueIn2D <= attacker.AutoAttackRange;
                            },
                        };

                        context.EventRulesApi.SetupRule(new ExpediteOperationMovementEventRule(context.Logger, this, conditionsForExpedition, 1), this.GetPartitionKey());
                        */
                    }

                    return;
                }

                if (!enoughCredits)
                {
                    return;
                }

                attackPerformed = this.PerformAttack(context);
            }
            finally
            {
                if (!attackPerformed)
                {
                    // Update the actual cost if the attack wasn't performed.
                    this.ExhaustionCost = TimeSpan.Zero;
                }
            }
        }

        private bool PerformAttack(IOperationContext context)
        {
            var rng = new Random();

            // Calculate the damage to inflict without any protections and reductions,
            // i.e. the amount of damage that the attacker can generate as it is.
            var attackPower = rng.Next(10) + 1;

            var damageToApplyInfo = new DamageInfo(attackPower);
            var damageDoneInfo = this.Target.ApplyDamage(damageToApplyInfo, this.Attacker?.Id ?? 0);

            var packetsToSend = new List<IOutboundPacket>
            {
                new MagicEffectPacket(this.Target.Location, damageDoneInfo.Effect),
            };

            if (damageDoneInfo.Damage != 0)
            {
                TextColor damageTextColor = TextColor.White;

                if (damageDoneInfo.Damage < 0)
                {
                    damageTextColor = TextColor.Blue;
                }

                damageTextColor = damageDoneInfo.Blood switch
                {
                    BloodType.Bones => TextColor.LightGrey,
                    BloodType.Fire => TextColor.Orange,
                    BloodType.Slime => TextColor.Green,
                    _ => TextColor.Red,
                };

                packetsToSend.Add(new AnimatedTextPacket(this.Target.Location, damageTextColor, Math.Abs(damageDoneInfo.Damage).ToString()));
            }

            this.Target.ConsumeCredits(CombatCreditType.Defense, 1);

            if (damageDoneInfo.ApplyBloodToEnvironment)
            {
                context.GameApi.CreateItemAtLocation(
                    this.Target.Location,
                    ItemConstants.BloodSplatterTypeId,
                    new KeyValuePair<ItemAttribute, IConvertible>(ItemAttribute.LiquidType, LiquidType.Blood));
            }

            // Normalize the attacker's defense speed based on the global round time and round that up.
            context.Scheduler.ScheduleEvent(
                context.OperationFactory.Create(new RestoreCombatCreditOperationCreationArguments(this.Target, CombatCreditType.Defense)),
                TimeSpan.FromMilliseconds((int)Math.Floor(CombatConstants.DefaultCombatRoundTimeInMs / this.Target.DefenseSpeed)));

            if (this.Attacker != null)
            {
                // this.Target.RecordDamageTaken(this.Attacker.Id, damageToApply);
                this.Attacker.ConsumeCredits(CombatCreditType.Attack, 1);

                // Normalize the attacker's defense speed based on the global round time and round that up.
                context.Scheduler.ScheduleEvent(
                    context.OperationFactory.Create(new RestoreCombatCreditOperationCreationArguments(this.Attacker, CombatCreditType.Attack)),
                    TimeSpan.FromMilliseconds((int)Math.Floor(CombatConstants.DefaultCombatRoundTimeInMs / this.Attacker.AttackSpeed)));

                if (this.Attacker.Location != this.Target.Location && this.Attacker.Id != this.Target.Id)
                {
                    var directionToTarget = this.Attacker.Location.DirectionTo(this.Target.Location);

                    context.Scheduler.ScheduleEvent(context.OperationFactory.Create(new TurnToDirectionOperationCreationArguments(this.Attacker.Id, this.Attacker, directionToTarget)));
                }
            }

            context.Scheduler.ScheduleEvent(
                new GenericNotification(
                    () => context.CreatureFinder.PlayersThatCanSee(context.Map, this.Target.Location),
                    new GenericNotificationArguments(packetsToSend.ToArray())));

            if (this.Target is IPlayer targetPlayer)
            {
                var squarePacket = new SquarePacket(this.Attacker.Id, SquareColor.Black);

                context.Scheduler.ScheduleEvent(new GenericNotification(() => targetPlayer.YieldSingleItem(), new GenericNotificationArguments(squarePacket)));
            }

            return true;
        }
    }
}
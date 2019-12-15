﻿// -----------------------------------------------------------------
// <copyright file="CanThrowBetweenEventCondition.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.MovementEvents.EventConditions
{
    using System;
    using OpenTibia.Common.Utilities;
    using OpenTibia.Scheduling.Contracts.Abstractions;
    using OpenTibia.Server.Contracts.Abstractions;
    using OpenTibia.Server.Contracts.Structs;

    /// <summary>
    /// Class that represents a condition that evaluates whether a throw from A to B can be performed.
    /// </summary>
    internal class CanThrowBetweenEventCondition : IEventCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanThrowBetweenEventCondition"/> class.
        /// </summary>
        /// <param name="game">A reference to the game instance.</param>
        /// <param name="requestor">The creature requesting the throw.</param>
        /// <param name="determineSourceLocationFunc">A function delegate to determine the source location of the throw.</param>
        /// <param name="determineDestinationLocationFunc">A function delegate to determine the destination location of the throw.</param>
        /// <param name="checkLineOfSight">Whether or not to check the line of sight.</param>
        // TODO: probably set a delegate as well for the actual check.
        public CanThrowBetweenEventCondition(
            IGame game,
            ICreature requestor,
            Func<Location> determineSourceLocationFunc,
            Func<Location> determineDestinationLocationFunc,
            bool checkLineOfSight = true)
        {
            determineSourceLocationFunc.ThrowIfNull(nameof(determineSourceLocationFunc));
            determineDestinationLocationFunc.ThrowIfNull(nameof(determineDestinationLocationFunc));

            this.Game = game;
            this.Requestor = requestor;
            this.GetSourceLocation = determineSourceLocationFunc;
            this.GetDestinationLocation = determineDestinationLocationFunc;
            this.CheckLineOfSight = checkLineOfSight;
        }

        /// <summary>
        /// Gets the reference to the game instance.
        /// </summary>
        public IGame Game { get; }

        /// <summary>
        /// Gets the delegate function to determine the source location of the throw.
        /// </summary>
        public Func<Location> GetSourceLocation { get; }

        /// <summary>
        /// Gets the delegate function to determine the destination location of the throw.
        /// </summary>
        public Func<Location> GetDestinationLocation { get; }

        /// <summary>
        /// Gets a value indicating whether the line of sight should be checked.
        /// </summary>
        public bool CheckLineOfSight { get; }

        /// <summary>
        /// Gets the creature requesting the throw.
        /// </summary>
        public ICreature Requestor { get; }

        /// <inheritdoc/>
        public string ErrorMessage => "You may not throw there.";

        /// <inheritdoc/>
        public bool Evaluate()
        {
            if (this.Requestor == null)
            {
                // Means not a creature generated event... possibly a script.
                return true;
            }

            return true; // this.Game.CanThrowBetween(this.GetSourceLocation(), this.GetDestinationLocation(), this.CheckLineOfSight);
        }
    }
}
﻿// -----------------------------------------------------------------
// <copyright file="BodyToBodyMovementOperationCreationArguments.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.Operations.Arguments
{
    using OpenTibia.Common.Utilities;
    using OpenTibia.Server.Contracts.Abstractions;
    using OpenTibia.Server.Contracts.Enumerations;

    /// <summary>
    /// Class that represents creation arguments for a <see cref="BodyToBodyMovementOperation"/>.
    /// </summary>
    public class BodyToBodyMovementOperationCreationArguments : IOperationCreationArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyToBodyMovementOperationCreationArguments"/> class.
        /// </summary>
        /// <param name="requestorId">The id of the requestor.</param>
        /// <param name="thingMoving">The thing moving.</param>
        /// <param name="targetCreature">The creature within which the movement is happening.</param>
        /// <param name="fromSlot">The slot from which the movement is happening.</param>
        /// <param name="toSlot">The slot to which the movement is happening.</param>
        /// <param name="amount">The amount being moved.</param>
        public BodyToBodyMovementOperationCreationArguments(uint requestorId, IThing thingMoving, ICreature targetCreature, Slot fromSlot, Slot toSlot, byte amount = 1)
        {
            thingMoving.ThrowIfNull(nameof(thingMoving));
            targetCreature.ThrowIfNull(nameof(targetCreature));

            this.RequestorId = requestorId;
            this.ThingMoving = thingMoving;
            this.TargetCreature = targetCreature;
            this.FromSlot = fromSlot;
            this.ToSlot = toSlot;
            this.Amount = amount;
        }

        /// <summary>
        /// Gets the id of the requestor of the operation.
        /// </summary>
        public uint RequestorId { get; }

        /// <summary>
        /// Gets the thing being moved.
        /// </summary>
        public IThing ThingMoving { get; }

        /// <summary>
        /// Gets the creature within which the movement is happening.
        /// </summary>
        public ICreature TargetCreature { get; }

        /// <summary>
        /// Gets the slot from which the movement is happening.
        /// </summary>
        public Slot FromSlot { get; }

        /// <summary>
        /// Gets the slot to which the movement is happening.
        /// </summary>
        public Slot ToSlot { get; }

        /// <summary>
        /// Gets the amount being moved.
        /// </summary>
        public byte Amount { get; }
    }
}

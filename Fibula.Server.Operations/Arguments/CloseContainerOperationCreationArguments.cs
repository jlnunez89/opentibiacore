﻿// -----------------------------------------------------------------
// <copyright file="CloseContainerOperationCreationArguments.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Server.Operations.Arguments
{
    using Fibula.Common.Utilities;
    using Fibula.Server.Contracts.Abstractions;
    using Fibula.Server.Operations.Contracts.Abstractions;

    /// <summary>
    /// Class that represents creation arguments for a <see cref="CloseContainerOperation"/>.
    /// </summary>
    public class CloseContainerOperationCreationArguments : IOperationCreationArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseContainerOperationCreationArguments"/> class.
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="player"></param>
        /// <param name="containerItem"></param>
        /// <param name="containerId"></param>
        public CloseContainerOperationCreationArguments(uint requestorId, IPlayer player, IContainerItem containerItem, byte containerId)
        {
            player.ThrowIfNull(nameof(player));
            containerItem.ThrowIfNull(nameof(containerItem));

            this.RequestorId = requestorId;
            this.Player = player;
            this.Container = containerItem;
            this.ContainerId = containerId;
        }

        /// <summary>
        /// Gets the id of the requestor of the operation.
        /// </summary>
        public uint RequestorId { get; }

        public IPlayer Player { get; }

        public IContainerItem Container { get; }

        public byte ContainerId { get; }
    }
}
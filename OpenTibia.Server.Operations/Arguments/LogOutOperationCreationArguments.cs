﻿// -----------------------------------------------------------------
// <copyright file="LogOutOperationCreationArguments.cs" company="2Dudes">
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
    using OpenTibia.Server.Operations.Environment;

    /// <summary>
    /// Class that represents creation arguments for a <see cref="LogOutOperation"/>.
    /// </summary>
    public class LogOutOperationCreationArguments : IOperationCreationArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogOutOperationCreationArguments"/> class.
        /// </summary>
        /// <param name="player"></param>
        public LogOutOperationCreationArguments(IPlayer player)
        {
            player.ThrowIfNull(nameof(player));

            this.Player = player;
        }

        public IPlayer Player { get; }

        /// <summary>
        /// Gets the id of the requestor of the operation.
        /// </summary>
        public uint RequestorId => this.Player.Id;
    }
}

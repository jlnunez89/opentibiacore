﻿// -----------------------------------------------------------------
// <copyright file="SpeechOperationCreationArguments.cs" company="2Dudes">
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
    using OpenTibia.Communications.Packets.Contracts.Abstractions;
    using OpenTibia.Server.Contracts.Abstractions;

    /// <summary>
    /// Class that represents creation arguments for a <see cref="SpeechOperation"/>.
    /// </summary>
    public class SpeechOperationCreationArguments : IOperationCreationArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechOperationCreationArguments"/> class.
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="speechInfo"></param>
        public SpeechOperationCreationArguments(uint requestorId, ISpeechInfo speechInfo)
        {
            this.RequestorId = requestorId;
            this.SpeechInfo = speechInfo;
        }

        /// <summary>
        /// Gets the id of the requestor of the operation.
        /// </summary>
        public uint RequestorId { get; }

        public ISpeechInfo SpeechInfo { get; }
    }
}

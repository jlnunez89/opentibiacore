﻿// -----------------------------------------------------------------
// <copyright file="RuleViolationPacket.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Communications.Packets.Incoming
{
    using System.Net;
    using Fibula.Communications.Contracts.Abstractions;
    using OpenTibia.Communications.Packets.Contracts.Abstractions;

    public class RuleViolationPacket : IIncomingPacket, IRuleViolationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleViolationPacket"/> class.
        /// </summary>
        /// <param name="gamemasterId"></param>
        /// <param name="characterName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="reason"></param>
        /// <param name="comment"></param>
        public RuleViolationPacket(uint gamemasterId, string characterName, string ipAddress, string reason, string comment)
        {
            this.GamemasterId = gamemasterId;
            this.CharacterName = characterName;
            this.IpAddress = IPAddress.Parse(ipAddress);
            this.Reason = reason;
            this.Comment = comment;
        }

        public uint GamemasterId { get; }

        public string CharacterName { get; }

        public IPAddress IpAddress { get; }

        public string Reason { get; }

        public string Comment { get; }
    }
}

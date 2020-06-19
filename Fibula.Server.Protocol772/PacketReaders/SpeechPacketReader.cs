﻿// -----------------------------------------------------------------
// <copyright file="SpeechPacketReader.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Server.Protocol772.PacketReaders
{
    using Fibula.Common.Utilities;
    using Fibula.Communications;
    using Fibula.Communications.Contracts.Abstractions;
    using Fibula.Communications.Packets.Incoming;
    using Fibula.Server.Contracts.Enumerations;
    using Serilog;

    /// <summary>
    /// Class that represents a speech packet reader for the game server.
    /// </summary>
    public sealed class SpeechPacketReader : BasePacketReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechPacketReader"/> class.
        /// </summary>
        /// <param name="logger">A reference to the logger in use.</param>
        public SpeechPacketReader(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Reads a packet from the given <see cref="INetworkMessage"/>.
        /// </summary>
        /// <param name="message">The message to read from.</param>
        /// <returns>The packet read from the message.</returns>
        public override IIncomingPacket ReadFromMessage(INetworkMessage message)
        {
            message.ThrowIfNull(nameof(message));

            SpeechType type = (SpeechType)message.GetByte();

            switch (type)
            {
                case SpeechType.Private:
                // case SpeechType.PrivateRed:
                case SpeechType.RuleViolationAnswer:
                    return new SpeechPacket(type, ChatChannelType.Private, receiver: message.GetString(), content: message.GetString());
                case SpeechType.ChannelYellow:
                    // case SpeechType.ChannelRed:
                    // case SpeechType.ChannelRedAnonymous:
                    // case SpeechType.ChannelWhite:
                    return new SpeechPacket(type, channelId: (ChatChannelType)message.GetUInt16(), content: message.GetString());
                default:
                    return new SpeechPacket(type, channelId: ChatChannelType.None, content: message.GetString());
            }
        }
    }
}
﻿// -----------------------------------------------------------------
// <copyright file="ContainerAddItemPacket.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Communications.Packets.Outgoing
{
    using Fibula.Communications.Contracts.Abstractions;
    using Fibula.Communications.Contracts.Enumerations;
    using Fibula.Items.Contracts.Abstractions;

    public class ContainerAddItemPacket : IOutboundPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerAddItemPacket"/> class.
        /// </summary>
        /// <param name="containerId"></param>
        /// <param name="item"></param>
        public ContainerAddItemPacket(byte containerId, IItem item)
        {
            this.ContainerId = containerId;
            this.Item = item;
        }

        /// <summary>
        /// Gets the type of this packet.
        /// </summary>
        public byte PacketType => (byte)OutgoingGamePacketType.ContainerAddItem;

        public byte ContainerId { get; }

        public IItem Item { get; }
    }
}
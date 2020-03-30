﻿// -----------------------------------------------------------------
// <copyright file="ItemEventType.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.Contracts.Enumerations
{
    using OpenTibia.Server.Contracts.Abstractions;

    /// <summary>
    /// Enumerates the different types of <see cref="IEventRule"/>s.
    /// </summary>
    public enum EventRuleType : byte
    {
        /// <summary>
        /// An item is used.
        /// </summary>
        Use,

        /// <summary>
        /// An item is used on something else.
        /// </summary>
        MultiUse,

        /// <summary>
        /// Something moves between cyclinders.
        /// </summary>
        Movement,

        /// <summary>
        /// A thing is moved into the same tile cyclinder as another thing.
        /// </summary>
        Collision,

        /// <summary>
        /// Something is moved from a particular tile cyclinder.
        /// </summary>
        Separation,
    }
}

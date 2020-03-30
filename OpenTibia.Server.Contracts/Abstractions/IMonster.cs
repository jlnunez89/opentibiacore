﻿// -----------------------------------------------------------------
// <copyright file="IMonster.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.Contracts.Abstractions
{
    /// <summary>
    /// Interface for all monsters.
    /// </summary>
    public interface IMonster : ICombatant
    {
        /// <summary>
        /// The default attack range for melee figthing in monsters.
        /// </summary>
        const int DefaultMeleeFightingAttackRange = 1;

        /// <summary>
        /// The default attack range for distance figthing in monsters.
        /// </summary>
        const int DefaultDistanceFightingAttackRange = 5;

        /// <summary>
        /// Gets the type of this monster.
        /// </summary>
        IMonsterType Type { get; }

        /// <summary>
        /// Gets the experience yielded when this monster dies.
        /// </summary>
        uint Experience { get; }
    }
}

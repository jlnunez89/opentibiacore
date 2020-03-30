﻿// -----------------------------------------------------------------
// <copyright file="ICombatApi.cs" company="2Dudes">
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
    using OpenTibia.Server.Contracts.Enumerations;

    /// <summary>
    /// Interface for the combat api.
    /// </summary>
    public interface ICombatApi
    {
        void OnCombatantCombatStarted(ICombatant combatant);

        void OnCombatantCombatEnded(ICombatant combatant);

        void OnCombatCreditsConsumed(ICombatant combatant, CombatCreditType creditType, byte amount);

        void OnCombatantTargetChanged(ICombatant combatant, ICombatant oldTarget);

        void OnCombatantChaseModeChanged(ICombatant combatant, ChaseMode oldMode);
    }
}
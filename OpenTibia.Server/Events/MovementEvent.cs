﻿// <copyright file="MovementEvent.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace OpenTibia.Server.Events
{
    using System.Collections.Generic;
    using OpenTibia.Data.Contracts;

    internal class MovementEvent : BaseEvent
    {
        public MovementEvent(IList<string> conditionSet, IList<string> actionSet)
            : base(conditionSet, actionSet)
        {
        }

        public override EventType Type => EventType.Movement;
    }
}
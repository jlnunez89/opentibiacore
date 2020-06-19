﻿// -----------------------------------------------------------------
// <copyright file="MapConstants.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Map.Contracts.Constants
{
    using Fibula.Server.Contracts.Abstractions;

    /// <summary>
    /// Static class that contains constants regarding the map.
    /// </summary>
    public static class MapConstants
    {
        /// <summary>
        /// The maximum number of <see cref="IThing"/>s to describe per tile.
        /// </summary>
        public const int MaximumNumberOfThingsToDescribePerTile = 9;

        /// <summary>
        /// The default window size in the X coordinate.
        /// </summary>
        public const byte DefaultWindowSizeX = 18;

        /// <summary>
        /// The default window size in the Y coordinate.
        /// </summary>
        public const byte DefaultWindowSizeY = 14;
    }
}
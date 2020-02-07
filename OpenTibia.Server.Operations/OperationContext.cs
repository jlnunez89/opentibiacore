﻿// -----------------------------------------------------------------
// <copyright file="OperationContext.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.Operations
{
    using OpenTibia.Common.Utilities;
    using OpenTibia.Communications.Contracts.Abstractions;
    using OpenTibia.Scheduling.Contracts.Abstractions;
    using OpenTibia.Server.Contracts.Abstractions;

    /// <summary>
    /// Class that represents a context for operations.
    /// </summary>
    public class OperationContext : IOperationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationContext"/> class.
        /// </summary>
        /// <param name="mapDescriptor"></param>
        /// <param name="tileAccessor"></param>
        /// <param name="connectionFinder"></param>
        /// <param name="creatureFinder"></param>
        /// <param name="pathFinder"></param>
        /// <param name="itemFactory"></param>
        /// <param name="creatureFactory"></param>
        /// <param name="scheduler"></param>
        public OperationContext(
            IMapDescriptor mapDescriptor,
            ITileAccessor tileAccessor,
            IConnectionFinder connectionFinder,
            ICreatureFinder creatureFinder,
            IPathFinder pathFinder,
            IItemFactory itemFactory,
            ICreatureFactory creatureFactory,
            IScheduler scheduler)
        {
            mapDescriptor.ThrowIfNull(nameof(mapDescriptor));
            tileAccessor.ThrowIfNull(nameof(tileAccessor));
            connectionFinder.ThrowIfNull(nameof(connectionFinder));
            creatureFinder.ThrowIfNull(nameof(creatureFinder));
            pathFinder.ThrowIfNull(nameof(pathFinder));
            itemFactory.ThrowIfNull(nameof(itemFactory));
            creatureFactory.ThrowIfNull(nameof(creatureFactory));
            scheduler.ThrowIfNull(nameof(scheduler));

            this.MapDescriptor = mapDescriptor;
            this.TileAccessor = tileAccessor;
            this.ConnectionFinder = connectionFinder;
            this.CreatureFinder = creatureFinder;
            this.PathFinder = pathFinder;
            this.ItemFactory = itemFactory;
            this.CreatureFactory = creatureFactory;
            this.Scheduler = scheduler;
        }

        /// <summary>
        /// Gets a reference to the map descriptor in use.
        /// </summary>
        public IMapDescriptor MapDescriptor { get; }

        /// <summary>
        /// Gets the reference to the tile accessor in use.
        /// </summary>
        public ITileAccessor TileAccessor { get; }

        /// <summary>
        /// Gets the reference to the connection finder in use.
        /// </summary>
        public IConnectionFinder ConnectionFinder { get; }

        /// <summary>
        /// Gets the reference to the creature finder in use.
        /// </summary>
        public ICreatureFinder CreatureFinder { get; }

        /// <summary>
        /// Gets the reference to the path finder helper in use.
        /// </summary>
        public IPathFinder PathFinder { get; }

        /// <summary>
        /// Gets a reference to the item factory in use.
        /// </summary>
        public IItemFactory ItemFactory { get; }

        /// <summary>
        /// Gets a reference to the creature factory in use.
        /// </summary>
        public ICreatureFactory CreatureFactory { get; }

        /// <summary>
        /// Gets a reference to the scheduler in use.
        /// </summary>
        public IScheduler Scheduler { get; }
    }
}

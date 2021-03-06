﻿// -----------------------------------------------------------------
// <copyright file="ContainerExtensions.cs" company="2Dudes">
// Copyright (c) | Jose L. Nunez de Caceres et al.
// https://linkedin.com/in/nunezdecaceres
//
// All Rights Reserved.
//
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Mechanics.Contracts.Extensions
{
    using System.Collections.Generic;
    using Fibula.Common.Contracts.Abstractions;
    using Fibula.Common.Utilities;
    using Fibula.Map.Contracts.Abstractions;

    /// <summary>
    /// Helper class that provides extensions for the <see cref="IContainedThing"/> and <see cref="IThingContainer"/> implementations.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Gets this thing's parent container hierarchy.
        /// </summary>
        /// <param name="containedThing">The contained thing to get the hierarchy for.</param>
        /// <param name="includeTiles">Optional. A value indicating whether to include tiles in the hierarchy. Defaults to true.</param>
        /// <returns>The ordered collection of <see cref="IThingContainer"/>s in this thing's container hierarchy.</returns>
        public static IEnumerable<IThingContainer> GetParentContainerHierarchy(this IContainedThing containedThing, bool includeTiles = true)
        {
            containedThing.ThrowIfNull(nameof(containedThing));

            IThingContainer currentContainer = containedThing is IThingContainer containedThingContainer ? containedThingContainer : containedThing.ParentContainer;

            return currentContainer.GetParentContainerHierarchy(includeTiles);
        }

        /// <summary>
        /// Gets this thing's parent container hierarchy.
        /// </summary>
        /// <param name="thingContainer">The thing container to get the hierarchy for.</param>
        /// <param name="includeTiles">Optional. A value indicating whether to include tiles in the hierarchy. Defaults to true.</param>
        /// <returns>The ordered collection of <see cref="IThingContainer"/>s in this thing's container hierarchy.</returns>
        public static IEnumerable<IThingContainer> GetParentContainerHierarchy(this IThingContainer thingContainer, bool includeTiles = true)
        {
            thingContainer.ThrowIfNull(nameof(thingContainer));

            IThingContainer currentContainer = thingContainer;

            while (currentContainer != null)
            {
                yield return currentContainer;

                if (currentContainer is IContainedThing currentContainedContainer)
                {
                    currentContainer = includeTiles || !(currentContainedContainer.ParentContainer is ITile) ? currentContainedContainer.ParentContainer : null;
                }
            }
        }
    }
}

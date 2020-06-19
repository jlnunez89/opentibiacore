﻿// -----------------------------------------------------------------
// <copyright file="ItemCreationArguments.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// jlnunez89@gmail.com
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace Fibula.Items
{
    using Fibula.Server.Contracts.Abstractions;

    /// <summary>
    /// Class that implements <see cref="IThingCreationArguments"/>, for the creation of an item.
    /// </summary>
    public class ItemCreationArguments : IThingCreationArguments
    {
        /// <summary>
        /// Gets or sets the id of type for the item to create.
        /// </summary>
        public ushort TypeId { get; set; }
    }
}
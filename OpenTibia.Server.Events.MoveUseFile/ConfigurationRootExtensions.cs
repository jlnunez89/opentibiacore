﻿// -----------------------------------------------------------------
// <copyright file="ConfigurationRootExtensions.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.Events.MoveUseFile
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using OpenTibia.Common.Utilities;
    using OpenTibia.Server.Contracts.Abstractions;

    /// <summary>
    /// Static class that adds convenient methods to add the concrete implementations contained in this library.
    /// </summary>
    public static class ConfigurationRootExtensions
    {
        /// <summary>
        /// Adds all implementations related to Objects item type files contained in this library to the services collection.
        /// Additionally, registers the options related to the concrete implementations added, such as:
        ///     <see cref="MoveUseEventRulesLoaderOptions"/>.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration reference.</param>
        public static void AddMoveUseEventRulesLoader(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.ThrowIfNull(nameof(configuration));

            // configure options
            services.Configure<MoveUseEventRulesLoaderOptions>(configuration.GetSection(nameof(MoveUseEventRulesLoaderOptions)));

            services.AddSingleton<IEventRulesFactory, MoveUseEventRulesFactory>();
            services.AddSingleton<IEventRulesLoader, MoveUseEventRulesLoader>();
        }
    }
}

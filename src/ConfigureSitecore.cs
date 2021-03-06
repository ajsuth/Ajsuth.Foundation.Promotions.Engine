﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2020
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Ajsuth.Foundation.Promotions.Engine
{
    /// <summary>The configure sitecore class.</summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>The configure services.</summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            services.Sitecore().Pipelines(configure => configure

                .ConfigurePipeline<IGetEntityViewPipeline>(pipeline => pipeline
                    .Add<Pipelines.Blocks.GetPromotionDetailsViewBlock>().After<GetPromotionDetailsViewBlock>()
                )

            );
        }
    }
}

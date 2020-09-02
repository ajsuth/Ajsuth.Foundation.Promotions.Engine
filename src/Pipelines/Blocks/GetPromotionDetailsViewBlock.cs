// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetPromotionDetailsViewBlock.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2020
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Ajsuth.Foundation.Promotions.Engine.Pipelines.Blocks
{
    /// <summary>Defines the synchronous executing get promotion details pipeline block</summary>
    /// <seealso cref="SyncPipelineBlock{TInput, TOutput, TContext}" />
    [PipelineDisplayName(PromotionsConstants.Pipelines.Blocks.GetPromotionDetailsView)]
    public class GetPromotionDetailsViewBlock : SyncPipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        /// <summary>Executes the pipeline block's code logic.</summary>
        /// <param name="entityView">The <see cref="EntityView"/>.</param>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="EntityView"/>.</returns>
        public override EntityView Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{Name}: The argument cannot be null");

            var request = context.CommerceContext.GetObject<EntityViewArgument>();
            var enablementPolicy = context.GetPolicy<Policies.PromotionsFeatureEnablementPolicy>();
            if (!enablementPolicy.RenderDateTimes
                || string.IsNullOrEmpty(request?.ViewName)
                || (!request.ViewName.Equals(context.GetPolicy<KnownPromotionsViewsPolicy>().Details, StringComparison.OrdinalIgnoreCase)
                    && !request.ViewName.Equals(context.GetPolicy<KnownPromotionsViewsPolicy>().Master, StringComparison.OrdinalIgnoreCase)))
            {
                return entityView;
            }

            if (!string.IsNullOrEmpty(request.ForAction))
            {
                return entityView;
            }

            if (!(request.Entity is Promotion))
            {
                return entityView;
            }

            // Is View Action
            var detailsView = entityView.ChildViews.FirstOrDefault(view => view.Name.Equals(context.GetPolicy<KnownPromotionsViewsPolicy>().Details, StringComparison.OrdinalIgnoreCase)) as EntityView;
            if (detailsView == null)
            {
                return entityView;
            }

            var promotion = (Promotion)request.Entity;

            detailsView.Properties.Insert(1, new ViewProperty
            {
                Name = "DateCreated",
                RawValue = promotion?.DateCreated ?? DateTimeOffset.UtcNow,
                UiType = "FullDateTime"
            });

            var validFromProperty = detailsView.GetProperty("ValidFrom");
            if (validFromProperty != null)
            {
                validFromProperty.UiType = "FullDateTime";
            }

            var validToProperty = detailsView.GetProperty("ValidTo");
            if (validToProperty != null)
            {
                validToProperty.UiType = "FullDateTime";
            }

            return entityView;
        }
    }
}

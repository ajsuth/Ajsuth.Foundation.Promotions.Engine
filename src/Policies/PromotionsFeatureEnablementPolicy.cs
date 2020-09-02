// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromotionsFeatureEnablementPolicy.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2020
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Sitecore.Commerce.Core;

namespace Ajsuth.Foundation.Promotions.Engine.Policies
{
    /// <summary>Defines the promotions feature enablement policy</summary>
    /// <seealso cref="Policy" />
    public class PromotionsFeatureEnablementPolicy : Policy
    {
        public bool RenderDateTimes { get; set; }
    }
}

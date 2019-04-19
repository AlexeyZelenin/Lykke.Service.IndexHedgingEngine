﻿namespace Lykke.Service.IndexHedgingEngine.Domain.Settings
{
    /// <summary>
    /// Represents cross asset pair setting.
    /// </summary>
    public class CrossAssetPairSettings
    {
        /// <summary>
        /// The identifier of the the original asset pair.
        /// </summary>
        public string AssetPairId { get; set; }

        /// <summary>
        /// The identifier of the the cross asset pair.
        /// </summary>
        public string CrossAssetPairId { get; set; }

        /// <summary>
        /// If cross asset pair must be inverted
        /// </summary>
        public bool IsInverted { get; set; }
    }
}
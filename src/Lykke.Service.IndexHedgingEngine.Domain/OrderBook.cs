using System;
using System.Collections.Generic;

namespace Lykke.Service.IndexHedgingEngine.Domain
{
    /// <summary>
    /// Represents an order book.
    /// </summary>
    public class OrderBook
    {
        /// <summary>
        /// The asset pair.
        /// </summary>
        public string AssetPairId { get; set; }

        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The collection of limit orders.
        /// </summary>
        public IReadOnlyCollection<LimitOrder> LimitOrders { get; set; }
    }
}

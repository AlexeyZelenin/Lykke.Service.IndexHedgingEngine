using System;

namespace Lykke.Service.IndexHedgingEngine.Domain
{
    /// <summary>
    /// Represents a limit order details.
    /// </summary>
    public class LimitOrder
    {
        /// <summary>
        /// The identifier of the limit order.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The identifier of the client wallet.
        /// </summary>
        public string WalletId { get; set; }
        
        /// <summary>
        /// The limit order price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The limit order volume.
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The limit order type.
        /// </summary>
        public LimitOrderType Type { get; set; }

        /// <summary>
        /// The code of the error which occurred while processing on ME.
        /// </summary>
        public LimitOrderError Error { get; set; }

        /// <summary>
        /// The error details.
        /// </summary>
        public string ErrorMessage { get; set; }

        public void UpdateVolume(decimal volume)
        {
            Volume = volume;
        }

        public static LimitOrder CreateSell(string walletId, decimal price, decimal volume)
            => Create(walletId, price, volume, LimitOrderType.Sell);

        public static LimitOrder CreateBuy(string walletId, decimal price, decimal volume)
            => Create(walletId, price, volume, LimitOrderType.Buy);

        private static LimitOrder Create(string walletId, decimal price, decimal volume, LimitOrderType type)
            => new LimitOrder
            {
                Id = Guid.NewGuid().ToString("D"),
                WalletId = walletId,
                Price = price,
                Volume = volume,
                Type = type
            };
    }
}

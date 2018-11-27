using System;

namespace Lykke.Service.IndexHedgingEngine.Domain
{
    /// <summary>
    /// Represents settings of service timers. 
    /// </summary>
    public class TimersSettings
    {
        /// <summary>
        /// The timer interval of lykke exchange balances.
        /// </summary>
        public TimeSpan LykkeBalances { get; set; }
        
        /// <summary>
        /// The timer interval of external exchange balances.
        /// </summary>
        public TimeSpan ExternalBalances { get; set; }
    }
}

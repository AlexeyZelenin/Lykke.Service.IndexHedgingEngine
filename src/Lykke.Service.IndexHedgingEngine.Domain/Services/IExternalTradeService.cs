using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.IndexHedgingEngine.Domain.Services
{
    public interface IExternalTradeService
    {
        Task<IReadOnlyCollection<ExternalTrade>> GetAsync(DateTime startDate, DateTime endDate, string exchange,
            string assetPairId, int limit);

        Task RegisterAsync(ExternalTrade externalTrade);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.IndexHedgingEngine.Domain.Trades;

namespace Lykke.Service.IndexHedgingEngine.Domain.Repositories
{
    public interface IVirtualTradeRepository
    {
        Task<IReadOnlyCollection<VirtualTrade>> GetAsync(DateTime startDate, DateTime endDate, string assetPairId,
            int limit);

        Task InsertAsync(VirtualTrade virtualTrade);
    }
}

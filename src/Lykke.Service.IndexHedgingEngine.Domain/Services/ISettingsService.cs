using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.IndexHedgingEngine.Domain.Services
{
    public interface ISettingsService
    {
        string GetInstanceName();

        string GetWalletId();

        string GetTransitWalletId();

        IReadOnlyCollection<ExchangeSettings> GetExchanges();
    }
}

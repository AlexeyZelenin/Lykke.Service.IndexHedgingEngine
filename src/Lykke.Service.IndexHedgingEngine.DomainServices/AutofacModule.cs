using System.Collections.Generic;
using Autofac;
using JetBrains.Annotations;
using Lykke.Service.IndexHedgingEngine.Domain;
using Lykke.Service.IndexHedgingEngine.Domain.Handlers;
using Lykke.Service.IndexHedgingEngine.Domain.Infrastructure;
using Lykke.Service.IndexHedgingEngine.Domain.Services;
using Lykke.Service.IndexHedgingEngine.DomainServices.Balances;
using Lykke.Service.IndexHedgingEngine.DomainServices.ExchangeAdapters;
using Lykke.Service.IndexHedgingEngine.DomainServices.Exchanges;
using Lykke.Service.IndexHedgingEngine.DomainServices.Hedging;
using Lykke.Service.IndexHedgingEngine.DomainServices.Indices;
using Lykke.Service.IndexHedgingEngine.DomainServices.OrderBooks;
using Lykke.Service.IndexHedgingEngine.DomainServices.Positions;
using Lykke.Service.IndexHedgingEngine.DomainServices.Reports;
using Lykke.Service.IndexHedgingEngine.DomainServices.Settings;
using Lykke.Service.IndexHedgingEngine.DomainServices.Timers;
using Lykke.Service.IndexHedgingEngine.DomainServices.Trades;

namespace Lykke.Service.IndexHedgingEngine.DomainServices
{
    [UsedImplicitly]
    public class AutofacModule : Module
    {
        private readonly string _instanceName;
        private readonly string _walletId;
        private readonly IReadOnlyCollection<ExchangeSettings> _exchanges;

        public AutofacModule(string instanceName, string walletId, IReadOnlyCollection<ExchangeSettings> exchanges)
        {
            _instanceName = instanceName;
            _walletId = walletId;
            _exchanges = exchanges;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BalanceOperationService>()
                .As<IBalanceOperationService>()
                .SingleInstance();

            builder.RegisterType<BalanceService>()
                .As<IBalanceService>()
                .SingleInstance();

            builder.RegisterType<FundingService>()
                .As<IFundingService>()
                .SingleInstance();

            builder.RegisterType<TokenService>()
                .As<ITokenService>()
                .SingleInstance();

            builder.RegisterType<LykkeExchangeService>()
                .As<ILykkeExchangeService>()
                .SingleInstance();

            builder.RegisterType<VirtualExchangeAdapter>()
                .As<IExchangeAdapter>()
                .SingleInstance();

            builder.RegisterType<LykkeExchangeAdapter>()
                .As<IExchangeAdapter>()
                .As<IInternalTradeHandler>()
                .SingleInstance();

            builder.RegisterType<AssetHedgeSettingsService>()
                .As<IAssetHedgeSettingsService>()
                .SingleInstance();

            builder.RegisterType<HedgeLimitOrderService>()
                .As<IHedgeLimitOrderService>()
                .SingleInstance();

            builder.RegisterType<InvestmentService>()
                .As<IInvestmentService>()
                .SingleInstance();

            builder.RegisterType<IndexService>()
                .As<IIndexService>()
                .SingleInstance();

            builder.RegisterType<IndexSettingsService>()
                .As<IIndexSettingsService>()
                .SingleInstance();

            builder.RegisterType<IndexPriceService>()
                .As<IIndexPriceService>()
                .SingleInstance();

            builder.RegisterType<LimitOrderService>()
                .As<ILimitOrderService>()
                .SingleInstance();

            builder.RegisterType<OrderBookService>()
                .As<IOrderBookService>()
                .SingleInstance();

            builder.RegisterType<QuoteService>()
                .As<IQuoteService>()
                .SingleInstance();

            builder.RegisterType<PositionService>()
                .As<IPositionService>()
                .SingleInstance();

            builder.RegisterType<IndexReportService>()
                .As<IIndexReportService>()
                .SingleInstance();

            builder.RegisterType<PositionReportService>()
                .As<IPositionReportService>()
                .SingleInstance();

            builder.RegisterType<AssetLinkService>()
                .As<IAssetLinkService>()
                .SingleInstance();

            builder.RegisterType<HedgeSettingsService>()
                .As<IHedgeSettingsService>()
                .SingleInstance();

            builder.RegisterType<MarketMakerStateService>()
                .As<IMarketMakerStateService>()
                .SingleInstance();

            builder.RegisterType<SettingsService>()
                .As<ISettingsService>()
                .WithParameter(new NamedParameter("instanceName", _instanceName))
                .WithParameter(new NamedParameter("walletId", _walletId))
                .WithParameter(TypedParameter.From(_exchanges))
                .SingleInstance();

            builder.RegisterType<TimersSettingsService>()
                .As<ITimersSettingsService>()
                .SingleInstance();

            builder.RegisterType<LykkeBalancesTimer>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<InternalTradeService>()
                .As<IInternalTradeService>()
                .SingleInstance();

            builder.RegisterType<LykkeTradeService>()
                .As<ILykkeTradeService>()
                .SingleInstance();

            builder.RegisterType<VirtualTradeService>()
                .As<IVirtualTradeService>()
                .SingleInstance();

            builder.RegisterType<HedgeService>()
                .As<IHedgeService>()
                .SingleInstance();

            builder.RegisterType<MarketMakerManager>()
                .As<IIndexHandler>()
                .As<IInternalTradeHandler>()
                .SingleInstance();

            builder.RegisterType<MarketMakerService>()
                .As<IMarketMakerService>()
                .SingleInstance();

            builder.RegisterType<RateService>()
                .As<IRateService>()
                .SingleInstance();
            
            builder.RegisterType<RiskExposureReportService>()
                .As<IRiskExposureReportService>()
                .SingleInstance();
        }
    }
}
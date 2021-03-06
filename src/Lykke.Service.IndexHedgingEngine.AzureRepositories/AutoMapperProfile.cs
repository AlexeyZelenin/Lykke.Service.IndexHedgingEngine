using AutoMapper;
using JetBrains.Annotations;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Balances;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.ExternalOrders;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Hedging;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Indices;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.HedgeLimitOrders;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Instruments;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Positions;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.PrimaryMarket;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Settings;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Settlements;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Simulation;
using Lykke.Service.IndexHedgingEngine.AzureRepositories.Trades;
using Lykke.Service.IndexHedgingEngine.Domain;
using Lykke.Service.IndexHedgingEngine.Domain.Simulation;

namespace Lykke.Service.IndexHedgingEngine.AzureRepositories
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BalanceOperation, BalanceOperationEntity>(MemberList.Source)
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Timestamp));
            CreateMap<BalanceOperationEntity, BalanceOperation>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Time));

            CreateMap<Funding, FundingEntity>(MemberList.Source);
            CreateMap<FundingEntity, Funding>(MemberList.Destination);

            CreateMap<Token, TokenEntity>(MemberList.Source);
            CreateMap<TokenEntity, Token>(MemberList.Destination);

            CreateMap<ExternalOrder, ExternalOrderEntity>(MemberList.Source);
            CreateMap<ExternalOrderEntity, ExternalOrder>(MemberList.Destination);

            CreateMap<AssetHedgeSettings, AssetHedgeSettingsEntity>(MemberList.Source)
                .ForMember(dest => dest.ReferenceExchange, opt => opt.AddTransform(value => value ?? string.Empty));
            CreateMap<AssetHedgeSettingsEntity, AssetHedgeSettings>(MemberList.Destination)
                .ForMember(dest => dest.ReferenceExchange,
                    opt => opt.AddTransform(value => string.IsNullOrEmpty(value) ? null : value));

            CreateMap<IndexSettings, IndexSettingsEntity>(MemberList.Source);
            CreateMap<IndexSettingsEntity, IndexSettings>(MemberList.Destination);

            CreateMap<AssetSettings, AssetSettingsEntity>(MemberList.Source);
            CreateMap<AssetSettingsEntity, AssetSettings>(MemberList.Destination);

            CreateMap<PrimaryMarketHistoryItem, PrimaryMarketHistoryItemEntity>(MemberList.Source);
            CreateMap<PrimaryMarketHistoryItemEntity, PrimaryMarketHistoryItem>(MemberList.Destination);

            CreateMap<AssetPairSettings, AssetPairSettingsEntity>(MemberList.Source);
            CreateMap<AssetPairSettingsEntity, AssetPairSettings>(MemberList.Destination);

            CreateMap<HedgeLimitOrder, HedgeLimitOrderEntity>(MemberList.Source)
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Timestamp))
                .ForSourceMember(src => src.Error, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.ErrorMessage, opt => opt.DoNotValidate());
            CreateMap<HedgeLimitOrderEntity, HedgeLimitOrder>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore());

            CreateMap<IndexPrice, IndexPriceEntity>(MemberList.Source)
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Timestamp));
            CreateMap<IndexPriceEntity, IndexPrice>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Time));

            CreateMap<Position, PositionEntity>(MemberList.Source);
            CreateMap<PositionEntity, Position>(MemberList.Destination);

            CreateMap<HedgeSettings, HedgeSettingsEntity>(MemberList.Source);
            CreateMap<HedgeSettingsEntity, HedgeSettings>(MemberList.Destination);

            CreateMap<MarketMakerState, MarketMakerStateEntity>(MemberList.Source)
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Timestamp));
            CreateMap<MarketMakerStateEntity, MarketMakerState>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Date));

            CreateMap<QuoteThresholdSettings, QuoteThresholdSettingsEntity>(MemberList.Source);
            CreateMap<QuoteThresholdSettingsEntity, QuoteThresholdSettings>(MemberList.Destination);

            CreateMap<TimersSettings, TimersSettingsEntity>(MemberList.Source);
            CreateMap<TimersSettingsEntity, TimersSettings>(MemberList.Destination);

            CreateMap<AssetSettlement, AssetSettlementEntity>(MemberList.Source);
            CreateMap<AssetSettlementEntity, AssetSettlement>(MemberList.Destination);

            CreateMap<Settlement, SettlementEntity>(MemberList.Source)
                .ForSourceMember(src => src.Assets, opt => opt.DoNotValidate());
            CreateMap<SettlementEntity, Settlement>(MemberList.Destination)
                .ForMember(dest => dest.Assets, opt => opt.Ignore());

            CreateMap<SimulationParameters, SimulationParametersEntity>(MemberList.Source);
            CreateMap<SimulationParametersEntity, SimulationParameters>(MemberList.Destination);

            CreateMap<ExternalTrade, ExternalTradeEntity>(MemberList.Source)
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Timestamp));
            CreateMap<ExternalTradeEntity, ExternalTrade>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Date));

            CreateMap<InternalTrade, InternalTradeEntity>(MemberList.Source)
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Timestamp));
            CreateMap<InternalTradeEntity, InternalTrade>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Date));

            CreateMap<VirtualTrade, VirtualTradeEntity>(MemberList.Source)
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Timestamp));
            CreateMap<VirtualTradeEntity, VirtualTrade>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Date));
        }
    }
}

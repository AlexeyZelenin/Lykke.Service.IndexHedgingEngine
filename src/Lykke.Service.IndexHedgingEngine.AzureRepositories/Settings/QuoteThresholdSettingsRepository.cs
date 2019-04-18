﻿using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.IndexHedgingEngine.Domain.Repositories;
using Lykke.Service.IndexHedgingEngine.Domain.Settings;

namespace Lykke.Service.IndexHedgingEngine.AzureRepositories.Settings
{
    public class QuoteThresholdSettingsRepository : IQuoteThresholdSettingsRepository
    {
        private readonly INoSQLTableStorage<QuoteThresholdSettingsEntity> _storage;

        public QuoteThresholdSettingsRepository(INoSQLTableStorage<QuoteThresholdSettingsEntity> storage)
        {
            _storage = storage;
        }

        public async Task<QuoteThresholdSettings> GetAsync()
        {
            QuoteThresholdSettingsEntity entity = await _storage.GetDataAsync(GetPartitionKey(), GetRowKey());

            return Mapper.Map<QuoteThresholdSettings>(entity);
        }

        public async Task InsertOrReplaceAsync(QuoteThresholdSettings quoteThresholdSettings)
        {
            var entity = new QuoteThresholdSettingsEntity(GetPartitionKey(), GetRowKey());

            Mapper.Map(quoteThresholdSettings, entity);

            await _storage.InsertOrReplaceAsync(entity);
        }

        private static string GetPartitionKey()
            => "QuoteThreshold";

        private static string GetRowKey()
            => "QuoteThreshold";
    }
}

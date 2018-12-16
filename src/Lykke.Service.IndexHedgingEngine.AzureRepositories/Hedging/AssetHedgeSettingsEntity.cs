using JetBrains.Annotations;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IndexHedgingEngine.Domain;

namespace Lykke.Service.IndexHedgingEngine.AzureRepositories.Hedging
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateIfDirty)]
    public class AssetHedgeSettingsEntity : AzureTableEntity
    {
        private decimal _minVolume;
        private int _volumeAccuracy;
        private int _priceAccuracy;
        private bool _approved;
        private AssetHedgeMode _mode;

        public AssetHedgeSettingsEntity()
        {
        }

        public AssetHedgeSettingsEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string AssetId { get; set; }

        public string Exchange { get; set; }

        public string AssetPairId { get; set; }

        public decimal MinVolume
        {
            get => _minVolume;
            set
            {
                if (_minVolume != value)
                {
                    _minVolume = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public int VolumeAccuracy
        {
            get => _volumeAccuracy;
            set
            {
                if (_volumeAccuracy != value)
                {
                    _volumeAccuracy = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public int PriceAccuracy
        {
            get => _priceAccuracy;
            set
            {
                if (_priceAccuracy != value)
                {
                    _priceAccuracy = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public bool Approved
        {
            get => _approved;
            set
            {
                if (_approved != value)
                {
                    _approved = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public AssetHedgeMode Mode
        {
            get => _mode == AssetHedgeMode.None ? AssetHedgeMode.Disabled : _mode;
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }
    }
}
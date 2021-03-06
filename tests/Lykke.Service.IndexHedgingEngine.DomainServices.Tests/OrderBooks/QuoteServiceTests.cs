﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.Service.IndexHedgingEngine.Domain;
using Lykke.Service.IndexHedgingEngine.Domain.Constants;
using Lykke.Service.IndexHedgingEngine.Domain.Services;
using Lykke.Service.IndexHedgingEngine.DomainServices.OrderBooks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lykke.Service.IndexHedgingEngine.DomainServices.Tests.OrderBooks
{
    [TestClass]
    public class QuoteServiceTests
    {
        private readonly Mock<IQuoteThresholdSettingsService> _quoteThresholdSettingsServiceMock =
            new Mock<IQuoteThresholdSettingsService>();

        private readonly Mock<IInstrumentService> _instrumentServiceMock = new Mock<IInstrumentService>();

        private readonly QuoteThresholdSettings _quoteThresholdSettings = new QuoteThresholdSettings
        {
            Value = .2m,
            Enabled = true
        };

        private QuoteService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _quoteThresholdSettingsServiceMock.Setup(o => o.GetAsync())
                .Returns(() => Task.FromResult(_quoteThresholdSettings));

            _instrumentServiceMock.Setup(o => o.IsAssetPairExistAsync(It.IsAny<string>()))
                .Returns((string assetPair) => Task.FromResult(true));

            _service = new QuoteService(
                _quoteThresholdSettingsServiceMock.Object,
                _instrumentServiceMock.Object,
                EmptyLogFactory.Instance);
        }

        [TestMethod]
        public async Task Quote_Do_Not_Exceed_Threshold()
        {
            // arrange

            var firstQuote = new Quote("BTCUSD", DateTime.UtcNow.AddSeconds(-10), 6000, 5990, "lykke");

            decimal secondMid = firstQuote.Mid * (1 + _quoteThresholdSettings.Value - .1m);

            var secondQuote = new Quote("BTCUSD", DateTime.UtcNow, secondMid + 10, secondMid - 10, "lykke");

            // act

            await _service.UpdateAsync(firstQuote);

            await _service.UpdateAsync(secondQuote);

            Quote quote = _service.GetByAssetPairId("lykke", "BTCUSD");

            // assert

            Assert.IsTrue(secondQuote.Ask == quote.Ask && secondQuote.Bid == quote.Bid);
        }

        [TestMethod]
        public async Task Quote_Exceed_Threshold()
        {
            // arrange

            var firstQuote = new Quote("BTCUSD", DateTime.UtcNow.AddSeconds(-10), 6000, 5990, "lykke");

            decimal secondMid = firstQuote.Mid * (1 + _quoteThresholdSettings.Value + .1m);

            var secondQuote = new Quote("BTCUSD", DateTime.UtcNow, secondMid + 10, secondMid - 10, "lykke");

            // act

            await _service.UpdateAsync(firstQuote);

            await _service.UpdateAsync(secondQuote);

            Quote quote = _service.GetByAssetPairId("lykke", "BTCUSD");

            // assert

            Assert.IsTrue(firstQuote.Ask == quote.Ask && firstQuote.Bid == quote.Bid);
        }

        [TestMethod]
        public async Task Quote_Exceed_Threshold_Disabled()
        {
            // arrange

            _quoteThresholdSettings.Enabled = false;

            var firstQuote = new Quote("BTCUSD", DateTime.UtcNow.AddSeconds(-10), 6000, 5990, "lykke");

            decimal secondMid = firstQuote.Mid * (1 + _quoteThresholdSettings.Value + .1m);

            var secondQuote = new Quote("BTCUSD", DateTime.UtcNow, secondMid + 10, secondMid - 10, "lykke");

            // act

            await _service.UpdateAsync(firstQuote);

            await _service.UpdateAsync(secondQuote);

            Quote quote = _service.GetByAssetPairId("lykke", "BTCUSD");

            // assert

            Assert.IsTrue(secondQuote.Ask == quote.Ask && secondQuote.Bid == quote.Bid);
        }

        [TestMethod]
        public void Quote_Get_Avg_Price_From_0_Quotes()
        {
            // arrange

            const string assetPair = "BTCUSD";

            // act

            Quote actualQuote = _service.GetByAssetPairId(ExchangeNames.Virtual, assetPair);

            // assert

            Assert.IsNull(actualQuote);
        }
        
        [TestMethod]
        public async Task Do_Not_Update_Quote_If_No_Associated_Instrument()
        {
            // arrange

            _instrumentServiceMock.Setup(o => o.IsAssetPairExistAsync(It.IsAny<string>()))
                .Returns((string assetPair) => Task.FromResult(false));
            
            var expectedQuote = new Quote("BTCUSD", DateTime.UtcNow, 6000, 5990, "lykke");

            // act

            await _service.UpdateAsync(expectedQuote);

            Quote actualQuote = _service.GetByAssetPairId(expectedQuote.Source, expectedQuote.AssetPairId);

            // assert

            Assert.IsNull(actualQuote);
        }
    }
}

using Newtonsoft.Json;
using StockView.Database;
using StockView.Models;
using StockView.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StockView.Services
{
    public class StockService 
    {
        private IRestService _service;
        public StockService(IRestService baseService)
        {
            _service = baseService;
        }
        public async Task<StockItems[]> GetStocksInBatch(string[] testStocks)
        {
            string testQuery = string.Empty;
            foreach (var stock in testStocks)
            {
                testQuery = $"{testQuery},{stock}";
            }
            testQuery = testQuery.TrimStart(',');
            var requestUrl = $"https://financialmodelingprep.com/api/v3/quote/{testQuery}?apikey={_service.GetAPIKey()}";
            try
            {
                var content = await _service.GetContentFromRestCall(requestUrl);
                return JsonConvert.DeserializeObject<StockItems[]>(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        public async Task<List<SearchResponse>> SearchStocks(string name)
        {
            var requestUrl = $"https://financialmodelingprep.com/api/v3/search?query={name}&limit={Constants.MaxResponse}&apikey={_service.GetAPIKey()}";
            try
            {
                var content = await _service.GetContentFromRestCall(requestUrl);
                return JsonConvert.DeserializeObject<List<SearchResponse>>(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        public void AddToConfiguredStocks(string symbol)
        {
            if (!ConfiguredStocks.Stocks.Contains(symbol.ToUpper()))
            {
                ConfiguredStocks.Stocks = ConfiguredStocks.Stocks.Append(symbol.ToUpper()).ToArray();
            }
        }

        public void DeleteFromConfiguredStocks(string symbol)
        {
            ConfiguredStocks.Stocks = ConfiguredStocks.Stocks.Where(x => !x.Equals(symbol, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
        
    }
}

using StockView.Database;
using StockView.Models;
using StockView.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;


namespace StockView.Presenters
{
    public class StockPresenter
    {
        private readonly StockService service;
        private static Timer updateTimer;
        private readonly double timerInterval = 1000 *1;
        private readonly MainWindow view;
        private string[] currentStockListTodisplay = ConfiguredStocks.Stocks;
        private bool isDashboardView = true;

        public StockPresenter(MainWindow view)
        {
            this.view = view;
            service = new StockService();
            SetTimer();
        }

        public void SetTimer()
        {
            updateTimer = new Timer(timerInterval);
            updateTimer.Elapsed += OnTimedEvent;
            updateTimer.AutoReset = false;
            EnableTimer();
        }

        public void DisableTimer()
        {
            updateTimer.Enabled = false;
        }

        public void EnableTimer()
        {
            updateTimer.Enabled = true;
        }

        public async Task<List<SearchResponse>> SearchBySymbol(string symbol)
        {
            return await service.SearchStocks(symbol);
        }

        public async Task AddToConfiguredStocks(string symbol)
        {
            service.AddToConfiguredStocks(symbol);
            await SwitchToDashBoard();
            OnTimedEvent(null, null);
        }

        public async void DeleteFromConfiguredStocks(string symbol)
        {
            service.DeleteFromConfiguredStocks(symbol);
            await SwitchToDashBoard();
            OnTimedEvent(null, null);
        }

        public async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            var stocks = FilterInvalidStocks((await service.GetStocksInBatch(currentStockListTodisplay)).ToList());
            view.Dispatcher.Invoke(() =>
            {
                view.UpdateStockDataIntoGrid(stocks, isDashboardView);
            });
        }

        private List<StockItems> FilterInvalidStocks(List<StockItems> stocks)
        {
            return stocks.Where(x => !(string.IsNullOrEmpty(x.name) || string.IsNullOrEmpty(x.symbol) || string.IsNullOrEmpty(x.exchange) || x.yearHigh == null || x.yearLow == null || x.price == null || x.changesPercentage == null)).ToList();
        }

        public async Task SwitchToSearchView(List<string> stockList)
        {
            currentStockListTodisplay = stockList.ToArray();
            isDashboardView = false;
            OnTimedEvent(null, null);
            EnableTimer();
        }

        public async Task SwitchToDashBoard()
        {
            currentStockListTodisplay = ConfiguredStocks.Stocks;
            isDashboardView = true;
            OnTimedEvent(null, null);
            EnableTimer();
        }

    }

}

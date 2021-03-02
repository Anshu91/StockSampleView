using System.Linq;

namespace StockView.Database
{
    public class ConfiguredStocks
    {
        //some more foreign stocks symbols
        //"GOOGL", "TCDA", "WMT", "LIXT", "OCGN"
        public static string[] ForeignStocks = { "AAPL", "MSFT", "FB", "ZNGA", "NVDA", "GOOGL", "TCDA", "WMT", "LIXT", "OCGN" };
        public static string[] IndianStocks = { "ONGC.NS", "GAIL.NS", "ICICIBANK.NS", "HDFCBANK.NS", "SBIN.NS" };
        public static string[] Stocks = ForeignStocks.Union(IndianStocks).ToArray();


    }
}

using System.Linq;

namespace StockView.Database
{
    public class ConfiguredStocks
    {
        //some more foreign stocks symbols
        //"GOOGL", "TCDA", "WMT", "LIXT", "OCGN"
        public static string[] ForeignStocks = { "AAPL", "MSFT", "FB", "ZNGA", "NVDA", "GOOGL", "TCDA", "WMT", "LIXT", "OCGN" };
        public static string[] IndianStocks = { "ONGC.NS", "GAIL.NS", "ICICIBANK.NS", "HDFCBANK.NS", "SBIN.NS", "RCOM.NS", "RELCAPITAL.NS","BHEL.NS", "TCS.NS", "INFY.NS" };
        public static string[] Stocks = IndianStocks;//ForeignStocks.Union(IndianStocks).ToArray();


    }
}

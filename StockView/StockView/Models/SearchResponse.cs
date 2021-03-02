namespace StockView.Models
{
    public class SearchResponse
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public string stockExchange { get; set; }
        public string exchangeShortName { get; set; }
    }
}

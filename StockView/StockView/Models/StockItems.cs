using System;

namespace StockView.Models
{
    /*
     Sample data below:
    [ {
  "symbol" : "AAPL",
  "name" : "Apple Inc.",
  "price" : 121.26000000,
  "changesPercentage" : 0.22000000,
  "change" : 0.27000000,
  "dayLow" : 121.20000000,
  "dayHigh" : 124.85000000,
  "yearHigh" : 145.09000000,
  "yearLow" : 53.15250000,
  "marketCap" : 2035725107200.00000000,
  "priceAvg50" : 132.95758000,
  "priceAvg200" : 122.75954400,
  "volume" : 164560045,
  "avgVolume" : 105668695,
  "exchange" : "NASDAQ",
  "open" : 122.59000000,
  "previousClose" : 120.99000000,
  "eps" : 3.68700000,
  "pe" : 32.88852700,
  "earningsAnnouncement" : "2021-01-27T16:30:00.000+0000",
  "sharesOutstanding" : 16788100835,
  "timestamp" : 1614397117
} ]


    another sample data
    [ {
  "symbol" : "OCGN",
  "name" : "Ocugen, Inc.",
  "price" : 10.95000000,
  "changesPercentage" : 36.88000000,
  "change" : 2.95000000,
  "dayLow" : 7.62000000,
  "dayHigh" : 11.95000000,
  "yearHigh" : 18.77000000,
  "yearLow" : 0.17000000,
  "marketCap" : 2058972288.00000000,
  "priceAvg50" : 5.61242440,
  "priceAvg200" : 1.76213870,
  "volume" : 90937704,
  "avgVolume" : 84269700,
  "exchange" : "NASDAQ",
  "open" : 8.05000000,
  "previousClose" : 8.00000000,
  "eps" : null,
  "pe" : null,
  "earningsAnnouncement" : "2020-11-06T07:30:00.000+0000",
  "sharesOutstanding" : 188033999,
  "timestamp" : 1614399668
} ]
     */

    public class StockItems
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public float? price { get; set; }
        public float? changesPercentage { get; set; }
        public float? change { get; set; }
        public float? dayLow { get; set; }
        public float? dayHigh { get; set; }
        public float? yearHigh { get; set; }
        public float? yearLow { get; set; }
        public float? marketCap { get; set; }
        public float? priceAvg50 { get; set; }
        public float? priceAvg200 { get; set; }
        public int? volume { get; set; }
        public int? avgVolume { get; set; }
        public string exchange { get; set; }
        public float? open { get; set; }
        public float? previousClose { get; set; }
        public float? eps { get; set; }
        public float? pe { get; set; }
        public DateTime? earningsAnnouncement { get; set; }
        public long? sharesOutstanding { get; set; }
        public int? timestamp { get; set; }
    }

}

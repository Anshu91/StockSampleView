using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using StockView.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace StockViewTests
{
    public class StockServiceTests
    {
        private Mock<IRestService> MockService = new Mock<IRestService>();
        private StockService stockService;
        public StockServiceTests()
        {
            stockService = new StockService(MockService.Object);
            var filename = "dummytestresponse.txt";
            var content = File.ReadAllText(filename);
            MockService.Setup(x => x.GetContentFromRestCall(It.IsAny<string>())).Returns(Task.FromResult(content));
        }

        [Fact]
        public async void StockService_RestAPITest()
        {
               var stocks = await stockService.GetStocksInBatch( new string[] { "AAPL", "Googl" });
            MockService.Verify(x => x.GetContentFromRestCall(It.IsAny<string>()), Times.Once());
        }


        [Fact]
        public async void StockService_RestAPITest2()
        {
            var stocks = await stockService.SearchStocks("AAPL");
            MockService.Verify(x => x.GetContentFromRestCall(It.IsAny<string>()), Times.Once());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockView.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace StockViewTests
{
    public class StockServiceTests
    {
        private Mock<BaseService> MockService = new Mock<BaseService>();

        public StockServiceTests()
        {
            var filename = "dummytestresponse.txt";
            var content = File.ReadAllText(filename);
            MockService.Setup(x => x.GetContentFromRestCall(It.IsAny<string>())).Returns(Task.FromResult(content));
        }
        [Fact]
        public void StockService_RestAPITest()
        {

            
        }
    }
}

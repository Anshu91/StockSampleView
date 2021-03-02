using System.Threading.Tasks;

namespace StockView.Services
{
    public interface IService
    {
        Task<string> GetContentFromRestCall(string url);
    }
}

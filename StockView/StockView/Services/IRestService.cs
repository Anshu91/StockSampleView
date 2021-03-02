using System.Threading.Tasks;

namespace StockView.Services
{
    public interface IRestService
    {
        Task<string> GetContentFromRestCall(string url);
        string GetAPIKey();
    }
}

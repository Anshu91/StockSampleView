using Newtonsoft.Json;
using StockView.Utilities;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace StockView.Services
{
    public class RestService : IRestService
    {
        private int _apiCount = -1;
        private int _exceptionCount = 0;
        private string lastContent = string.Empty;
        private int apiKeyIndex = 0;
        public async Task<string> GetContentFromRestCall(string url)
        {
            using (var httpClient = new HttpClient())
            {
                string content = string.Empty;
                _apiCount++;
                _apiCount %= 5;
                var savedResponseInFile = "testresponse" + _apiCount.ToString() + ".txt";
                try
                {
                    if(true)// (!File.Exists(savedResponseInFile))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))

                        {
                            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                            //Console.WriteLine($"start time={DateTime.Now:HH:mm:ss.fff}");
                            var response = await httpClient.SendAsync(request);
                            content = await response.Content.ReadAsStringAsync();
                            //Console.WriteLine($"stop time={DateTime.Now:HH:mm:ss.fff}");
                            //File.WriteAllText(savedResponseInFile, content);
                            lastContent = content;
                            return content;
                        }
                    }
                    else
                    {
                        return File.ReadAllText(savedResponseInFile);
                    }
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(content))
                    {
                        var result = JsonConvert.DeserializeObject<JsonException>(content);
                        //MessageBox.Show(result.Message);
                    }
                    _exceptionCount++;
                    ChangeAPIKey();
                    return lastContent;
                    
                }
            }

        }

        private void ChangeAPIKey()
        {
            apiKeyIndex++;
            apiKeyIndex %= 4;
        }

        public string GetAPIKey()
        {
            return Constants.ApiKeys[apiKeyIndex];
        }
    }
}

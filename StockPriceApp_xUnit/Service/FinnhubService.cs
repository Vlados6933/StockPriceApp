using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Text.Json;

namespace Service
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                StreamReader streamReader = new StreamReader(stream);

                string response = await streamReader.ReadToEndAsync();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>?>(response);

                if(responseDictionary == null)
                    throw new InvalidOperationException("No response from finhub server");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;          
            }
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using(HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                StreamReader streamReader = new StreamReader(stream);
                string response = await streamReader.ReadToEndAsync();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize <Dictionary<string, object>>(response);

                if (responseDictionary == null) 
                    throw new InvalidOperationException("No response from finhub server");

                if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;
            }
        }

    }
}

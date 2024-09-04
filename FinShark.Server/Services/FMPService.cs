using FinShark.Server.Dtos.Stock;
using FinShark.Server.Interfaces;
using FinShark.Server.Mappers;
using FinShark.Server.Models;
using Newtonsoft.Json;

namespace FinShark.Server.Services
{
    public class FMPService : InterfaceFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");

                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];

                    if(stock != null)
                    {
                        return stock.ToStockFromFMPStock();
                    }
                    return null;
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

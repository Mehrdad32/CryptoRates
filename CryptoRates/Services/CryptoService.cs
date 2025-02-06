using CryptoRates.Models;
using System.Text.Json;

namespace CryptoRates.Services
{
    public class CryptoService(HttpClient httpClient) : ICryptoService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _coinMarketCapApiKey = "1cff7826-0ba7-40fb-82fe-571465af3552";
        private readonly string _exchangeRatesApiKey = "53c49443c2a9a29e38a86f18ee9b620b";
        private readonly List<string> _currencies = ["USD", "EUR", "BRL", "GBP", "AUD"];

        public async Task<List<CryptoRate>> GetCryptoRatesAsync(string cryptoCode)
        {
            var result = new List<CryptoRate>();

            try
            {
                string cryptoUrl = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={cryptoCode}&convert=EUR";
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _coinMarketCapApiKey);
                var cryptoResponse = await _httpClient.GetFromJsonAsync<JsonElement>(cryptoUrl);

                if (!cryptoResponse.TryGetProperty("data", out var dataProperty) ||
                    !dataProperty.TryGetProperty(cryptoCode.ToUpper(), out var cryptoData) ||
                    !cryptoData.TryGetProperty("quote", out var quoteProperty) ||
                    !quoteProperty.TryGetProperty("EUR", out var eurProperty) ||
                    !eurProperty.TryGetProperty("price", out var priceProperty))
                {
                    Console.WriteLine($"Invalid API response for {cryptoCode}");
                    return result; 
                }

                decimal cryptoPriceEur = priceProperty.GetDecimal();
                result.Add(new CryptoRate { Currency = "EUR", Price = cryptoPriceEur });

                string exchangeUrl = $"https://api.exchangeratesapi.io/latest?access_key={_exchangeRatesApiKey}&base=EUR";
                var exchangeResponse = await _httpClient.GetFromJsonAsync<JsonElement>(exchangeUrl);

                if (!exchangeResponse.TryGetProperty("rates", out var rates))
                {
                    Console.WriteLine("ExchangeRates API returned an invalid response.");
                    return result;
                }

                foreach (var currency in _currencies.Where(c => c != "EUR"))
                {
                    if (rates.TryGetProperty(currency, out var exchangeRateProperty))
                    {
                        decimal exchangeRate = exchangeRateProperty.GetDecimal();
                        result.Add(new CryptoRate { Currency = currency, Price = cryptoPriceEur * exchangeRate });
                    }
                    else
                    {
                        Console.WriteLine($"Exchange rate for {currency} not found.");
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cryptocurrency rates: {ex.Message}");
                return result;
            }
        }

        public async Task<List<string>> GetSimilarCryptoNamesAsync(string query)
        {
            var suggestions = new List<string>();
            try
            {
                string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/map";
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _coinMarketCapApiKey);
                var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);

                if (!response.TryGetProperty("data", out var dataArray))
                {
                    Console.WriteLine("Invalid API response for similar names.");
                    return suggestions;
                }

                foreach (var item in dataArray.EnumerateArray())
                {
                    if (item.TryGetProperty("name", out var nameProperty) &&
                        item.TryGetProperty("symbol", out var symbolProperty))
                    {
                        string name = nameProperty.GetString()!;
                        string symbol = symbolProperty.GetString()!;

                        if (name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            symbol.Contains(query, StringComparison.OrdinalIgnoreCase))
                        {
                            suggestions.Add($"{symbol} - {name}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching similar crypto names: {ex.Message}");
            }
            return suggestions;
        }
    }
}

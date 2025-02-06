using CryptoRates.Models;

namespace CryptoRates.Services
{
    public interface ICryptoService
    {
        Task<List<CryptoRate>> GetCryptoRatesAsync(string cryptoCode);

        Task<List<string>> GetSimilarCryptoNamesAsync(string query);
    }
}

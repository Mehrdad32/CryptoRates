using CryptoRates.Models;
using CryptoRates.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoRates.Controllers
{
    public class CryptoController(ICryptoService cryptoService) : Controller
    {
        private readonly ICryptoService _cryptoService = cryptoService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRates(string cryptoCode)
        {
            if (string.IsNullOrWhiteSpace(cryptoCode))
            {
                ViewBag.ErrorMessage = "Please enter a valid cryptocurrency code.";
                return View("Index", new List<CryptoRate>());
            }

            var rates = await _cryptoService.GetCryptoRatesAsync(cryptoCode);

            if (rates == null || rates.Count == 0)
            {
                var suggestions = await _cryptoService.GetSimilarCryptoNamesAsync(cryptoCode);
                ViewBag.ErrorMessage = $"No results found for '{cryptoCode}'. Did you mean:";
                ViewBag.Suggestions = suggestions;
                return View("Index", new List<CryptoRate>());
            }

            ViewBag.CryptoCode = cryptoCode.ToUpper();
            return View("Index", rates);
        }
    }
}

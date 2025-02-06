using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoRates.Services;
using Moq;
using Moq.Protected;
using Xunit;

public class CryptoServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly CryptoService _cryptoService;

    public CryptoServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _cryptoService = new CryptoService(_httpClient);
    }

    [Fact]
    public async Task GetCryptoRatesAsync_ValidCryptoCode_ReturnsRates()
    {
        // Arrange
        var fakeApiResponse = new
        {
            data = new
            {
                BTC = new
                {
                    quote = new
                    {
                        EUR = new { price = 30000.0m }
                    }
                }
            }
        };

        var fakeExchangeRatesResponse = new
        {
            rates = new
            {
                USD = 1.1m,
                EUR = 1.0m,
                BRL = 5.5m,
                GBP = 0.9m,
                AUD = 1.6m
            },
            @base = "EUR", 
            date = "2025-02-06"
        };


        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.AbsoluteUri.Contains("coinmarketcap.com")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(fakeApiResponse))
            });

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.AbsoluteUri.Contains("exchangeratesapi.io")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(fakeExchangeRatesResponse))
            });

        // Act
        var result = await _cryptoService.GetCryptoRatesAsync("BTC");

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x.Currency == "EUR");
        Assert.Contains(result, x => x.Currency == "USD");
    }

    [Fact]
    public async Task GetCryptoRatesAsync_InvalidCryptoCode_ReturnsEmptyList()
    {
        // Arrange
        var fakeApiResponse = new { data = new { } };
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(fakeApiResponse))
            });

        // Act
        var result = await _cryptoService.GetCryptoRatesAsync("INVALID_CODE");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetSimilarCryptoNamesAsync_ValidQuery_ReturnsSuggestions()
    {
        // Arrange
        var fakeApiResponse = new
        {
            data = new[]
            {
                new { name = "Bitcoin", symbol = "BTC" },
                new { name = "Ethereum", symbol = "ETH" }
            }
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(fakeApiResponse))
            });

        // Act
        var result = await _cryptoService.GetSimilarCryptoNamesAsync("BTC");

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x.Contains("Bitcoin"));
    }

    [Fact]
    public async Task GetSimilarCryptoNamesAsync_InvalidQuery_ReturnsEmptyList()
    {
        // Arrange
        var fakeApiResponse = new { data = Array.Empty<object>() };
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(fakeApiResponse))
            });

        // Act
        var result = await _cryptoService.GetSimilarCryptoNamesAsync("INVALID");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}

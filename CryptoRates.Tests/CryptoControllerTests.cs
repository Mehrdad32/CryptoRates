using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoRates.Controllers;
using CryptoRates.Models;
using CryptoRates.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class CryptoControllerTests
{
    [Fact]
    public async Task GetRates_ValidCryptoCode_ReturnsViewWithRates()
    {
        // Arrange
        var mockService = new Mock<ICryptoService>();
        mockService.Setup(x => x.GetCryptoRatesAsync("BTC"))
            .ReturnsAsync(
            [
                new CryptoRate { Currency = "USD", Price = 30000.0m },
                new CryptoRate { Currency = "EUR", Price = 25000.0m }
            ]);

        var controller = new CryptoController(mockService.Object);

        // Act
        var result = await controller.GetRates("BTC") as ViewResult;

        // Assert
        Assert.NotNull(result);
        var model = Assert.IsAssignableFrom<List<CryptoRate>>(result.Model);
        Assert.Equal(2, model.Count);
    }

    [Fact]
    public async Task GetRates_InvalidCryptoCode_ReturnsViewWithErrorMessage()
    {
        // Arrange
        var mockService = new Mock<ICryptoService>();
        mockService.Setup(x => x.GetCryptoRatesAsync("INVALID_CODE"))
            .ReturnsAsync([]);

        var controller = new CryptoController(mockService.Object);

        // Act
        var result = await controller.GetRates("INVALID_CODE") as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ViewName);
        Assert.Contains("No results found", (string)controller.ViewBag.ErrorMessage);
    }
}

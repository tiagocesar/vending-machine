using System;
using Domain;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests
{
    public class SellingServiceTests
    {
        [Theory(DisplayName = "Should sell product")]
        [InlineData(1, 130)]
        [InlineData(2, 180)]
        [InlineData(3, 180)]
        [InlineData(4, 180)]
        public void SellProduct_01(int code, int paidAmount)
        {
            var productGrid = new ProductGrid();

            productGrid.AddProduct(1, "Tea", 130, 10);
            productGrid.AddProduct(2, "Espresso", 180, 20);
            productGrid.AddProduct(3, "Juice", 180, 20);
            productGrid.AddProduct(4, "Chicken soup", 180, 15);

            var sut = new SellingService(productGrid);

            var result = sut.SellProduct(code, paidAmount);
            result.Should().BeTrue();
        }
    }
}
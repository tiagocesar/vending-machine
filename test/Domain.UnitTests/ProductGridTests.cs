using System;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests
{
    public class ProductGridTests
    {
        [Fact(DisplayName = "Should add a product successfully")]
        public void AddProduct_01()
        {
            var sut = new ProductGrid();

            var result = sut.AddProduct(1, "Tea", 130, 20);

            result.Code.Should().Be(1);
            result.Name.Should().Be("Tea");
            result.Price.Should().Be(130);
        }
        
        [Fact(DisplayName = "Should fail to add a product with a ArgumentException")]
        public void AddProduct_02()
        {
            var sut = new ProductGrid();

            sut.AddProduct(1, "Tea", 130, 20);

            var exception = Record.Exception(() => sut.AddProduct(1, "Tea", 130, 20));

            Assert.IsType<ArgumentException>(exception);
        }
        
        [Fact(DisplayName = "Should find the specified product")]
        public void GetProduct_01()
        {
            var productGrid = new ProductGrid();

            productGrid.AddProduct(1, "Tea", 130, 20);

            var result = productGrid.GetProduct(1);

            result.Code.Should().Be(1);
            result.Name.Should().Be("Tea");
            result.Price.Should().Be(130);
        }

        [Fact(DisplayName = "Should fail to find the specified product")]
        public void GetProduct_02()
        {
            var productGrid = new ProductGrid();

            var exception = Record.Exception(() => productGrid.GetProduct(1));

            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
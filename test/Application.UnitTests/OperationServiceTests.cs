using System;
using System.Linq;
using Application.Exceptions;
using Domain;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests
{
    public class OperationServiceTests
    {
        [Fact(DisplayName = "Adding 2 1 euros coins should return an available amount of 200")]
        public void AddCoin_01()
        {
            var customerCoinStack = new CustomerCoinStack();
            var sut = new OperationService(customerCoinStack, default, default);

            sut.AddCoin(100);
            var results = sut.AddCoin(100);

            results.Should().Be(200);
        }

        [Fact(DisplayName = "Should return the 2 coins that were inserted")]
        public void ReturnCoins_01()
        {
            var customerCoinStack = new CustomerCoinStack();
            var sut = new OperationService(customerCoinStack, default, default);

            sut.AddCoin(50);
            sut.AddCoin(20);

            var result = sut.ReturnCoins();

            result.First().Should().Be(20);
            result.Last().Should().Be(50);

            customerCoinStack.GetTotal().Should().Be(0);
        }

        [Fact(DisplayName = "Should sell a product and return change")]
        public void ProcessSale_01()
        {
            var customerCoinStack = new CustomerCoinStack();
            customerCoinStack.Push(100);
            customerCoinStack.Push(100);

            var productGrid = new ProductGrid();
            productGrid.AddProduct(1, "Tea", 130, 20);

            var machineCoinStack = new MachineCoinStack();
            machineCoinStack.AddCoin(50);
            machineCoinStack.AddCoin(20);

            var changeService = new ChangeService(machineCoinStack);

            var sut = new OperationService(customerCoinStack, productGrid, changeService);

            var result = sut.ProcessSale(1);

            var product = result.Product;
            product.Code.Should().Be(1);
            product.Name.Should().Be("Tea");
            product.Price.Should().Be(130);
            product.Quantity.Should().Be(19);

            var change = result.Change;
            change.First().Should().Be(20);
            change.Last().Should().Be(50);
        }

        [Fact(DisplayName = "Should refuse to sell a product with invalid code")]
        public void ProcessSale_02()
        {
            var customerCoinStack = new CustomerCoinStack();
            customerCoinStack.Push(100);
            customerCoinStack.Push(100);

            var productGrid = new ProductGrid();
            productGrid.AddProduct(1, "Tea", 130, 20);

            var sut = new OperationService(customerCoinStack, productGrid, new ChangeService(new MachineCoinStack()));

            var exception = Record.Exception(() => sut.ProcessSale(2));

            Assert.IsType<ArgumentNullException>(exception);
        }
        
        [Fact(DisplayName = "Should refuse to sell a product that is depleted")]
        public void ProcessSale_03()
        {
            var customerCoinStack = new CustomerCoinStack();
            customerCoinStack.Push(100);
            customerCoinStack.Push(100);

            var productGrid = new ProductGrid();
            productGrid.AddProduct(1, "Tea", 130, 0);

            var sut = new OperationService(customerCoinStack, productGrid, new ChangeService(new MachineCoinStack()));

            var exception = Record.Exception(() => sut.ProcessSale(1));

            Assert.IsType<ProductNotAvailableException>(exception);
        }
        
        [Fact(DisplayName = "Should refuse to sell a product due to insufficient change")]
        public void ProcessSale_04()
        {
            var customerCoinStack = new CustomerCoinStack();
            customerCoinStack.Push(100);
            customerCoinStack.Push(100);

            var productGrid = new ProductGrid();
            productGrid.AddProduct(1, "Tea", 130, 20);

            var sut = new OperationService(customerCoinStack, productGrid, new ChangeService(new MachineCoinStack()));

            var exception = Record.Exception(() => sut.ProcessSale(1));

            Assert.IsType<NotEnoughChangeException>(exception);
        }
    }
}
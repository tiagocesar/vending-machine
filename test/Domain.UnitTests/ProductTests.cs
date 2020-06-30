using Domain.Exceptions;
using Xunit;

namespace Domain.UnitTests
{
    public class ProductTests
    {
        [Fact(DisplayName = "Should fail with ProductNotAvailableException")]
        public void Sell_01()
        {
            var product = new Product(1, "Tea", 130, 0);
            
            var exception = Record.Exception(() => product.Sell(130));

            Assert.IsType<ProductNotAvailableException>(exception);
        }
        
        [Fact(DisplayName = "Should fail with InsufficientAmountException")]
        public void Sell_02()
        {
            var product = new Product(1, "Tea", 130, 20);
            
            var exception = Record.Exception(() => product.Sell(100));

            Assert.IsType<InsufficientAmountException>(exception);
        }
    }
}
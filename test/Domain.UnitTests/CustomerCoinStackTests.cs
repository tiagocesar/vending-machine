using FluentAssertions;
using Xunit;

namespace Domain.UnitTests
{
    public class CustomerCoinStackTests
    {
        [Fact(DisplayName = "Pushing a value to the stack should return the sum of all added values")]
        public void Push_01()
        {
            var sut = new CustomerCoinStack();

            var total = sut.Push(100);
            total.Should().Be(100);
            
            total = sut.Push(50);
            total.Should().Be(150);
        }

        [Fact(DisplayName = "After adding values to the stack, it should be possible to get them")]
        public void TryPop_01()
        {
            var sut = new CustomerCoinStack();

            sut.Push(100);

            sut.TryPop(out var coin);
            coin.Should().Be(100);
        }
    }
}
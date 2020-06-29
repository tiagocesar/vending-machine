using Application.Exceptions;
using Domain;
using Domain.Contracts;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests
{
    public class ChangeServiceTests
    {
        private readonly IMachineCoinStack _machineCoinStack = new MachineCoinStack();

        public ChangeServiceTests()
        {
            // Adding sample coins
            for (var i = 0; i < 100; i++)
            {
                _machineCoinStack.AddCoin(50);
                _machineCoinStack.AddCoin(100);
            }
            
            _machineCoinStack.AddCoin(10);
            _machineCoinStack.AddCoin(20);
        }

        [Theory(DisplayName = "Various change operations should succeed")]
        [InlineData(120, 130, new int[0])]
        [InlineData(130, 130, new int[0])]
        [InlineData(140, 130, new[] { 10 })]
        [InlineData(150, 130, new[] { 20 })]
        [InlineData(180, 130, new[] { 50 })]
        [InlineData(200, 130, new[] { 20, 50 })]
        [InlineData(300, 130, new[] { 20, 50, 100 })]
        [InlineData(170, 180, new int[0])]
        [InlineData(180, 180, new int[0])]
        [InlineData(190, 180, new[] { 10 })]
        [InlineData(200, 180, new[] { 20 })]
        [InlineData(250, 180, new[] { 20, 50 })]
        [InlineData(500, 180, new[] { 20, 100, 100, 100 })]
        public void CalculateChange_01(int paidAmount, int price, int[] expectedResult)
        {
            var change = paidAmount - price;

            var sut = new ChangeService(_machineCoinStack);
            var actualResult = sut.CalculateChange(change);

            expectedResult.Should().Equal(actualResult);
        }
        
        [Fact(DisplayName = "Should return an exception due to not having enough change:")]
        public void CalculateChange_02()
        {
            var sut = new ChangeService(_machineCoinStack);
            var exception = Record.Exception(() => sut.CalculateChange(90));
	
            Assert.IsType<NotEnoughChangeException>(exception);
        }
    }
}
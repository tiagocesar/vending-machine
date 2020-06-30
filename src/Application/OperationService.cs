using Application.Contracts;
using Domain;
using Domain.Contracts;

namespace Application
{
    public class OperationService : IOperationService
    {
        private readonly ICustomerCoinStack _customerCoinStack;
        private readonly IProductGrid _productGrid;
        private readonly IChangeService _changeService;
        private readonly IMachineCoinStack _machineCoinStack;

        public OperationService(ICustomerCoinStack customerCoinStack, IProductGrid productGrid,
            IChangeService changeService, IMachineCoinStack machineCoinStack)
        {
            _customerCoinStack = customerCoinStack;
            _productGrid = productGrid;
            _changeService = changeService;
            _machineCoinStack = machineCoinStack;
        }

        public int AddCoin(int value) => _customerCoinStack.Push(value);

        public int[] ReturnCoins() => _customerCoinStack.Flush();

        public Sale ProcessSale(int productCode)
        {
            var product = _productGrid.GetProduct(productCode);
            var paidAmount = _customerCoinStack.GetTotal();
            var change = _changeService.CalculateChange(paidAmount - product.Price).ToArray();

            product.Sell(paidAmount);

            // Adding the customer coins to the machine coin stack
            while (_customerCoinStack.TryPop(out var coin))
            {
                _machineCoinStack.AddCoin(coin);
            }

            return new Sale(product, change);
        }
    }
}
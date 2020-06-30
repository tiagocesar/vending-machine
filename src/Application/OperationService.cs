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

        public OperationService(ICustomerCoinStack customerCoinStack, IProductGrid productGrid, IChangeService changeService)
        {
            _customerCoinStack = customerCoinStack;
            _productGrid = productGrid;
            _changeService = changeService;
        }

        public int AddCoin(int value) => _customerCoinStack.Push(value);
        
        public int[] ReturnCoins() => _customerCoinStack.Flush();

        public Sale ProcessSale(int productCode)
        {
            var product = _productGrid.GetProduct(productCode);
            var paidAmount = _customerCoinStack.GetTotal();
            var change = _changeService.CalculateChange(paidAmount - product.Price).ToArray();
            
            product.Sell(paidAmount);

            return new Sale(product, change);
        }
    }
}
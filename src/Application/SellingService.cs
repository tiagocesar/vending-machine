using Application.Contracts;
using Domain.Contracts;

namespace Application
{
    public class SellingService : ISellingService
    {
        private readonly IProductGrid _productGrid;

        public SellingService(IProductGrid productGrid)
        {
            _productGrid = productGrid;
        }

        public bool SellProduct(int code, int paidAmount)
        {
            var product = _productGrid.GetProduct(code);
            return product.Sell(paidAmount);
        }
    }
}
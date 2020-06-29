using Domain.Contracts;

namespace Application
{
    public class SellingService
    {
        private readonly IProductGrid _productGrid;

        public SellingService(IProductGrid productGrid)
        {
            _productGrid = productGrid;
        }

        public void SellProduct()
        {
            
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using Domain;
using Domain.Contracts;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public class MachineInitialization
    {
        private bool _executed;
        private readonly IMachineCoinStack _machineCoinStack;
        private readonly IProductGrid _productGrid;

        public MachineInitialization(IMachineCoinStack machineCoinStack, IProductGrid productGrid)
        {
            _machineCoinStack = machineCoinStack;
            _productGrid = productGrid;
        }

        public void Initialize()
        {
            // Should run only once per app initialization
            if (_executed) return;
            
            // Setting the default amount of coins
            for (var i = 0; i < 100; i++)
            {
                foreach (var coinValue in Settings.CoinValues)
                {
                    _machineCoinStack.AddCoin(coinValue);
                }                
            }

            // Adding products
            _productGrid.AddProduct(1, "Tea", 130, 10);
            _productGrid.AddProduct(2, "Espresso", 180, 20);
            _productGrid.AddProduct(3, "Juice", 180, 20);
            _productGrid.AddProduct(4, "Chicken soup", 180, 15);

            _executed = true;
        }
    }
}
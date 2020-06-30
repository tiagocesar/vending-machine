using Domain;

namespace Application.Contracts
{
    public interface IOperationService
    {
        int AddCoin(int value);
        int[] ReturnCoins();
        Sale ProcessSale(int productCode);
    }
}
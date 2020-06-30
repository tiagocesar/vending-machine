using Domain;

namespace Application.Contracts
{
    public interface IOperationService
    {
        int[] ReturnCoins();
        Sale ProcessSale(int productCode);
    }
}
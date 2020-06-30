namespace Application.Contracts
{
    public interface ISellingService
    {
        bool SellProduct(int code, int paidAmount);
    }
}
namespace Domain.Contracts
{
    public interface ICustomerCoinStack
    {
        int Push(int coin);
        bool TryPop(out int coin);
    }
}
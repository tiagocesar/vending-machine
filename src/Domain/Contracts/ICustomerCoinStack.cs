namespace Domain.Contracts
{
    public interface ICustomerCoinStack
    {
        int GetTotal();
        int Push(int coin);
        bool TryPop(out int coin);
        int[] Flush();
    }
}
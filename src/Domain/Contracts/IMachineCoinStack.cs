namespace Domain.Contracts
{
    public interface IMachineCoinStack
    {
        void AddCoin(int faceValue);
        bool RemoveCoin(int faceValue);
    }
}
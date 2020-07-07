using System.Collections.Generic;
using Domain.Contracts;

namespace Domain
{
    public class MachineCoinStack : IMachineCoinStack
    {
        private Dictionary<int, int> Coins { get; } = new Dictionary<int, int>();

        public MachineCoinStack()
        {
            // Initial seed
            Coins.Add(10, 0);
            Coins.Add(20, 0);
            Coins.Add(50, 0);
            Coins.Add(100, 0);
        }

        public void AddCoin(int faceValue) => Coins[faceValue] += 1;
        
        public bool RemoveCoin(int faceValue)
        {
            if (Coins[faceValue] <= 0) return false;
            
            Coins[faceValue] -= 1;
            return true;
        }

        public bool HasEnoughCoins(int faceValue, int amount) => Coins[faceValue] >= amount;
    }
}
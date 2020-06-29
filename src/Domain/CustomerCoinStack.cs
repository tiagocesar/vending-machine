using System.Collections.Generic;
using System.Linq;
using Domain.Contracts;

namespace Domain
{
    public class CustomerCoinStack : Stack<int>, ICustomerCoinStack
    {
        private Stack<int> Coins { get; } = new Stack<int>();

        public new int Push(int coin)
        {
            Coins.Push(coin);

            return Coins.Sum();
        }

        public new bool TryPop(out int coin) => Coins.TryPop(out coin);
    }
}
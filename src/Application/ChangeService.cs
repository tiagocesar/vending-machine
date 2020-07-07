using System.Collections.Generic;
using Application.Contracts;
using Application.Exceptions;
using Domain;
using Domain.Contracts;

namespace Application
{
    public class ChangeService : IChangeService
    {
        private readonly IMachineCoinStack _machineCoinStack;

        public ChangeService(IMachineCoinStack machineCoinStack)
        {
            _machineCoinStack = machineCoinStack;
        }

        public Stack<int> CalculateChange(int change)
        {
            var userCoinStack = new Stack<int>();
            var tempCoinsStack = new Dictionary<int, int>();

            foreach (var faceValue in Settings.CoinValues)
            {
                var totalCoins = CalculateCoinsToReturn(ref change, faceValue);
                tempCoinsStack.Add(faceValue, totalCoins);
            }

            if (change > 0) throw new NotEnoughChangeException("Not enough change");
            
            ReleaseCoins(tempCoinsStack, userCoinStack);

            return userCoinStack;
        }

        private int CalculateCoinsToReturn(ref int change, int faceValue)
        {
            var amountOfCoins = change / faceValue;

            if (amountOfCoins == 0) return 0;
            if (!_machineCoinStack.HasEnoughCoins(faceValue, amountOfCoins)) return 0;
            
            change -= amountOfCoins * faceValue;

            return amountOfCoins;
        }

        private void ReleaseCoins(Dictionary<int, int> tempCoinStack, Stack<int> userCoinStack)
        {
            foreach (var faceValue in tempCoinStack.Keys)
            {
                for (var i = 1; i <= tempCoinStack[faceValue]; i++)
                {
                    _machineCoinStack.RemoveCoin(faceValue);
                    userCoinStack.Push(faceValue);
                }
            }
        }
    }
}
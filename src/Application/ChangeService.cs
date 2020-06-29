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

            foreach (var faceValue in Settings.CoinValues)
            {
                SelectCoins(ref change, faceValue, userCoinStack);
            }

            if (change > 0) throw new NotEnoughChangeException("Not enough change");

            return userCoinStack;
        }

        private void SelectCoins(ref int change, int faceValue, Stack<int> userCoinStack)
        {
            while (change >= faceValue)
            {
                // Are there enough coins of this value?
                if (!_machineCoinStack.RemoveCoin(faceValue)) break;

                change -= faceValue;
                userCoinStack.Push(faceValue);
            }
        }
    }
}
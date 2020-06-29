using System.Collections.Generic;

namespace Application.Contracts
{
    public interface IChangeService
    {
        Stack<int> CalculateChange(int change);
    }
}
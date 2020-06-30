using System;

namespace Domain.Exceptions
{
    public class InsufficientAmountException : Exception
    {
        public InsufficientAmountException(string message) : base(message)
        {
        }
    }
}
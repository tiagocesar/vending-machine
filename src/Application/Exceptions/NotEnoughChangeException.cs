using System;

namespace Application.Exceptions
{
    public class NotEnoughChangeException : Exception
    {
        public NotEnoughChangeException(string message) : base(message)
        {
        }
    }
}
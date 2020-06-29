using System;

namespace Domain.Exceptions
{
    public class ProductNotAvailableException : Exception
    {
        public ProductNotAvailableException(string message) : base(message)
        {
        }
    }
}
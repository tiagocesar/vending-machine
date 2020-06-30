using Domain.Exceptions;

namespace Domain
{
    public class Product
    {
        public int Code { get; }
        public string Name { get; }
        public int Price { get; }
        private int Quantity { get; set; }

        public Product(int code, string name, int price, int quantity)
        {
            Code = code;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public bool Sell(int paidAmount)
        {
            if (Quantity == 0) throw new ProductNotAvailableException("The product is depleted");
            if (paidAmount < Price)
                throw new InsufficientAmountException($"The product price is higher than the paid amount");

            Quantity -= 1;

            return true;
        }
    }
}
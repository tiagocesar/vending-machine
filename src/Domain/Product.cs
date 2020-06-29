using Domain.Exceptions;

namespace Domain
{
    public class Product
    {
        public int Code { get; }
        public string Name { get; }
        public int Price { get; }
        public int Quantity { get; private set; }
	
        public Product(int code, string name, int price, int quantity)
        {
            Code = code;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        protected internal void Sell()
        {
            if (Quantity == 0) throw new ProductNotAvailableException("The product is depleted");
		
            Quantity -= 1;
        }
    }
}
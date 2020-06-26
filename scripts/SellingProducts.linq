<Query Kind="Program" />

void Main()
{
	var products = new MachineProducts();
	var tea = products.Products.Where(x => x.Code == 1);
	var chai = products.Products.Where(x => x.Code == 5);
	
	tea.Dump("Beginning of the day - tea");
	chai.Dump("Beginning of the day - chai");
	
	products.Sell(1);
	products.Sell(5);
	
	tea.Dump("End of the day - tea");
	chai.Dump("End of the day - chai");

	products.Sell(5);
}

public class MachineProducts
{
	public List<ProductGrid> Products { get; } = new List<ProductGrid>();
	
	public MachineProducts()
	{
		// Seeding
		AddProduct(1, "Tea", 130, 10);
		AddProduct(2, "Espresso", 180, 20);
		AddProduct(3, "Juice", 180, 20);
		AddProduct(4, "Chicken soup", 180, 15);
		AddProduct(5, "Chai", 200, 1);
	}
	
	public void AddProduct(int code, string name, int price, int quantity)
	{
		if (Products.Any(x => x.Code == code)) throw new ArgumentException("The specified code is already assigned");
		
		Products.Add(new ProductGrid(code, new Product(name, price, quantity)));
	}
	
	public bool Sell(int code)
	{
		var product = Products.Single(x => x.Code == code).Product;
		
		product.Sell();
		
		return true;
	}
}

public class ProductGrid
{
	public int Code { get; }
	public Product Product { get; }
	
	public ProductGrid(int code, Product product)
	{
		Code = code;
		Product = product;
	}
}

public class Product
{
	public string Name { get; }
	public int Price { get; }
	public int Quantity { get; private set; }
	
	public Product(string name, int price, int quantity)
	{
		Name = name;
		Price = price;
		Quantity = quantity;
	}
	
	internal void Sell()
	{
		if (Quantity == 0) throw new ProductNotAvailableException("The product is depleted");
		
		Quantity -= 1;
	}
}

public class ProductNotAvailableException : Exception
{
	public ProductNotAvailableException(string message) : base(message)
	{
	}
}
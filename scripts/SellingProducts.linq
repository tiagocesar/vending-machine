<Query Kind="Program" />

void Main()
{
	var products = new ProductGrid();
	var tea = products.Products.Where(x => x.Code == 1);
	var chai = products.Products.Where(x => x.Code == 5);
	
	tea.Dump("Beginning of the day - tea");
	chai.Dump("Beginning of the day - chai");
	
	products.Sell(1);
	products.Sell(5);
	
	tea.Dump("End of the day - tea");
	chai.Dump("End of the day - chai");

	try
	{	        
		products.Sell(5);
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Exception of type {ex.GetType()} thrown: {ex.Message}");
	}
}

public class ProductGrid
{
	public List<Product> Products { get; } = new List<Product>();

	public ProductGrid()
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

		Products.Add(new Product(code, name, price, quantity));
	}

	public bool Sell(int code)
	{
		var product = Products.Single(x => x.Code == code);

		product.Sell();

		return true;
	}
}

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
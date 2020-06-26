<Query Kind="Program" />

void Main()
{
	var products = GetMachineProductsInfo();
	
	products.Dump();
}



public Dictionary<int, (string name, int price, int quantity)> GetMachineProductsInfo() =>
	new Dictionary<int, (string, int, int)>
	{
		{ 1, ("Tea", 130, 10) },
		{ 2, ("Espresso", 180, 20) },
		{ 3, ("Juice", 180, 20) },
		{ 4, ("Chicken soup", 180, 15) }
	};
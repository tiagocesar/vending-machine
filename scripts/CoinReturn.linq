void Main()
{
	var paidAmount = 200;
	var price = 130;
	var change = paidAmount - price;
	
	while (change > 0)
	{
		if (change % 100 != change)
		{
			change -= 100;			
			Console.WriteLine("Returned 1 euro coin");
		}

		if (change % 50 != change)
		{
			change -= 50;
			Console.WriteLine("Returned 50 cents coin");
		}

		if (change % 20 != change)
		{
			change -= 20;
			Console.WriteLine("Returned 20 cents coin");
		}

		if (change % 10 != change)
		{
			change -= 10;
			Console.WriteLine("Returned 10 cents coin");
		}
	}
}

// Stack of coins:

/*
VALUE		AMOUNT
10 cent		100
20 cent		100
50 cent		100
1 euro		100

Products

Tea (1.30 eur), 10 portions
Espresso (1.80 eur), 20 portions
Juice  (1.80 eur), 20 portions
Chicken soup (1.80 eur), 15 portions
*/

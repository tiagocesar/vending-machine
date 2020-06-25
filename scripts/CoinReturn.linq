<Query Kind="Program" />

void Main()
{
	var paidAmount = 200;
	var price = 130;
	var change = paidAmount - price;
	
	while (change > 0)
	{
		if (CalculateChange(ref change, 100) > 0)
		{
			Console.WriteLine("Returned 1 euro coin");
		}

		if (CalculateChange(ref change, 50) > 0)
		{
			Console.WriteLine("Returned 50 cents coin");
		}

		if (CalculateChange(ref change, 20) > 0)
		{
			Console.WriteLine("Returned 20 cents coin");
		}

		if (CalculateChange(ref change, 10) > 0)
		{
			Console.WriteLine("Returned 10 cents coin");
		}
	}
}

public int CalculateChange(ref int change, int faceValue)
{
	if (change % faceValue != change)
	{
		change -= faceValue;
		return faceValue;
	}
	
	return 0;
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

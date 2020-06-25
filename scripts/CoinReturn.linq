<Query Kind="Program" />

void Main()
{
	var paidAmount = 200;
	var price = 130;
	var change = paidAmount - price;

	var faceValues = new[] { 100, 50, 20, 10 };
	
	var results = new Stack<int>();

	foreach (var faceValue in faceValues)
	{
		CalculateChange(ref change, results, faceValue);
	}
	
	results.Dump();
}

public void CalculateChange(ref int change, Stack<int> results, int faceValue)
{
	while (change >= faceValue)
	{
		change -= faceValue;
		results.Push(faceValue);
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

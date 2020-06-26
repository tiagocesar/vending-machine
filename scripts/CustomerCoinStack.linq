<Query Kind="Program" />

void Main()
{
	var coinStack = new CustomerCoinStack();
	int totalAmount = 0;
	
	totalAmount = coinStack.Push(100);
	totalAmount = coinStack.Push(50);
	
	coinStack.Coins.Dump("Coins inserted by the customer");
	
	// Make the sell
	
	var machineCoins = GetMachineCoinInfo();
	
	machineCoins.Dump("Before adding the new coins");
	
	while(coinStack.TryPop(out var coin))
	{
		machineCoins[coin] += 1;
	}
	
	machineCoins.Dump("After adding the new coins");
}

public class CustomerCoinStack : Stack<int>
{
	public Stack<int> Coins { get; } = new Stack<int>();
	
	public new int Push(int coin)
	{
		Coins.Push(coin);
		
		return Coins.Sum();
	}
	
	public new bool TryPop(out int coin) => Coins.TryPop(out coin);
}

public Dictionary<int, int> GetMachineCoinInfo()
{
	var coinStack = new Dictionary<int, int>
	{
		{ 10, 1 },
		{ 20, 1 },
		{ 50, 1 },
		{ 100, 1 }
	};

	return coinStack;
}
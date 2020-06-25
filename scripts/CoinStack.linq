<Query Kind="Program" />

void Main()
{
	var coinStack = new CoinStack();
	
	coinStack.Push(100).Dump();
	coinStack.Push(50).Dump();
	coinStack.Push(20).Dump();
	coinStack.Push(10).Dump();
	
	coinStack.Coins.Dump();
}

public class CoinStack : Stack<int>
{
	public Stack<int> Coins { get; } = new Stack<int>();
	
	public new int Push(int coin)
	{
		Coins.Push(coin);
		
		return Coins.Sum();
	}
}
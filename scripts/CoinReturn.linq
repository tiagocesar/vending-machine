<Query Kind="Program">
  <NuGetReference>xunit</NuGetReference>
  <NuGetReference>xunit.core</NuGetReference>
  <NuGetReference>xunit.runner.utility</NuGetReference>
  <Namespace>Xunit</Namespace>
  <Namespace>Xunit.Runners</Namespace>
  <CopyLocal>true</CopyLocal>
</Query>

void Main()
{
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
	
	// Press F4, go to Advanced, then 
	// tick-mark "Copy all non-framework references to a single local folder"
	var me = Assembly.GetExecutingAssembly();
	var result = XunitRunner.Run(me);
	result.Dump("Xunit runner result");
}

[Theory]
[InlineData(120, 130, new int[0])]
[InlineData(130, 130, new int[0])]
[InlineData(140, 130, new[] { 10 })]
[InlineData(150, 130, new[] { 20 })]
[InlineData(180, 130, new[] { 50 })]
[InlineData(200, 130, new[] { 20, 50 })]
[InlineData(300, 130, new[] { 20, 50, 100 })]
[InlineData(170, 180, new int[0])]
[InlineData(180, 180, new int[0])]
[InlineData(190, 180, new[] { 10 })]
[InlineData(200, 180, new[] { 20 })]
[InlineData(250, 180, new[] { 20, 50 })]
[InlineData(500, 180, new[] { 20, 100, 100, 100 })]
public void TestCoinReturns(int paidAmount, int price, int[] expectedResult)
{
	var change = paidAmount - price;
	
	var actualResult = CalculateChange(change);
	
	Assert.Equal(expectedResult, actualResult.ToArray());
}

public Stack<int> CalculateChange(int change)
{
	var faceValues = new[] { 100, 50, 20, 10 };
	
	var results = new Stack<int>();

	foreach (var faceValue in faceValues)
	{
		SelectCoins(ref change, results, faceValue);
	}
	
	return results;
}

public void SelectCoins(ref int change, Stack<int> results, int faceValue)
{
	while (change >= faceValue)
	{
		change -= faceValue;
		results.Push(faceValue);
	}
}

public class XunitRunner
{
	private static readonly object sync = new object();
	private ManualResetEvent done;
	private int result = 0;
	private Assembly testAssembly = null;

	public static object Sync => sync;

	public XunitRunner(Assembly assembly)
	{
		if (assembly == null)
		{
			throw new ArgumentNullException("assembly");
		}

		this.testAssembly = assembly;
	}

	public static int Run(Assembly assembly, Action<AssemblyRunner> configureRunner = null)
	{
		var runner = new XunitRunner(assembly);
		return runner.Run(configureRunner);
	}

	public int Run(Action<AssemblyRunner> configureRunner = null)
	{
		var targetAssembly = GetTargetAssemblyFilename(this.testAssembly);

		using (var runner = AssemblyRunner.WithoutAppDomain(targetAssembly))
		{
			using (this.done = new ManualResetEvent(false))
			{
				runner.OnDiscoveryComplete = this.OnDiscoveryComplete;
				runner.OnExecutionComplete = this.OnExecutionComplete;
				runner.OnTestFailed = this.OnTestFailed;
				runner.OnTestSkipped = this.OnTestSkipped;

				configureRunner?.Invoke(runner);

				runner.Start();

				this.done.WaitOne();
			}

			return this.result;
		}
	}


	protected virtual void OnDiscoveryComplete(DiscoveryCompleteInfo info)
	{
		lock (sync)
			Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
	}

	protected virtual void OnExecutionComplete(ExecutionCompleteInfo info)
	{
		lock (sync)
			Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");

		this.done.Set();
	}

	protected virtual void OnTestFailed(TestFailedInfo info)
	{
		lock (sync)
		{
			Console.WriteLine("[FAIL] {0}: {1}", info.TestDisplayName, info.ExceptionMessage);

			if (info.ExceptionStackTrace != null)
				Console.WriteLine(info.ExceptionStackTrace);
		}

		result = 1;
	}

	protected virtual void OnTestSkipped(TestSkippedInfo info)
	{
		lock (sync)
		{
			Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);
		}
	}

	static string GetTargetAssemblyFilename(Assembly assembly)
	{
		var assemblyFilename = assembly.Location;

		var shadowFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		var xunitFolder = Path.GetDirectoryName(typeof(Xunit.Assert).Assembly.Location);

		if (shadowFolder != xunitFolder || Directory.GetFiles(shadowFolder, "xunit.execution.*.dll").Length == 0)
		{
#if NETFRAMEWORK
                string refText = "non-framework references";
#else
			string refText = "NuGet assemblies";
#endif

			//throw new InvalidOperationException($"Please enable the single folder option for {refText} (F4 -> Advanced).");
		}

		var targetAssembly = Path.Combine(shadowFolder, Path.GetFileName(assemblyFilename));

		//File.Copy(assemblyFilename, targetAssembly, true);

		return targetAssembly;
	}
}
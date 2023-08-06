using Xunit.Abstractions;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace HyperVector.Core.Tests
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	[RankColumn, MemoryDiagnoser]
	public class BenchmarkTests
	{
		private readonly ITestOutputHelper? _outputHelper;

		// The BenchmarkDotNet framework does not provide an injection
		public BenchmarkTests(ITestOutputHelper? outputHelper = null)
		{
			_outputHelper = outputHelper;
		}

		[Fact(Skip = "This method is time consuming")]
		public void StartBenchmarkTests()
		{
			var accumulationlogger = new AccumulationLogger();
			var manualConfig = ManualConfig.Create(DefaultConfig.Instance)
				.WithOptions(ConfigOptions.DisableOptimizationsValidator)
				.AddLogger(accumulationlogger);

			BenchmarkRunner.Run<BenchmarkTests>(manualConfig);
			if (_outputHelper != null)
				_outputHelper.WriteLine(accumulationlogger.GetLog());
		}

		[Fact, Benchmark]
		public void BenchmarkHalfArray()
		{
			var halfVector = DataVector<half>.GenerateBaseVector(4096, (half) 0.1f);

			if (_outputHelper != null)
			{
				_outputHelper.WriteLine
					($"Generated half base vector of size {halfVector.Size}");
			}
		}

		[Fact, Benchmark]
		public void BenchmarkFloatArray()
		{
			var floatVector = DataVector<float>.GenerateBaseVector(4096, 0.1f);

			if (_outputHelper != null)
			{
				_outputHelper.WriteLine
					($"Generated float base vector of size {floatVector.Size}");
			}
		}

		[Fact, Benchmark]
		public void BenchmarkDoubleArray()
		{
			var doubleVector = DataVector<double>.GenerateBaseVector(4096, 0.1);

			if (_outputHelper != null)
			{
				_outputHelper.WriteLine
					($"Generated double base vector of size {doubleVector.Size}");
			}
		}
	}
}

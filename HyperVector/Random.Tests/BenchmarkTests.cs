using System.Security.Cryptography;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace HyperVector.Random.Tests
{
	[RankColumn, MemoryDiagnoser]
	public class BenchmarkTests
	{
		private readonly ITestOutputHelper? _outputHelper;

		private SourceArray _sourceArray;
		private System.Random _systemRandom;

		// The BenchmarkDotNet framework does not provide an injection
		public BenchmarkTests(ITestOutputHelper? outputHelper = null)
		{
			_outputHelper = outputHelper;

			ulong randomSeed = SourceArray.GetRandomSeed();
			_sourceArray = new SourceArray(randomSeed);
			_systemRandom = new System.Random();
		}

		[Fact (Skip = "This method is time consuming")]
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
		public void BenchmarkSourceArray()
		{
			ulong randomValue = 0;
			for (int i = 0; i < 100; i++)
				randomValue ^= _sourceArray.NextUlong();

			if (_outputHelper != null)
			{
				_outputHelper.WriteLine
					($"Final SourceArray number:\n=====> {randomValue}");
			}
		}

		[Fact, Benchmark]
		public void BenchmarkSystemRandom()
		{
			long randomValue = 0;
			for (int i = 0; i < 100; i++)
				randomValue ^= _systemRandom.NextInt64();

			if (_outputHelper != null)
			{
				_outputHelper.WriteLine
					($"Final SystemRandom number:\n=====> {randomValue}");
			}
		}

		[Fact, Benchmark]
		public void BenchmarkCryptoRandom()
		{
			ulong randomValue = 0;
			byte[] currentValue;
			for (int i = 0; i < 100; i++)
			{
				currentValue = RandomNumberGenerator.GetBytes(8);
				randomValue ^= BitConverter.ToUInt64(currentValue);
			}

			if (_outputHelper != null)
			{
				_outputHelper.WriteLine
					($"Final CryptoRandom number:\n=====> {randomValue}");
			}
		}
	}
}

namespace HyperVector.Random.Tests
{
	public class SourceArrayTests
	{
		private readonly ITestOutputHelper _outputHelper;

		public SourceArrayTests(ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
		}

		[Fact]
		public void CheckStaticInstance()
		{
			var sourceArray = SourceArray.StaticInstance;
			Assert.NotNull(sourceArray);

			ulong randomValue1 = sourceArray.NextUlong();
			ulong randomValue2 = sourceArray.NextUlong();
			ulong randomValue3 = sourceArray.NextUlong();

			Assert.NotEqual(randomValue1, randomValue2);
			Assert.NotEqual(randomValue2, randomValue3);
			Assert.NotEqual(randomValue3, randomValue1);
		}

		[Fact]
		public void CheckRandomSeed()
		{
			ulong randomSeed = SourceArray.GetRandomSeed();
			var sourceArray = new SourceArray(randomSeed);
			Assert.NotNull(sourceArray);

			ulong randomValue1 = sourceArray.NextUlong();
			ulong randomValue2 = sourceArray.NextUlong();
			ulong randomValue3 = sourceArray.NextUlong();

			Assert.NotEqual(randomValue1, randomValue2);
			Assert.NotEqual(randomValue2, randomValue3);
			Assert.NotEqual(randomValue3, randomValue1);
		}

		[Fact]
		public void CheckBooleanValues()
		{
			int trueCounter = 0, falseCounter = 0;
			ulong randomSeed = SourceArray.GetRandomSeed();
			var sourceArray = new SourceArray(randomSeed);
			Assert.NotNull(sourceArray);

			for (int i = 0; i < 100; i++)
			{
				bool randomBool = sourceArray.NextBool();
				if (randomBool)
					trueCounter++;
				else
					falseCounter++;
			}

			_outputHelper.WriteLine($"True boolean counter: {trueCounter}");
			_outputHelper.WriteLine($"False boolean counter: {falseCounter}");
		}

		[Fact]
		public void FloatingPointNumbers()
		{
			float randomFloat; double randomDouble;
			var sourceArray = SourceArray.StaticInstance;
			Assert.NotNull(sourceArray);

			for (int i = 0; i < 10; i++)
			{
				randomFloat = sourceArray.NextFloat12();
				Assert.True(randomFloat >= 1.0f);
				Assert.True(randomFloat < 2.0f);

				randomFloat = sourceArray.NextFloat01();
				Assert.True(randomFloat >= 0.0f);
				Assert.True(randomFloat < 1.0f);
			}

			for (int i = 0; i < 10; i++)
			{
				randomDouble = sourceArray.NextDouble12();
				Assert.True(randomDouble >= 1.0);
				Assert.True(randomDouble < 2.0);

				randomDouble = sourceArray.NextDouble01();
				Assert.True(randomDouble >= 0.0);
				Assert.True(randomDouble < 1.0);
			}

			_outputHelper.WriteLine("Completed testing uniform numbers");
		}

		[Fact]
		public void HyperVectorNumbers()
		{
			float randomFloat; double randomDouble;
			var sourceArray = SourceArray.StaticInstance;
			Assert.NotNull(sourceArray);

			for (int i = 0; i < 10; i++)
			{
				randomFloat = sourceArray.NextUnitFloat();
				Assert.True(randomFloat < 1.0f);
				Assert.True(randomFloat > -1.0f);

				randomFloat = sourceArray.NextVectorFloat();
				Assert.True(randomFloat < 1.0f);
				Assert.True(randomFloat > -1.0f);
				Assert.True(randomFloat > 0.1f || randomFloat < -0.1f);
			}

			for (int i = 0; i < 10; i++)
			{
				randomDouble = sourceArray.NextUnitDouble();
				Assert.True(randomDouble < 1.0);
				Assert.True(randomDouble > -1.0);

				randomDouble = sourceArray.NextVectorDouble();
				Assert.True(randomDouble < 1.0);
				Assert.True(randomDouble > -1.0);
				Assert.True(randomDouble > 0.1 || randomDouble < -0.1);
			}

			_outputHelper.WriteLine("Completed testing hyper-vector numbers");
		}
	}
}

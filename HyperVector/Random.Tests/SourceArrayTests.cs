namespace HyperVector.Random.Tests
{
	public class SourceArrayTests
	{
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
		public void CheckInitialSeed()
		{
			var sourceArray = new SourceArray(1);
			Assert.NotNull(sourceArray);

			ulong randomValue1 = sourceArray.NextUlong();
			ulong randomValue2 = sourceArray.NextUlong();
			ulong randomValue3 = sourceArray.NextUlong();

			Assert.NotEqual(randomValue1, randomValue2);
			Assert.NotEqual(randomValue2, randomValue3);
			Assert.NotEqual(randomValue3, randomValue1);
		}

		[Fact]
		public void FloatingPointNumbers()
		{
			var sourceArray = SourceArray.StaticInstance;
			Assert.NotNull(sourceArray);

			for (int i = 0; i < 10; i++)
			{
				float randomFloat = sourceArray.NextFloat12();
				Assert.True(randomFloat >= 1.0f);
				Assert.True(randomFloat < 2.0f);

				double randomDouble = sourceArray.NextDouble12();
				Assert.True(randomDouble >= 1.0);
				Assert.True(randomDouble < 2.0);
			}
		}
	}
}

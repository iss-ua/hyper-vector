namespace HyperVector.Random.Tests
{
	public class SourceArrayTests
	{
		[Fact]
		public void CheckStaticInstance()
		{
			var sourceArray = SourceArray.StaticInstance;
			Assert.NotNull(sourceArray);
		}

		[Fact]
		public void CheckInitialSeed()
		{
			var sourceArray = new SourceArray(1);
			Assert.NotNull(sourceArray);
		}
	}
}

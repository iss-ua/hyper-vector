namespace HyperVector.Random.Tests
{
	public class SourceArrayTests
	{
		[Fact]
		public void CheckStaticContructor()
		{
			var sourceArray = SourceArray.StaticInstance;
			Assert.NotNull(sourceArray);
		}
	}
}

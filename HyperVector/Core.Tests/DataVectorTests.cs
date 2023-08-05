namespace HyperVector.Core.Tests
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	public class DataVectorTests
	{
		[Fact]
		public void InstantiationTests()
		{
			var halfVector = new DataVector<half>(4096);
			var floatVector = new DataVector<float>(4096);
			var doubleVector = new DataVector<double>(4096);
		}
	}
}

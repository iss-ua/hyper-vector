namespace HyperVector.Core.Tests
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	public class DataVectorTests
	{
		[Fact]
		public void VectorCreationTests()
		{
			var halfVector = new DataVector<half>(4096);
			var floatVector = new DataVector<float>(4096);
			var doubleVector = new DataVector<double>(4096);
		}

		[Fact]
		public void VectorGenerationTests()
		{
			var halfVector = DataVector<half>.GenerateBaseVector(4096, (half) 0.1f);
			var floatVector = DataVector<float>.GenerateBaseVector(4096, 0.1f);
			var doubleVector = DataVector<double>.GenerateBaseVector(4096, 0.1);
		}
	}
}

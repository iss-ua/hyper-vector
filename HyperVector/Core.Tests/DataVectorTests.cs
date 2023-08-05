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
			Assert.NotNull(halfVector);

			var floatVector = new DataVector<float>(4096);
			Assert.NotNull(floatVector);

			var doubleVector = new DataVector<double>(4096);
			Assert.NotNull(doubleVector);
		}

		[Fact]
		public void VectorGenerationTests()
		{
			var halfVector = DataVector<half>.GenerateBaseVector(4096, (half) 0.1f);
			Assert.NotNull(halfVector);

			var floatVector = DataVector<float>.GenerateBaseVector(4096, 0.1f);
			Assert.NotNull(floatVector);

			var doubleVector = DataVector<double>.GenerateBaseVector(4096, 0.1);
			Assert.NotNull(doubleVector);
		}
	}
}

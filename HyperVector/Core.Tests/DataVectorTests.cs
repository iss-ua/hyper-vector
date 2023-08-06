namespace HyperVector.Core.Tests
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	public class DataVectorTests
	{
		private readonly ITestOutputHelper _outputHelper;

		public DataVectorTests(ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
		}

		[Fact]
		public void VectorCreationTests()
		{
			var halfVector = new DataVector<half>(64);
			Assert.NotNull(halfVector);

			var floatVector = new DataVector<float>(64);
			Assert.NotNull(floatVector);

			var doubleVector = new DataVector<double>(64);
			Assert.NotNull(doubleVector);
		}

		[Fact]
		public void VectorGenerationTests()
		{
			var halfVector = DataVector<half>.GenerateBaseVector(64, (half) 0.1f);
			Assert.NotNull(halfVector);

			var floatVector = DataVector<float>.GenerateBaseVector(64, 0.1f);
			Assert.NotNull(floatVector);

			var doubleVector = DataVector<double>.GenerateBaseVector(64, 0.1);
			Assert.NotNull(doubleVector);
		}

		[Fact]
		public void HalfDotProductTests()
		{
			var leftVector = DataVector<half>.GenerateBaseVector(64, (half) 0.1);
			var rightVector = DataVector<half>.GenerateBaseVector(64, (half) 0.1);

			half leftNorm = leftVector.GetNorm();
			_outputHelper.WriteLine($"Left vector norm: {leftNorm}");
			half rightNorm = rightVector.GetNorm();
			_outputHelper.WriteLine($"Right vector norm: {rightNorm}");

			half staticProduct = DataVector<half>.GetDotProduct(leftVector, rightVector);
			half memberProduct = leftVector.GetDotProduct(rightVector);
			Assert.Equal(staticProduct, memberProduct);
			_outputHelper.WriteLine($"Vector dot product: {memberProduct}");

			half staticMetric = DataVector<half>.GetCosineMetric(leftVector, rightVector);
			half memberMetric = leftVector.GetCosineMetric(rightVector);
			Assert.Equal(staticMetric, memberMetric);
			_outputHelper.WriteLine($"Vector cosine metric: {memberMetric}");
		}

		[Fact]
		public void FloatDotProductTests()
		{
			var leftVector = DataVector<float>.GenerateBaseVector(64, 0.1f);
			var rightVector = DataVector<float>.GenerateBaseVector(64, 0.1f);

			float leftNorm = leftVector.GetNorm();
			_outputHelper.WriteLine($"Left vector norm: {leftNorm}");
			float rightNorm = rightVector.GetNorm();
			_outputHelper.WriteLine($"Right vector norm: {rightNorm}");

			float staticProduct = DataVector<float>.GetDotProduct(leftVector, rightVector);
			float memberProduct = leftVector.GetDotProduct(rightVector);
			Assert.Equal(staticProduct, memberProduct);
			_outputHelper.WriteLine($"Vector dot product: {memberProduct}");

			float staticMetric = DataVector<float>.GetCosineMetric(leftVector, rightVector);
			float memberMetric = leftVector.GetCosineMetric(rightVector);
			Assert.Equal(staticMetric, memberMetric);
			_outputHelper.WriteLine($"Vector cosine metric: {memberMetric}");
		}

		[Fact]
		public void DoubleDotProductTests()
		{
			var leftVector = DataVector<double>.GenerateBaseVector(64, 0.1);
			var rightVector = DataVector<double>.GenerateBaseVector(64, 0.1);

			double leftNorm = leftVector.GetNorm();
			_outputHelper.WriteLine($"Left vector norm: {leftNorm}");
			double rightNorm = rightVector.GetNorm();
			_outputHelper.WriteLine($"Right vector norm: {rightNorm}");

			double staticProduct = DataVector<double>.GetDotProduct(leftVector, rightVector);
			double memberProduct = leftVector.GetDotProduct(rightVector);
			Assert.Equal(staticProduct, memberProduct);
			_outputHelper.WriteLine($"Vector dot product: {memberProduct}");

			double staticMetric = DataVector<double>.GetCosineMetric(leftVector, rightVector);
			double memberMetric = leftVector.GetCosineMetric(rightVector);
			Assert.Equal(staticMetric, memberMetric);
			_outputHelper.WriteLine($"Vector cosine metric: {memberMetric}");
		}
	}
}

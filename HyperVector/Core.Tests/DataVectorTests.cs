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
		public void VectorGenerationTests()
		{
			var halfVector = new DataVector<half>(8);
			Assert.NotNull(halfVector);

			halfVector = DataVector<half>.GenerateBaseVector(8, (half) 0.1f);
			var halfVectorCopy = halfVector.Copy();
			Assert.True(halfVector.Equals(halfVectorCopy));

			var floatVector = new DataVector<float>(8);
			Assert.NotNull(floatVector);

			floatVector = DataVector<float>.GenerateBaseVector(8, 0.1f);
			var floatVectorCopy = floatVector.Copy();
			Assert.True(floatVector.Equals(floatVectorCopy));

			var doubleVector = new DataVector<double>(8);
			Assert.NotNull(doubleVector);

			doubleVector = DataVector<double>.GenerateBaseVector(8, 0.1);
			var doubleVectorCopy = doubleVector.Copy();
			Assert.True(doubleVector.Equals(doubleVectorCopy));
		}

		[Fact]
		public void HalfDotProductTests()
		{
			var leftVector = DataVector<half>.GenerateBaseVector(8, (half) 0.1);
			var rightVector = DataVector<half>.GenerateBaseVector(8, (half) 0.1);

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
			var leftVector = DataVector<float>.GenerateBaseVector(8, 0.1f);
			var rightVector = DataVector<float>.GenerateBaseVector(8, 0.1f);

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
			var leftVector = DataVector<double>.GenerateBaseVector(8, 0.1);
			var rightVector = DataVector<double>.GenerateBaseVector(8, 0.1);

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

		[Fact]
		public void HalfVectorRotationTests()
		{
			var testVector = DataVector<half>.GenerateBaseVector(8, (half) 0.1);
			var leftVector = testVector.RotateLeft();
			Assert.False(leftVector.Equals(testVector));
			var rightVector = leftVector.RotateRight();
			Assert.True(rightVector.Equals(testVector));

			var copyVector = testVector.Copy();
			copyVector.RotateRightInPlace();
			Assert.False(copyVector.Equals(testVector));
			copyVector.RotateLeftInPlace();
			Assert.True(copyVector.Equals(testVector));
		}

		[Fact]
		public void FloatVectorRotationTests()
		{
			var testVector = DataVector<float>.GenerateBaseVector(8, 0.1f);
			var leftVector = testVector.RotateLeft();
			Assert.False(leftVector.Equals(testVector));
			var rightVector = leftVector.RotateRight();
			Assert.True(rightVector.Equals(testVector));

			var copyVector = testVector.Copy();
			copyVector.RotateRightInPlace();
			Assert.False(copyVector.Equals(testVector));
			copyVector.RotateLeftInPlace();
			Assert.True(copyVector.Equals(testVector));
		}

		[Fact]
		public void DoubleVectorRotationTests()
		{
			var testVector = DataVector<double>.GenerateBaseVector(8, 0.1);
			var leftVector = testVector.RotateLeft();
			Assert.False(leftVector.Equals(testVector));
			var rightVector = leftVector.RotateRight();
			Assert.True(rightVector.Equals(testVector));

			var copyVector = testVector.Copy();
			copyVector.RotateRightInPlace();
			Assert.False(copyVector.Equals(testVector));
			copyVector.RotateLeftInPlace();
			Assert.True(copyVector.Equals(testVector));
		}
	}
}

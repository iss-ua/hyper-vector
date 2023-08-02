namespace HyperVector.Random.Tests
{
	public class BinaryHelperTests
	{
		[Fact]
		public void CheckOnesCounters()
		{
			int onesCounter = 0;

			onesCounter = BinaryHelper.GetNumberOfOnes((byte) 0);
			Assert.Equal(0, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((byte) 0xFF);
			Assert.Equal(8, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((ushort) 0);
			Assert.Equal(0, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((ushort) 0xFFFF);
			Assert.Equal(16, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((uint) 0);
			Assert.Equal(0, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((uint) 0xFFFFFFFF);
			Assert.Equal(32, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((ulong) 0);
			Assert.Equal(0, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((ulong) 0xFFFFFFFFFFFFFFFF);
			Assert.Equal(64, onesCounter);

			onesCounter = BinaryHelper.GetNumberOfOnes((ulong) 0x0123456789ABCDEF);
			Assert.Equal(32, onesCounter);
		}

		[Fact]
		public void CheckUlintRotation()
		{
			uint currentValue = 0x01234567U, rotatedValue = 0;

			rotatedValue = BinaryHelper.RotateLeft(currentValue, 0);
			Assert.Equal(0x01234567U, rotatedValue);

			rotatedValue = BinaryHelper.RotateLeft(currentValue, 4);
			Assert.Equal(0x12345670U, rotatedValue);

			rotatedValue = BinaryHelper.RotateLeft(currentValue, 8);
			Assert.Equal(0x23456701U, rotatedValue);

			rotatedValue = BinaryHelper.RotateLeft(currentValue);
			rotatedValue = BinaryHelper.RotateLeft(rotatedValue);
			rotatedValue = BinaryHelper.RotateLeft(rotatedValue);
			rotatedValue = BinaryHelper.RotateLeft(rotatedValue);
			Assert.Equal(0x12345670U, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue, 0);
			Assert.Equal(0x01234567U, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue, 4);
			Assert.Equal(0x70123456U, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue, 8);
			Assert.Equal(0x67012345U, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue);
			rotatedValue = BinaryHelper.RotateRight(rotatedValue);
			rotatedValue = BinaryHelper.RotateRight(rotatedValue);
			rotatedValue = BinaryHelper.RotateRight(rotatedValue);
			Assert.Equal(0x70123456U, rotatedValue);
		}

		[Fact]
		public void CheckUlongRotation()
		{
			ulong currentValue = 0x0123456789ABCDEFUL, rotatedValue = 0;

			rotatedValue = BinaryHelper.RotateLeft(currentValue, 0);
			Assert.Equal(0x0123456789ABCDEFUL, rotatedValue);

			rotatedValue = BinaryHelper.RotateLeft(currentValue, 4);
			Assert.Equal(0x123456789ABCDEF0UL, rotatedValue);

			rotatedValue = BinaryHelper.RotateLeft(currentValue, 8);
			Assert.Equal(0x23456789ABCDEF01UL, rotatedValue);

			rotatedValue = BinaryHelper.RotateLeft(currentValue);
			rotatedValue = BinaryHelper.RotateLeft(rotatedValue);
			rotatedValue = BinaryHelper.RotateLeft(rotatedValue);
			rotatedValue = BinaryHelper.RotateLeft(rotatedValue);
			Assert.Equal(0x123456789ABCDEF0UL, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue, 0);
			Assert.Equal(0x0123456789ABCDEFUL, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue, 4);
			Assert.Equal(0xF0123456789ABCDEUL, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue, 8);
			Assert.Equal(0xEF0123456789ABCDUL, rotatedValue);

			rotatedValue = BinaryHelper.RotateRight(currentValue);
			rotatedValue = BinaryHelper.RotateRight(rotatedValue);
			rotatedValue = BinaryHelper.RotateRight(rotatedValue);
			rotatedValue = BinaryHelper.RotateRight(rotatedValue);
			Assert.Equal(0xF0123456789ABCDEUL, rotatedValue);
		}
	}
}

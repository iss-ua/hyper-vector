using System;

namespace HyperVector.Random
{
	public static class BinaryHelper
	{
		private static int[] _onesInByte = new int[256];

		static BinaryHelper()
		{
			for (int i = 0; i < 256; i++)
			{
				int onesCounter = 0;

				if ((i & 0b00000001) > 0) onesCounter++;
				if ((i & 0b00000010) > 0) onesCounter++;
				if ((i & 0b00000100) > 0) onesCounter++;
				if ((i & 0b00001000) > 0) onesCounter++;
				if ((i & 0b00010000) > 0) onesCounter++;
				if ((i & 0b00100000) > 0) onesCounter++;
				if ((i & 0b01000000) > 0) onesCounter++;
				if ((i & 0b10000000) > 0) onesCounter++;

				_onesInByte[i] = onesCounter;
			}
		}

		public static int GetNumberOfOnes(byte inputNumber)
		{
			return _onesInByte[inputNumber];
		}

		public static int GetNumberOfOnes(ushort inputNumber)
		{
			byte higherByte = (byte) (inputNumber >> 8);
			byte lowerByte = (byte) (inputNumber & 0xff);
			return GetNumberOfOnes(higherByte) + GetNumberOfOnes(lowerByte);
		}

		public static int GetNumberOfOnes(uint inputNumber)
		{
			ushort higherUshort = (ushort) (inputNumber >> 16);
			ushort lowerUshort = (ushort) (inputNumber & 0xffff);
			return GetNumberOfOnes(higherUshort) + GetNumberOfOnes(lowerUshort);
		}

		public static int GetNumberOfOnes(ulong inputNumber)
		{
			uint higherUint = (uint) (inputNumber >> 32);
			uint lowerUint = (uint) (inputNumber & 0xffffffff);
			return GetNumberOfOnes(higherUint) + GetNumberOfOnes(lowerUint);
		}

		public static uint RotateLeft(uint inputNumber)
		{
			return (inputNumber << 1) | (inputNumber >> 31);
		}

		public static uint RotateRight(uint inputNumber)
		{
			return (inputNumber >> 1) | (inputNumber << 31);
		}

		public static uint RotateLeft(uint inputNumber, int shiftBits)
		{
			return (inputNumber << shiftBits) | (inputNumber >> (32 - shiftBits));
		}

		public static uint RotateRight(uint inputNumber, int shiftBits)
		{
			return (inputNumber >> shiftBits) | (inputNumber << (32 - shiftBits));
		}

		public static ulong RotateLeft(ulong inputNumber)
		{
			return (inputNumber << 1) | (inputNumber >> 63);
		}

		public static ulong RotateRight(ulong inputNumber)
		{
			return (inputNumber >> 1) | (inputNumber << 63);
		}

		public static ulong RotateLeft(ulong inputNumber, int shiftBits)
		{
			return (inputNumber << shiftBits) | (inputNumber >> (64 - shiftBits));
		}

		public static ulong RotateRight(ulong inputNumber, int shiftBits)
		{
			return (inputNumber >> shiftBits) | (inputNumber << (64 - shiftBits));
		}
	}
}

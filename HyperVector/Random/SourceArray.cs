using System;
using System.Diagnostics;

namespace HyperVector.Random
{
	/// <summary>
	/// This class is not thread safe
	/// </summary>
	public class SourceArray
	{
		private const int _arraySize = 37;

		private int _currentIndex = 0;
		private ulong[] _sourceArray = new ulong[_arraySize];

		private static SourceArray _staticInstance = null;

		public static SourceArray StaticInstance
		{
			get
			{
				if (_staticInstance == null)
					_staticInstance = new SourceArray();
				return _staticInstance;
			}
		}

		public SourceArray(ulong initialSeed = 0)
		{
			// 64-bit starting number that is built of primes
			// and has exactly 32 ones in the represenatation

			ulong currentValue = 2654435687UL * 2654435711UL;
			Debug.Assert(BinaryHelper.GetNumberOfOnes(currentValue) == 32);

			int shiftBits = (int) initialSeed & 0x000000000000003f;
			currentValue = BinaryHelper.RotateRight(currentValue, shiftBits);
			currentValue ^= initialSeed;

			for (int i = 0; i < _arraySize; i++)
			{
				_sourceArray[i] = currentValue;
				currentValue ^= BinaryHelper.RotateRight(currentValue, 23);
			}
		}

		public byte NextByte()
		{
			return (byte) (NextUlong() & 0xFF);
		}

		public ushort NextUshort()
		{
			return (ushort) (NextUlong() & 0xFFFF);
		}

		public uint NextUint()
		{
			return (uint) (NextUlong() & 0xFFFFFFFF);
		}

		public ulong NextUlong()
		{
			ulong currentValue = _sourceArray[_currentIndex];

			int nextIndex = _currentIndex + 1;
			if (nextIndex > _arraySize - 1)
				nextIndex = 0;

			ulong shaffleValue = _sourceArray[nextIndex];
			 shaffleValue = BinaryHelper.RotateRight(shaffleValue);
			_sourceArray[_currentIndex] ^= shaffleValue;

			_currentIndex = nextIndex;
			return currentValue;
		}

		/// <returns>Random floating-point number in the range [1, 2)</returns>
		public unsafe float NextFloat12()
		{
			uint floatPattern = NextUint();
			floatPattern |= 0b0_01111111_00000000000000000000000;
			floatPattern &= 0b0_01111111_11111111111111111111111;
			return *((float*) &floatPattern);
		}

		/// <returns>Random floating-point number in the range [1, 2)</returns>
		public unsafe double NextDouble12()
		{
			ulong doublePattern = NextUlong();
			doublePattern |= 0b0_01111111111_0000000000000000000000000000000000000000000000000000;
			doublePattern &= 0b0_01111111111_1111111111111111111111111111111111111111111111111111;
			return *((double*) &doublePattern);
		}
	}
}

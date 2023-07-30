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

			int shiftBits = (int) initialSeed & 0x0000003f;
			currentValue = BinaryHelper.RotateRight(currentValue, shiftBits);
			currentValue ^= initialSeed;

			for (int i = 0; i < _arraySize; i++)
			{
				_sourceArray[i] = currentValue;
				currentValue ^= BinaryHelper.RotateRight(currentValue, 23);
			}
		}
	}
}

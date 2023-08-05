using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace HyperVector.Random
{
	/// <summary>
	/// This class can be used to generate integer and floating point
	/// random numbers. It can be instantiated or can be used as static
	/// instance. This implementation is not thread safe.
	/// </summary>
	public class SourceArray
	{
		private const uint _floatOrMask = 0b0_01111111_00000000000000000000000;
		private const uint _floatAndMask = 0b0_01111111_11111111111111111111111;
		private const uint _floatSignMask = 0b1_00000000_00000000000000000000000;

		private const ulong _doubleOrMask = 0b0_01111111111_0000000000000000000000000000000000000000000000000000;
		private const ulong _doubleAndMask = 0b0_01111111111_1111111111111111111111111111111111111111111111111111;
		private const ulong _doubleSignMask = 0b1_00000000000_0000000000000000000000000000000000000000000000000000;

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

		/// <returns>Random boolean value with uniform distribution</returns>
		public bool NextBool()
		{
			return ((NextUlong() & 0b1) > 0);
		}

		/// <returns>Random unsigned 8-bit value with uniform distribution</returns>
		public byte NextByte()
		{
			return (byte) (NextUlong() & 0xFF);
		}

		/// <returns>Random unsigned 16-bit value with uniform distribution</returns>
		public ushort NextUshort()
		{
			return (ushort) (NextUlong() & 0xFFFF);
		}

		/// <returns>Random unsigned 32-bit value with uniform distribution</returns>
		public uint NextUint()
		{
			return (uint) (NextUlong() & 0xFFFFFFFF);
		}

		/// <returns>Random unsigned 64-bit value with uniform distribution</returns>
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

		/// <returns>Random floating-point number in the range [0, 1)</returns>
		public float NextFloat01()
		{
			return NextFloat12() - 1.0f;
		}

		/// <returns>Random floating-point number in the range [1, 2)</returns>
		public unsafe float NextFloat12()
		{
			uint floatPattern = NextUint();
			floatPattern |= _floatOrMask;
			floatPattern &= _floatAndMask;
			return *((float*) &floatPattern);
		}

		/// <returns>Random floating-point number in the range (-1, 1)</returns>
		public unsafe float NextUnitFloat()
		{
			uint floatPattern = NextUint();
			floatPattern |= _floatOrMask;
			floatPattern &= (_floatAndMask | _floatSignMask);

			if ((floatPattern & _floatSignMask) > 0)
				return *((float*) &floatPattern) + 1.0f; // Negative value
			else
				return *((float*) &floatPattern) - 1.0f; // Positive value
		}

		/// <summary>
		/// The method returns floating point random number for the use in hyper-vector
		/// representation. The parameter epsilonValue can be in range [0.1, 0.5]
		/// </summary>
		/// <returns>Random floating-point number in the range (-1, 1) excluding [-ε, ε]</returns>
		public float NextVectorFloat(float epsilonValue = 0.1f)
		{
			Contract.Requires(epsilonValue >= 0.1f && epsilonValue <= 0.5f);

			float unitFloat = NextUnitFloat();
			if (unitFloat > epsilonValue)
				return unitFloat;
			if (unitFloat < -epsilonValue)
				return unitFloat;
			return NextVectorFloat(epsilonValue);
		}

		/// <returns>Random floating-point number in the range [0, 1)</returns>
		public double NextDouble01()
		{
			return NextDouble12() - 1.0;
		}

		/// <returns>Random floating-point number in the range [1, 2)</returns>
		public unsafe double NextDouble12()
		{
			ulong doublePattern = NextUlong();
			doublePattern |= _doubleOrMask;
			doublePattern &= _doubleAndMask;
			return *((double*) &doublePattern);
		}

		/// <returns>Random floating-point number in the range (-1, 1)</returns>
		public unsafe double NextUnitDouble()
		{
			ulong doublePattern = NextUlong();
			doublePattern |= _doubleOrMask;
			doublePattern &= (_doubleAndMask | _doubleSignMask);

			if ((doublePattern & _doubleSignMask) > 0)
				return *((double*) &doublePattern) + 1.0; // Negative value
			else
				return *((double*) &doublePattern) - 1.0; // Positive value
		}

		/// <summary>
		/// The method returns floating point random number for the use in hyper-vector
		/// representation. The parameter epsilonValue can be in range [0.1, 0.5]
		/// </summary>
		/// <returns>Random floating-point number in the range (-1, 1) excluding [-ε, ε]</returns>
		public double NextVectorDouble(double epsilonValue = 0.1)
		{
			Contract.Requires(epsilonValue >= 0.1 && epsilonValue <= 0.5);

			double unitDouble = NextUnitDouble();
			if (unitDouble > epsilonValue)
				return unitDouble;
			if (unitDouble < -epsilonValue)
				return unitDouble;
			return NextVectorDouble(epsilonValue);
		}
	}
}

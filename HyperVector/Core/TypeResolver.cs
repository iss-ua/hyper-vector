using System;
using System.Numerics;

using HyperVector.Random;

namespace HyperVector.Core
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	public interface ITypeResolver<T> where T : IFloatingPoint<T>
	{
		DataVector<T> GenerateBaseVector(int vectorSize, T zeroDelta);
	}

	public class TypeResolver :
		ITypeResolver<half>, ITypeResolver<float>, ITypeResolver<double>
	{
		private static SourceArray _sourceArray = SourceArray.StaticInstance;

		private static TypeResolver _staticInstance = null;

		public static TypeResolver StaticInstance
		{
			get
			{
				if (_staticInstance == null)
					_staticInstance = new TypeResolver();
				return _staticInstance;
			}
		}

		public DataVector<half> GenerateBaseVector(int vectorSize, half zeroDelta /* = 0.1 */)
		{
			if (zeroDelta < (half) 0.1f || zeroDelta > (half) 0.5f)
			{
				throw new ArgumentOutOfRangeException
					(nameof(zeroDelta), "Should be in range [0.1, 0.5]");
			}

			var baseVector = new DataVector<half>(vectorSize);

			for (int i = 0; i < vectorSize; i++)
			{
				half randomValue = _sourceArray.NextVectorHalf(zeroDelta);
				baseVector._presentation[i] = randomValue;
			}
			return baseVector;
		}

		public DataVector<float> GenerateBaseVector(int vectorSize, float zeroDelta = 0.1f)
		{
			if (zeroDelta < 0.1f || zeroDelta > 0.5f)
			{
				throw new ArgumentOutOfRangeException
					(nameof(zeroDelta), "Should be in range [0.1, 0.5]");
			}

			var baseVector = new DataVector<float>(vectorSize);

			for (int i = 0; i < vectorSize; i++)
			{
				float randomValue = _sourceArray.NextVectorFloat(zeroDelta);
				baseVector._presentation[i] = randomValue;
			}
			return baseVector;
		}

		public DataVector<double> GenerateBaseVector(int vectorSize, double zeroDelta = 0.1)
		{
			if (zeroDelta < 0.1 || zeroDelta > 0.5)
			{
				throw new ArgumentOutOfRangeException
					(nameof(zeroDelta), "Should be in range [0.1, 0.5]");
			}

			var baseVector = new DataVector<double>(vectorSize);

			for (int i = 0; i < vectorSize; i++)
			{
				double randomValue = _sourceArray.NextVectorDouble(zeroDelta);
				baseVector._presentation[i] = randomValue;
			}
			return baseVector;
		}
	}
}

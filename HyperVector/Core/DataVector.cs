using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace HyperVector.Core
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	public class DataVector<T>
		where T : IFloatingPoint<T>, IFloatingPointIeee754<T>
	{
		internal int _vectorSize;
		internal T[] _presentation;

		public int Size => _vectorSize;

		public T this[int index] => _presentation[index];

		public DataVector(int vectorSize)
		{
			_vectorSize = vectorSize;
			_presentation = new T[vectorSize];
		}

		/// <summary>
		/// The generic version of method to generate random base vector.
		/// </summary>
		/// <typeparam name="T">Either half, float or double</typeparam>
		/// <param name="zeroDelta">Should be in range [0.1, 0.5]</param>
		/// <returns>The base representation vector.</returns>
		public static DataVector<T> GenerateBaseVector(int vectorSize, T zeroDelta)
		{
			var typeResolver = TypeResolver.StaticInstance as ITypeResolver<T>;
			if (typeResolver != null)
				return typeResolver.GenerateBaseVector(vectorSize, zeroDelta);

			throw new NotImplementedException();
		}

		public T GetNorm()
		{
			T sumOfSquares = T.Zero;
			for (int i = 0; i < _vectorSize; i++)
				sumOfSquares += _presentation[i] * _presentation[i];

			return T.Sqrt(sumOfSquares);
		}

		public T GetDotProduct(DataVector<T> rightVector)
		{
			if (_vectorSize != rightVector._vectorSize)
			{
				throw new ArgumentException
					("The vector sizes for left and right operands should match");
			}

			T dotProduct = T.Zero;
			for (int i = 0; i < _vectorSize; i++)
				dotProduct += _presentation[i] * rightVector[i];
			return dotProduct;
		}

		public static T GetDotProduct
			(DataVector<T> leftVector, DataVector<T> rightVector)
		{
			return leftVector.GetDotProduct(rightVector);
		}

		public T GetCosineMetric(DataVector<T> rightVector)
		{
			T leftNorm = GetNorm();
			T rightNorm = rightVector.GetNorm();
			T dotProduct = GetDotProduct(rightVector);
			T cosineMetric = dotProduct / (leftNorm * rightNorm);
			return cosineMetric;
		}

		public static T GetCosineMetric
			(DataVector<T> leftVector, DataVector<T> rightVector)
		{
			return leftVector.GetCosineMetric(rightVector);
		}

		public DataVector<T> Multiply(DataVector<T> rightVector)
		{
			if (_vectorSize != rightVector._vectorSize)
			{
				throw new ArgumentException
					("The vector sizes for left and right operands should match");
			}

			var multiplyVector = new DataVector<T>(_vectorSize);
			for (int i = 0; i < _vectorSize; i++)
				multiplyVector._presentation[i] = _presentation[i] * rightVector[i];
			return multiplyVector;
		}

		public static DataVector<T> Multiply
			(DataVector<T> leftVector, DataVector<T> rightVector)
		{
			return leftVector.Multiply(rightVector);
		}

		public static DataVector<T> Aggregate
			(IEnumerable<DataVector<T>> dataVectors)
		{
			if (!dataVectors.Any())
			{
				throw new ArgumentException
					("The aggregation for empty collection is not allowed");
			}

			int vectorCounter = 0;
			DataVector<T> aggregateVector = null;

			foreach (var dataVector in dataVectors)
			{
				if (aggregateVector == null)
					aggregateVector = new DataVector<T>(dataVector.Size);

				if (aggregateVector._vectorSize != dataVector.Size)
				{
					throw new ArgumentException
						("The vector sizes should be consistent within collection");
				}

				int localSize = aggregateVector._vectorSize;
				for (int i = 0; i < localSize; i++)
					aggregateVector._presentation[i] += dataVector[i];
				vectorCounter++;
			}

			T normalizer = T.One / T.CreateChecked(vectorCounter);
			int vectorSize = aggregateVector._vectorSize;
			for (int i = 0; i < vectorSize; i++)
				aggregateVector._presentation[i] *= normalizer;
			return aggregateVector;
		}
	}
}

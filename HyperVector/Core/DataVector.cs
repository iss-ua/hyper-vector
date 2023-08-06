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
			if (vectorSize < 1)
			{
				throw new ArgumentException
					("The data vector size cannot be less than 1");
			}

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

		public DataVector<T> MultiplyInPlace(DataVector<T> rightVector)
		{
			if (_vectorSize != rightVector._vectorSize)
			{
				throw new ArgumentException
					("The vector sizes for left and right operands should match");
			}

			for (int i = 0; i < _vectorSize; i++)
				_presentation[i] *= rightVector[i];
			return this;
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

		public DataVector<T> AggregateInPlace
			(IEnumerable<DataVector<T>> dataVectors)
		{
			if (!dataVectors.Any())
			{
				throw new ArgumentException
					("The aggregation for empty collection is not allowed");
			}

			int vectorCounter = 0;
			for (int i = 0; i < _vectorSize; i++)
				_presentation[i] = T.Zero;

			foreach (var dataVector in dataVectors)
			{
				if (_vectorSize != dataVector.Size)
				{
					throw new ArgumentException
						("The input vector sizes should match the current vector");
				}

				for (int i = 0; i < _vectorSize; i++)
					_presentation[i] += dataVector[i];
				vectorCounter++;
			}

			T normalizer = T.One / T.CreateChecked(vectorCounter);
			for (int i = 0; i < _vectorSize; i++)
				_presentation[i] *= normalizer;
			return this;
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

		public DataVector<T> RotateLeftInPlace()
		{
			T firstElement = _presentation[0];
			for (int i = 1; i < _vectorSize; i++)
				_presentation[i - 1] = _presentation[i];

			_presentation[_vectorSize - 1] = firstElement;
			return this;
		}

		public DataVector<T> RotateLeft()
		{
			var rotatedVector = new DataVector<T>(_vectorSize);
			for (int i = 1; i < _vectorSize; i++)
				rotatedVector._presentation[i - 1] = _presentation[i];

			rotatedVector._presentation[_vectorSize - 1] = _presentation[0];
			return rotatedVector;
		}

		public DataVector<T> RotateRightInPlace()
		{
			T lastElement = _presentation[_vectorSize - 1];
			for (int i = _vectorSize - 1; i > 0; i--)
				_presentation[i] = _presentation[i - 1];

			_presentation[0] = lastElement;
			return this;
		}

		public DataVector<T> RotateRight()
		{
			var rotatedVector = new DataVector<T>(_vectorSize);
			for (int i = _vectorSize - 1; i > 0; i--)
				rotatedVector._presentation[i] = _presentation[i - 1];

			rotatedVector._presentation[0] = _presentation[_vectorSize - 1];
			return rotatedVector;
		}
	}
}

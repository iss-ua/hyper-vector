using System;
using System.Numerics;

using HyperVector.Random;

namespace HyperVector.Core
{
	public class DataVector<T> where T : IFloatingPoint<T>
	{
		private static SourceArray _sourceArray = SourceArray.StaticInstance;

		private int _vectorSize;
		private T[] _presentation;

		public int VectorSize => _vectorSize;

		public T this[int index] => _presentation[index];

		public DataVector(int vectorSize)
		{
			_vectorSize = vectorSize;
			_presentation = new T[vectorSize];
		}

		public static DataVector<T> GenerateBaseVector(int vectorSize, T zeroDelta)
		{
			var baseVector = new DataVector<T>(vectorSize);

			for (int i = 0; i < vectorSize; i++)
			{
				T randomValue = _sourceArray.NextVectorValue<T>(zeroDelta);
				baseVector._presentation[i] = randomValue;
			}
			return baseVector;
		}
	}
}

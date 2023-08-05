using System;
using System.Numerics;

namespace HyperVector.Core
{
	public class DataVector<T> where T : IFloatingPoint<T>
	{
		private int _vectorSize;
		private T[] _dataVector;

		public int VectorSize => _vectorSize;

		public T this[int index] => _dataVector[index];

		public DataVector(int vectorSize)
		{
			_vectorSize = vectorSize;
			_dataVector = new T[vectorSize];
		}
	}
}

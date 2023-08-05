using System;
using System.Numerics;

namespace HyperVector.Core
{
#pragma warning disable CS8981
	using half = System.Half;
#pragma warning restore

	public class DataVector<T> where T : IFloatingPoint<T>
	{
		internal int _vectorSize;
		internal T[] _presentation;

		public int VectorSize => _vectorSize;

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
	}
}

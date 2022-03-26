using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FpAssociativity
{
	internal static class Demo
	{
		const int NUM_TESTS = 5;

		/// <summary>
		/// Performs five tests, running <paramref name="size"/> number of
		/// operations, with an <paramref name="offset"/> away from zero.
		/// </summary>
		/// <param name="size">Number of operations to run.</param>
		/// <param name="offset">Offset from 0.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// If either <paramref name="size"/> is less than 1
		/// or <paramref name="offset"/> is not an acceptable value.
		/// </exception>
		public static void Run(int size, double offset)
		{
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException(
					nameof(size),
					size,
					"Size must be greater than 1."
				);
			}
			if (
				double.IsNaN(offset)
				|| offset == double.PositiveInfinity
				|| offset == double.NegativeInfinity
				)
			{
				throw new ArgumentOutOfRangeException(
					nameof(offset),
					offset,
					"Offset must be valid."
				);
			}

			// Perform even number of operations.
			if (size % 2 == 1) size++;
			Random randomProvider = new();

			Console.Clear();
			Console.WriteLine(
				$"Running tests (size: {size}, offset: {offset}){Environment.NewLine}"
			);


			double[] results = new double[NUM_TESTS];
			double[] seed = new double[size];

			// Generate values.
			for (int i = 0; i < size; i += 2)
			{
				double randomValue = randomProvider.NextDouble();
				seed[i] = offset + randomValue;
				seed[i + 1] = -(offset + randomValue);
			}

			// Run accumulations.
			for (int i = 0; i < NUM_TESTS; i++)
			{
				double[] values = FisherYates(randomProvider, seed);
				double total = offset;
				for (int k = 0; k < size; k += 2)
				{
					total += values[k];
					total += values[k + 1];
				}
				results[i] = total;
			}

			// Display results.
			Console.WriteLine($"Expected output: {offset}");
			
			for (int i = 0; i < NUM_TESTS; i++)
			{
				Console.WriteLine($"\t{i+1}: {results[i]}");
			}
		}

		/// <summary>
		/// Copies an array and then shuffles it, returning the shuffled copy.
		/// </summary>
		/// <typeparam name="T">Generic type of the array.</typeparam>
		/// <param name="randomProvider">Provider of random values.</param>
		/// <param name="array">Array to copy and shuffle.</param>
		/// <returns>Shuffled copy of the array.</returns>
		private static T[] FisherYates<T>(Random randomProvider, T[] array)
		{
			// Copy values.
			T[] copy = new T[array.Length];
			Array.Copy(array, 0, copy, 0, array.Length);

			// Perform FisherYates.
			int n = copy.Length;
			while (n > 1)
			{
				int m = randomProvider.Next(n--);
				(copy[m], copy[n]) = (copy[n], copy[m]);
			}

			return copy;
		}
	}
}

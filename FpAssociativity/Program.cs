using System;

namespace FpAssociativity
{
	/// <summary>
	/// This application shows that floating-point operations are not guaranteed
	/// to be associative.<para />
	/// 
	/// I.e.: A + (B + C) =/= (A + B) + C<para />
	/// 
	/// This is caused by minute inaccuracies in precision accumulating over many
	/// operations, especially as the numbers grow more imprecise (further away
	/// from zero). Inaccuracies may be observable even for small numbers of
	/// operations close to 0. The closer a given digit is to the small-end of
	/// the floating-point structure's precision, the more likely it is to be
	/// affected.<para />
	/// 
	/// The demonstration is performed by creating an array of numbers with their
	/// negated values around an offset (ex. 5.05 and -5.05, where the offset is
	/// '5'), and then adding them together in a different order. The end result
	/// should be the same as the offset (when solved analytically, this is
	/// true), butt because the values are added in different orders, the result
	/// has a chance to be difference.<para/>
	/// 
	/// Below, the "size" variable represents the number of operations and
	/// "offset" represents the distance away from zero.
	/// </summary>
	internal class Program
	{
		const string INVALID_INPUT_MESSAGE = "Input was invalid. Try again.";

		static void Main(string[] args)
		{
			int size = 0;
			double offset = 0;
			bool valid = false;

			while (!valid)
			{
				Console.Write("Enter the number of FP operations to add: ");
				string? input = Console.ReadLine();

				if (int.TryParse(input, out size))
				{
					if (size > 1)
					{
						valid = true;
					}
				}
				
				if (!valid)
				{
					Console.Clear();
					Console.WriteLine(INVALID_INPUT_MESSAGE);
				}
			}

			valid = false;

			while (!valid)
			{
				Console.Write("Enter the offset (default = 1): ");
				string? input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					offset = 1;
					valid = true;
				}
				else
				{
					if (double.TryParse(input, out offset))
					{
						valid = true;
					}
					else
					{
						Console.Clear();
						Console.WriteLine(INVALID_INPUT_MESSAGE);
					}
				}
			}

			Demo.Run(size, offset);

			Console.WriteLine();
			Console.WriteLine("Press any key to close...");
			Console.ReadKey(true);
		}
	}
}
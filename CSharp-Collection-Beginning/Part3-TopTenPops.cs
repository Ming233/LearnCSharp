using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Collection_Beginning
{
    public class Part3_TopTenPops
    {
		public static void Part3_TopTenPops_Main(string filePath)
		{
			Part3_CsvReader reader = new Part3_CsvReader(filePath);

			Console.WriteLine(reader);
			Part3_Country[] countries = reader.ReadFirstNCountries(10);

			foreach (Part3_Country country in countries)
			{
				Console.WriteLine($"{Part3_PopulationFormatter.Part3_FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
		}
	}

	class Part3_CsvReader
	{
		private string _csvFilePath;

		public Part3_CsvReader(string csvFilePath)
		{
			this._csvFilePath = csvFilePath;
		}

		public Part3_Country[] ReadFirstNCountries(int nCountries)
		{
			Part3_Country[] countries = new Part3_Country[nCountries];

			using (StreamReader sr = new StreamReader(_csvFilePath))
			{
				// read header line
				sr.ReadLine();

				for (int i = 0; i < nCountries; i++)
				{
					string csvLine = sr.ReadLine();
					countries[i] = ReadCountryFromCsvLine(csvLine);
				}
			}
			return countries;
		}

		public Part3_Country ReadCountryFromCsvLine(string csvLine)
		{
			string[] parts = csvLine.Split(new char[] { ',' });

			string name = parts[0];
			string code = parts[1];
			string region = parts[2];
			int population = int.Parse(parts[3]);

			return new Part3_Country(name, code, region, population);
		}
	}

	class Part3_Country
	{
		public string Name { get; }
		public string Code { get; }
		public string Region { get; }
		public int Population { get; }

		public Part3_Country(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = code;
			this.Region = region;
			this.Population = population;
		}
	}

	class Part3_PopulationFormatter
	{
		public static string Part3_FormatPopulation(int population)
		{
			if (population == 0)
				return "(Unknown)";

			int popRounded = Part3_RoundPopulation4(population);

			return $"{popRounded:### ### ### ###}".Trim();
		}

		// Rounds the population to 4 significant figures
		private static int Part3_RoundPopulation4(int population)
		{
			// work out what rounding accuracy we need if we are to round to 
			// 4 significant figures
			int accuracy = Math.Max((int)(Part3_GetHighestPowerofTen(population) / 10_000), 1);

			// now do the rounding
			return Part3_RoundToNearest(population, accuracy);

		}

		/// <summary>
		/// Rounds the number to the specified accuracy
		/// For example, if the accuracy is 10, then we round to the nearest 10:
		/// 23 -> 20
		/// 25 -> 30
		/// etc.
		/// </summary>
		/// <param name="exact"></param>
		/// <param name="accuracy"></param>
		/// <returns></returns>
		public static int Part3_RoundToNearest(int exact, int accuracy)
		{
			int adjusted = exact + accuracy / 2;
			return adjusted - adjusted % accuracy;
		}

		/// <summary>
		/// Returns the highest number that is a power of 10 and is no larger than the number supplied
		/// Examples:
		/// GetHighestPowerOfTen(11) = 10
		/// GetHighestPowerOfTen(99) = 10
		/// GetHighestPowerOfTen(100) = 100
		/// GetHighestPowerOfTen(843) = 100
		/// GetHighestPowerOfTen(1000) = 1000
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public static long Part3_GetHighestPowerofTen(int x)
		{
			long result = 1;
			while (x > 0)
			{
				x /= 10;
				result *= 10;
			}
			return result;
		}
	}
}

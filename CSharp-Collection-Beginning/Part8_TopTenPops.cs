using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Collection_Beginning
{
    class Part8_TopTenPops
    {
		static void Part8_TopTenPops_Main(string filePath)
		{
			Part9_CsvReader reader = new Part9_CsvReader(filePath);

			// using the array with an interface
			IList<Part9_Country> countries = reader.ReadFirstNCountries(10);

			foreach (Part9_Country country in countries)
			{
				Console.WriteLine($"{Part9_PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
		}
	}

	class Part9_CsvReader
	{
		private string _csvFilePath;

		public Part9_CsvReader(string csvFilePath)
		{
			this._csvFilePath = csvFilePath;
		}

		public Part9_Country[] ReadFirstNCountries(int nCountries)
		{
			Part9_Country[] countries = new Part9_Country[nCountries];

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

		private static Part9_Country ReadCountryFromCsvLine(string csvLine)
		{
			string[] parts = csvLine.Split(',');
			string name = parts[0];
			string code = parts[1];
			string region = parts[2];
			bool popOK = int.TryParse(parts[3], out int population);
			return new Part9_Country(name, code, region, population);
		}

	}

	class Part9_Country
	{
		public string Name { get; }
		public string Code { get; }
		public string Region { get; }
		public int Population { get; }

		public Part9_Country(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = code;
			this.Region = region;
			this.Population = population;
		}
	}

	class Part9_PopulationFormatter
	{
		public static string FormatPopulation(int population)
		{
			if (population == 0)
				return "(Unknown)";

			int popRounded = RoundPopulation(population);

			return $"{popRounded:### ### ### ###}".Trim();
		}

		// Rounds the population to 4 significant figures
		private static int RoundPopulation(int population)
		{
			int accuracy = Math.Max((int)(GetHighestPowerofTen(population) / 10_000), 1); // to round to 4 significant figures
			return RoundToNearest(population, accuracy);

		}

		public static string FormatPopulationNoRound(int population)
		{
			if (population == 0)
				return "(Unknown)";

			int popRounded = population;

			return $"{popRounded:### ### ### ###}".Trim();
		}


		public static int RoundToNearest(int exact, int accuracy)
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
		public static long GetHighestPowerofTen(int x)
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

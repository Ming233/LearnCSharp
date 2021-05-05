using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Collection_Beginning
{
    public class Part7_ReadAllCountries_LINQ
    {
		public static void Part7_ReadAllCountries_LINQ_Main(string filePath)
		{
			Part7_CsvReader reader = new Part7_CsvReader(filePath);

			List<Part7_Country> countries = reader.ReadAllCountries();

			var filterCountries1 = countries.Where(x => !x.Name.Contains(','));
			foreach (Part7_Country country in filterCountries1)
			{
				Console.WriteLine($"{Part7_PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}

			var filterCountries2 = from country in countries
								   where !country.Name.Contains(',')
								   select country;
			foreach (Part7_Country country in filterCountries2)
			{
				Console.WriteLine($"{Part7_PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
			
			// listing the first 20 countries without commas in their names
			foreach (Part7_Country country in countries.Where(x => !x.Name.Contains(',')).Take(20))
			{
				Console.WriteLine($"{Part7_PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}

			Console.WriteLine();

			// listing the 10 highest population countries in alphabetical order.
			// Reverse Where() and Take() to see the impact of swapping chaining order round
			foreach (Part7_Country country in countries.Take(10).OrderBy(x => x.Name))
			{
				Console.WriteLine($"{Part7_PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
			}
		}
	}

	class Part7_CsvReader
	{
		private string _csvFilePath;

		public Part7_CsvReader(string csvFilePath)
		{
			this._csvFilePath = csvFilePath;
		}

		private static Part7_Country ReadCountryFromCsvLine(string csvLine)
		{
			string[] parts = csvLine.Split(',');
			string name;
			string code;
			string region;
			string popText;
			switch (parts.Length)
			{
				case 4:
					name = parts[0];
					code = parts[1];
					region = parts[2];
					popText = parts[3];
					break;
				case 5:
					name = parts[0] + ", " + parts[1];
					name = name.Replace("\"", null).Trim();
					code = parts[2];
					region = parts[3];
					popText = parts[4];
					break;
				default:
					throw new Exception($"Can't parse country from csvLine: {csvLine}");
			}
			// TryParse leaves population=0 if can't parse
			int.TryParse(popText, out int population);
			return new Part7_Country(name, code, region, population);
		}

		public List<Part7_Country> ReadAllCountries()
		{
			List<Part7_Country> countries = new List<Part7_Country>();

			using (StreamReader sr = new StreamReader(_csvFilePath))
			{
				// read header line
				sr.ReadLine();

				string csvLine;
				while ((csvLine = sr.ReadLine()) != null)
				{
					countries.Add(ReadCountryFromCsvLine(csvLine));
				}
			}
			return countries;
		}
	}

	class Part7_Country
	{
		public string Name { get; }
		public string Code { get; }
		public string Region { get; }
		public int Population { get; }

		public Part7_Country(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = code;
			this.Region = region;
			this.Population = population;
		}

		//		public bool IsPopKnown => this.Population > 0;

	}
	class Part7_PopulationFormatter
	{
		public static string FormatPopulation(int population)
		{
			if (population == 0)
				return "(Unknown)";

			int popRounded = RoundPopulation4(population);

			return $"{popRounded:### ### ### ###}".Trim();
		}

		// Rounds the population to 4 significant figures
		private static int RoundPopulation4(int population)
		{
			// work out what rounding accuracy we need if we are to round to 
			// 4 significant figures
			int accuracy = Math.Max((int)(GetHighestPowerofTen(population) / 10_000), 1);

			// now do the rounding
			return RoundToNearest(population, accuracy);

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

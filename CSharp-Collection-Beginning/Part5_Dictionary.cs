using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Collection_Beginning
{
    public class Part5_Dictionary
    {
		public static void Part5_Dictionary_Main(string[] args)
		{
			Part5_Dictionary_Country norway = new Part5_Dictionary_Country("Norway", "NOR", "Europe", 5_282_223);
			Part5_Dictionary_Country finland = new Part5_Dictionary_Country("Finland", "FIN", "Europe", 5_511_303);

			var countries = new Dictionary<string, Part5_Dictionary_Country>();
			countries.Add(norway.Code, norway);
			countries.Add(finland.Code, finland);

			Console.WriteLine("Enumerating...");
			foreach (Part5_Dictionary_Country nextPart5_Country in countries.Values)
				Console.WriteLine(nextPart5_Country.Name);
			Console.WriteLine();

			//Console.WriteLine(countries["MUS"].Name);
			bool exists = countries.TryGetValue("MUS", out Part5_Dictionary_Country country);
			if (exists)
				Console.WriteLine(country.Name);
			else
				Console.WriteLine("There is no country with the code MUS");

		}
	}

	class Part5_Dictionary_Country
	{
		public string Name { get; }
		public string Code { get; }
		public string Region { get; }
		public int Population { get; }

		public Part5_Dictionary_Country(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = code;
			this.Region = region;
			this.Population = population;
		}
	}
}

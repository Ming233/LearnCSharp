using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Collection_Beginning
{
    public class Part2_DaysOfWeek
    {
		public static void Part2_DaysOfWeek_Main()
		{
			string[] daysOfWeek = {
				"Monday",
				"Tuesday",
				"Wensday",
				"Thursday",
				"Friday",
				"Saturday",
				"Sunday"
			};

			Console.WriteLine("Before:");
			foreach (string day in daysOfWeek)
				Console.WriteLine(day);

			daysOfWeek[2] = "Wednesday";

			Console.WriteLine("\r\nBefore:");
			foreach (string day in daysOfWeek)
				Console.WriteLine(day);
		}

		public static void Part2_Nth_DaysOfWeek_Main()
		{
			string[] daysOfWeek = {
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday",
				"Sunday"
			};

			Console.WriteLine("Which day do you want to display?");
			Console.Write("(Monday = 1, etc.) > ");
			int iDay = int.Parse(Console.ReadLine());

			string chosenDay = daysOfWeek[iDay - 1];
			Console.WriteLine($"That day is {chosenDay}");

		}


		public static void Part3_DaysOfWeek_Main()
		{
			List<string> daysOfWeek = new List<string>
			{
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday",
				"Sunda"
			};
		}
	}
}

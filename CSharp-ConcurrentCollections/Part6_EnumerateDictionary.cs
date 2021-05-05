using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CSharp_ConcurrentCollections
{
	class Part6_EnumerateDictionary
	{
		public static void Part6_EnumerateDictionary_Main()
		{
			// change this to a Dictionary<string, int> to see that enumerating while modifying
			// throws an exception for the standard dictionary
			var stock = new ConcurrentDictionary<string, int>();
			stock.TryAdd("jDays", 0);
			stock.TryAdd("Code School", 0);
			stock.TryAdd("Buddhist Geeks", 0);

			foreach (var shirt in stock)
			{
				//stock["jDays"] += 1;
				stock.AddOrUpdate("jDays", 0, (key, value) => value + 1);
				Console.WriteLine(shirt.Key + ": " + shirt.Value);
			}
		}
	}
}

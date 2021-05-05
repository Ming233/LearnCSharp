using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CSharp_ConcurrentCollections
{
    class Part2_ConcurrentDictionary
    {
        public static void Part2_ConcurrentDictionary_Main()
        {
            // StartOfModule contains the basic dictionary operations as presented
            // at the beginning of module 2, using Dictionary<string, int>
            StartOfModule();

            // StartOfModule contains the basic dictionary operations after modification
            // during the module, and therefore using ConcurrentDictionary<string, int>
            EndOfModule();

        }

        private static void StartOfModule()
        {
            Console.WriteLine("Start of Module - Using Dicstionary");
            var stock = new Dictionary<string, int>()
            {
                {"jDays", 4},
                {"technologyhour", 3}
            };
            Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            stock.Add("pluralsight", 6);
            stock["buddhistgeeks"] = 5;
            Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            stock["pluralsight"] = 7; // up from 6 - we just bought one			
            Console.WriteLine(string.Format("\r\nstock[pluralsight] = {0}", stock["pluralsight"]));

            stock.Remove("jDays");

            Console.WriteLine("\r\nEnumerating:");
            foreach (var keyValPair in stock)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }
        }

        private static void EndOfModule()
        {
            Console.WriteLine("\n\n\nEnd of Module - Using ConcurrentDicstionary");
            var stock = new ConcurrentDictionary<string, int>();
            stock.TryAdd("jDays", 4);
            stock.TryAdd("technologyhour", 3);
            Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            bool success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine("Added succeeded? " + success);
            success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine("Added succeeded? " + success);

            stock["buddhistgeeks"] = 5;

            //try to update this
            bool success1 = stock.TryUpdate("pluralsight", 8, 7);
            Console.WriteLine("pluralsight = {0}, did update work? {1} ", stock["pluralsight"], success1);
            success1 = stock.TryUpdate("pluralsight", 8, 7);
            Console.WriteLine("pluralsight = {0}, did update work? {1} ", stock["pluralsight"], success1);


            // stock["pluralsight"]++;
            int psStock = stock.AddOrUpdate("pluralsight", 1, (key, oldValue) => oldValue + 1);
            Console.WriteLine("New value is " + psStock);

            Console.WriteLine(string.Format("stock[pluralsight] = {0}", stock.GetOrAdd("pluralsight", 0)));
            Console.WriteLine(string.Format("stock[pluralsight] = {0}", stock.GetOrAdd("Ming T-Shirt", 0)));


            int jDaysValue;
            success = stock.TryRemove("jDays", out jDaysValue);
            if (success)
                Console.WriteLine("value removed was: " + jDaysValue);

            Console.WriteLine("\r\nEnumerating:");
            foreach (var keyValPair in stock)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }
        }


    }
}

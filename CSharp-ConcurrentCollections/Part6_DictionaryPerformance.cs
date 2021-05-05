using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace CSharp_ConcurrentCollections
{
    class Part6_DictionaryPerformance
    {
		public static void Part6_DictionaryPerformance_Main()
		{
			// comment out all calls to Worker.DoSomethingTimeConsuming() 
			// throughout project to see how the benchmark works when threads spend
			// most of their time dealing with the concurrent dictionary
			int dictSize = 1000000;

			Console.WriteLine("Dictionary, single thread:");
			var dict = new Dictionary<int, int>();
			SingleThreadBenchmark.TimeDict(dict, dictSize);

			Console.WriteLine("\r\nConcurrentDictionary, single thread:");
			var dict2 = new ConcurrentDictionary<int, int>();
			SingleThreadBenchmark.TimeDict(dict2, dictSize);

			Console.WriteLine("\r\nConcurrentDictionary, multiple threads:");
			dict2 = new ConcurrentDictionary<int, int>();
			ParallelBenchmark.TimeDictParallel(dict2, dictSize);

		}
	}

	class SingleThreadBenchmark
	{
		static void PopulateDict(IDictionary<int, int> dict, int dictSize)
		{
			for (int i = 0; i < dictSize; i++)
			{
				dict.Add(i, 0);
			}

			for (int i = 0; i < dictSize; i++)
			{
				dict[i] += 1;
				Worker.DoSomethingTimeConsuming();
			}
		}
		static int GetTotalValue(IDictionary<int, int> dict)
		{
			int total = 0;
			foreach (var item in dict)
			{
				total += dict[item.Value];
				Worker.DoSomethingTimeConsuming();
			}
			return total;
		}

		public static void TimeDict(IDictionary<int, int> dict, int dictSize)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			PopulateDict(dict, dictSize);
			stopwatch.Stop();
			Console.WriteLine(string.Format("Time taken to build dictionary (ms):     {0}", stopwatch.ElapsedMilliseconds));

			stopwatch.Restart();
			int total = GetTotalValue(dict);
			stopwatch.Stop();
			Console.WriteLine(string.Format("Time taken to enumerate dictionary (ms): {0}", stopwatch.ElapsedMilliseconds));

			Console.WriteLine("total is " + total.ToString());
			if (total != dictSize)
				Console.WriteLine("ERROR IN TOTAL!");

		}

	}

	class ParallelBenchmark
	{
		static void PopulateDictParallel(ConcurrentDictionary<int, int> dict, int dictSize)
		{
			Parallel.For(0, dictSize, (i) => dict.TryAdd(i, 0));
			Parallel.For(0, dictSize,
				(i) => {
					bool done = dict.TryUpdate(i, 1, 0);
					if (!done)
						throw new Exception("Error updating. Old value was " + dict[i]);
					Worker.DoSomethingTimeConsuming();
				});
		}
		static int GetTotalValueParallel(ConcurrentDictionary<int, int> dict)
		{
			int expectedTotal = dict.Count;

			int total = 0;
			Parallel.ForEach(dict,
				keyValPair => {
					Interlocked.Add(ref total, keyValPair.Value); Worker.DoSomethingTimeConsuming();
				});
			return total;
		}
		public static void TimeDictParallel(ConcurrentDictionary<int, int> dict, int dictSize)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			PopulateDictParallel(dict, dictSize);
			stopwatch.Stop();
			Console.WriteLine(string.Format("Time taken to build dictionary (ms):     {0}", stopwatch.ElapsedMilliseconds));

			stopwatch.Restart();
			int total = GetTotalValueParallel(dict);
			stopwatch.Stop();
			Console.WriteLine(string.Format("Time taken to enumerate dictionary (ms): {0}", stopwatch.ElapsedMilliseconds));

			Console.WriteLine("total is " + total.ToString());
			if (total != dictSize)
				Console.WriteLine("ERROR IN TOTAL!");
		}
	}

	public static class Worker
	{
		public static int DoSomethingTimeConsuming()
		{
			int total = 0;
			for (int i = 0; i < 1000; i++)
				total += i;
			return total;
		}
	}
}

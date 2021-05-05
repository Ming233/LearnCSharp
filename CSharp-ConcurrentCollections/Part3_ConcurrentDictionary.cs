using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace CSharp_ConcurrentCollections
{
	class Part3_ConcurrentDictionary
	{
		public static readonly List<string> AllShirtNames =
			new List<string> { "technologyhour", "Code School", "jDays", "buddhistgeeks", "iGeek" };

		public static void Part3_ConcurrentDictionary_Main()
		{
			StockController controller = new StockController();
			TimeSpan workDay = new TimeSpan(0, 0, 2);

			Task t1 = Task.Run(() => new SalesPerson("Sahil").Work(controller, workDay));
			Task t2 = Task.Run(() => new SalesPerson("Peter").Work(controller, workDay));
			Task t3 = Task.Run(() => new SalesPerson("Juliette").Work(controller, workDay));
			Task t4 = Task.Run(() => new SalesPerson("Xavier").Work(controller, workDay));

			Task.WaitAll(t1, t2, t3, t4);
			controller.DisplayStatus();
		}
	}

	public class StockController
	{
		ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
		int _totalQuantityBought;
		int _totalQuantitySold;

		public void BuyStock(string item, int quantity)
		{
			_stock.AddOrUpdate(item, quantity, (key, oldValue) => oldValue + quantity);
			Interlocked.Add(ref _totalQuantityBought, quantity);
		}

		public bool TrySellItem(string item)
		{
			bool success = false;
			int newStockLevel = _stock.AddOrUpdate(item,
				(itemName) => { success = false; return 0; },
				(itemName, oldValue) => {
					if (oldValue == 0)
					{
						success = false;
						return 0;
					}
					else
					{
						success = true;
						return oldValue - 1;
					}
				});
			if (success)
				Interlocked.Increment(ref _totalQuantitySold);
			return success;
		}

		public bool TrySellItem2(string item)
		{
			int newStockLevel = _stock.AddOrUpdate(item, -1, (key, oldValue) => oldValue - 1);
			if (newStockLevel < 0)
			{
				_stock.AddOrUpdate(item, 1, (key, oldValue) => oldValue + 1);
				return false;
			}
			else
			{
				Interlocked.Increment(ref _totalQuantitySold);
				return true;
			}
		}

		public void DisplayStatus()
		{
			int totalStock = _stock.Values.Sum();
			Console.WriteLine("\r\nBought = " + _totalQuantityBought);
			Console.WriteLine("Sold   = " + _totalQuantitySold);
			Console.WriteLine("Stock  = " + totalStock);
			int error = totalStock + _totalQuantitySold - _totalQuantityBought;
			if (error == 0)
				Console.WriteLine("Stock levels match");
			else
				Console.WriteLine("Error in stock level: " + error);

			Console.WriteLine();
			Console.WriteLine("Stock levels by item:");
			foreach (string itemName in Part3_ConcurrentDictionary.AllShirtNames)
			{
				int stockLevel = _stock.GetOrAdd(itemName, 0);
				Console.WriteLine("{0,-30}: {1}", itemName, stockLevel);
			}
		}


	}

	public class SalesPerson
	{
		public string Name { get; private set; }

		public SalesPerson(string id)
		{
			this.Name = id;
		}

		public void Work(StockController stockController, TimeSpan workDay)
		{
			Random rand = new Random(Name.GetHashCode());
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < workDay)
			{
				Thread.Sleep(rand.Next(100));
				bool buy = (rand.Next(6) == 0);
				string itemName = Part3_ConcurrentDictionary.AllShirtNames[rand.Next(Part3_ConcurrentDictionary.AllShirtNames.Count)];
				if (buy)
				{
					int quantity = rand.Next(9) + 1;
					stockController.BuyStock(itemName, quantity);
					DisplayPurchase(itemName, quantity);
				}
				else
				{
					bool success = stockController.TrySellItem(itemName);
					DisplaySaleAttempt(success, itemName);
				}
			}
			Console.WriteLine("SalesPerson {0} signing off", this.Name);
		}

		public void DisplayPurchase(string itemName, int quantity)
		{
			Console.WriteLine("Thread {0}: {1} bought {2} of {3}", Thread.CurrentThread.ManagedThreadId, this.Name, quantity, itemName);
		}

		public void DisplaySaleAttempt(bool success, string itemName)
		{
			int threadId = Thread.CurrentThread.ManagedThreadId;
			if (success)
				Console.WriteLine(string.Format("Thread {0}: {1} sold {2}", threadId, this.Name, itemName));
			else
				Console.WriteLine(string.Format("Thread {0}: {1}: Out of stock of {2}", threadId, this.Name, itemName));
		}

	}
}

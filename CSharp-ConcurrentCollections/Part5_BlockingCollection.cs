using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CSharp_ConcurrentCollections
{
    class ProgPart5_BlockingCollectionram
    {
        public static readonly List<string> AllShirtNames = new List<string>
        { "technologyhour", "Code School", "jDays", "buddhistgeeks", "iGeek" };

        public static void ProgPart5_BlockingCollectionram_Main()
        {

            StaffLogsForBonuses staffLogs = new StaffLogsForBonuses();
            ToDoQueue toDoQueue = new ToDoQueue(staffLogs);

            Part5_SalesPerson[] people = {   new Part5_SalesPerson("Sahil"),
                                       new Part5_SalesPerson("Peter"),
                                       new Part5_SalesPerson("Juliette"),
                                       new Part5_SalesPerson("Xavier") };

            Part5_StockController controller = new Part5_StockController(toDoQueue);

            TimeSpan workDay = new TimeSpan(0, 0, 1);

            Task t1 = Task.Run(() => people[0].Work(controller, workDay));
            Task t2 = Task.Run(() => people[1].Work(controller, workDay));
            Task t3 = Task.Run(() => people[2].Work(controller, workDay));
            Task t4 = Task.Run(() => people[3].Work(controller, workDay));

            Task bonusLogger = Task.Run(() => toDoQueue.MonitorAndLogTrades());
            Task bonusLogger2 = Task.Run(() => toDoQueue.MonitorAndLogTrades());

            Task.WaitAll(t1, t2, t3, t4);
            toDoQueue.CompleteAdding();
            Task.WaitAll(bonusLogger, bonusLogger2);

            controller.DisplayStatus();
            staffLogs.DisplayReport(people);
        }

    }

    public class Part5_SalesPerson
    {
        public string Name { get; private set; }

        public Part5_SalesPerson(string id)
        {
            this.Name = id;
        }

        public void Work(Part5_StockController stockController, TimeSpan workDay)
        {
            Random rand = new Random(Name.GetHashCode());
            DateTime start = DateTime.Now;
            while (DateTime.Now - start < workDay)
            {
                Thread.Sleep(rand.Next(100));
                bool buy = (rand.Next(6) == 0);
                string itemName = ProgPart5_BlockingCollectionram.AllShirtNames[rand.Next(ProgPart5_BlockingCollectionram.AllShirtNames.Count)];
                if (buy)
                {
                    int quantity = rand.Next(9) + 1;
                    stockController.BuyStock(this, itemName, quantity);
                    DisplayPurchase(itemName, quantity);
                }
                else
                {
                    bool success = stockController.TrySellItem(this, itemName);
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
    public class StaffLogsForBonuses
    {
        private ConcurrentDictionary<Part5_SalesPerson, int> _salesByPerson = new ConcurrentDictionary<Part5_SalesPerson, int>();
        private ConcurrentDictionary<Part5_SalesPerson, int> _purchasesByPerson =
             new ConcurrentDictionary<Part5_SalesPerson, int>();

        public void ProcessTrade(Trade sale)
        {
            Thread.Sleep(300);
            if (sale.QuantitySold > 0)
                _salesByPerson.AddOrUpdate(
                    sale.Person,
                    sale.QuantitySold,
                    (key, oldValue) => oldValue + sale.QuantitySold);
            else
                _purchasesByPerson.AddOrUpdate(
                    sale.Person,
                    -sale.QuantitySold,
                    (key, oldValue) => oldValue - sale.QuantitySold);
        }


        public void DisplayReport(Part5_SalesPerson[] people)
        {
            Console.WriteLine();
            Console.WriteLine("Transactions by salesperson:");
            foreach (Part5_SalesPerson person in people)
            {
                int sales = _salesByPerson.GetOrAdd(person, 0);
                int purchases = _purchasesByPerson.GetOrAdd(person, 0);
                Console.WriteLine("{0,15} sold {1,3}, bought {2,3} items, total {3}", person.Name, sales, purchases, sales + purchases);
            }
        }
    }

    public class Part5_StockController
    {
        ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
        int _totalQuantityBought;
        int _totalQuantitySold;
        ToDoQueue _toDoQueue;

        public Part5_StockController(ToDoQueue bonusCalculator)
        {
            this._toDoQueue = bonusCalculator;
        }

        public void BuyStock(Part5_SalesPerson person, string item, int quantity)
        {
            _stock.AddOrUpdate(item, quantity, (key, oldValue) => oldValue + quantity);
            Interlocked.Add(ref _totalQuantityBought, quantity);
            _toDoQueue.AddTrade(new Trade(person, -quantity));
        }

        public bool TrySellItem(Part5_SalesPerson person, string item)
        {
            bool success = false;
            int newStockLevel = _stock.AddOrUpdate(item,
                (itemName) => { success = false; return 0; },
                (itemName, oldValue) =>
                {
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
            {
                Interlocked.Increment(ref _totalQuantitySold);
                _toDoQueue.AddTrade(new Trade(person, 1));
            }
            return success;
        }

        public bool TrySellItem2(Part5_SalesPerson person, string item)
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
                _toDoQueue.AddTrade(new Trade(person, 1));
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
            foreach (string itemName in ProgPart5_BlockingCollectionram.AllShirtNames)
            {
                int stockLevel = _stock.GetOrAdd(itemName, 0);
                Console.WriteLine("{0,-30}: {1}", itemName, stockLevel);
            }
        }


    }

    public class ToDoQueue
    {
        private readonly BlockingCollection<Trade> _queue
            = new BlockingCollection<Trade>(new ConcurrentBag<Trade>());
        private readonly StaffLogsForBonuses _staffLogs;

        public ToDoQueue(StaffLogsForBonuses staffResults)
        {
            _staffLogs = staffResults;
        }

        public void AddTrade(Trade transaction)
        {
            _queue.Add(transaction);
        }

        public void CompleteAdding()
        {
            _queue.CompleteAdding();
        }



        public void MonitorAndLogTrades()
        {
            while (true)
            {
                try
                {
                    Trade nextTransaction = _queue.Take();
                    _staffLogs.ProcessTrade(nextTransaction);
                    Console.WriteLine("Processing transaction from " + nextTransaction.Person.Name);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }


    }

    public class Trade
    {
        public Part5_SalesPerson Person { get; private set; }

        //  QuantitySold is negative if the trade was a purchase
        public int QuantitySold { get; private set; }

        public Trade(Part5_SalesPerson person, int quantitySold)
        {
            this.Person = person;
            this.QuantitySold = quantitySold;
        }
    }
}

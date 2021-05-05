using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page3_4
    {
        public static void Page3_4_Main()
        {
            Parallel.Invoke(() => Task1(), () => Task2());
            Console.WriteLine("Finished processing. Press any key to end.");
            Console.ReadKey();
        }

        static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(3000);
            Console.WriteLine("Task 1 End");
        }

        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 End");
        }
    }

    class Page4_5
    {
        public static void Page4_5_Main()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });
            Console.WriteLine("Finished processing. Press any key to end.");
            Console.ReadKey();
        }

        public static void Page5_Main()
        {
            var items = Enumerable.Range(0, 500).ToArray();

            Parallel.For(0, items.Length, i =>
            {
                WorkOnItem(i);
            });
            Console.WriteLine();
            Console.WriteLine("Finished processing. Press any key to end.");
            Console.ReadKey();
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Start working on: " + item);
            Thread.Sleep(1000);
            Console.WriteLine("Finished working on: " + item);

        }

   
    }
}

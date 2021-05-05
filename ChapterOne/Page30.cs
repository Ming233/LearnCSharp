using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page30
    {


        public static void Page30_Main()
        {
            BlockingCollection<int> data = new BlockingCollection<int>(5);

            Task.Run(() =>
            {
                for (int i = 0; i < 11; i++)
                {
                    data.Add(i);
                    Console.WriteLine("Data {0} added successfully.", i);
                }
                data.CompleteAdding();
            });

            Console.WriteLine("Adding completed. Press any key to end.");
            Console.ReadKey();

            Task.Run(() =>
            {
                while (!data.IsAddingCompleted)
                {
                    try
                    {
                        int v = data.Take();
                        Console.WriteLine("Data {0} Taken successfully.", v);
                    }
                    catch (InvalidOperationException) { }
                }
            });

            Console.WriteLine("Finished processing. Press any key to end.");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page13
    {

        static void HelloTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello task");
        }

        static void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World task");
        }

        public static void Page13_Main()
        {
            Task task = Task.Run(() => HelloTask());
            task.ContinueWith((prevTask) => WorldTask());


            Console.WriteLine("Finished processing. Press any key to end.");
            Console.ReadKey();
        }
    }
}

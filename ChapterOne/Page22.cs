using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page22
    {
        static void DoWork(object data)
        {
            Console.WriteLine("Doing some work: "+data);
            Thread.Sleep(1000);
            Console.WriteLine("Finished work: " + data);
        }

        public static void Page22_Main()
        {

            for (int i = 0; i< 100; i++)
            {
                int stateNumber = 1;
                ThreadPool.QueueUserWorkItem(state => DoWork(stateNumber));
                   
            }

            Console.WriteLine("Finished processing. Press any key to end.");
            Console.ReadKey();
        }
    }
}

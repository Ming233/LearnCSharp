using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page51
    {
        static int counter;
        static void Initialized()
        {
            Console.WriteLine("Initialize Called");
            counter = 0;
        }
        static void Update()
        {
            Console.WriteLine("Update Called");
            counter ++;
        }
        static bool Test()
        {
            Console.WriteLine("Test Called");
            return counter < 5;
        }

        public static void Page51_Main()
        {
            for(Initialized();Test();Update())
            {
                Console.WriteLine("Hello {0} ", counter);
            }
            Console.ReadLine();
        }
    }
}

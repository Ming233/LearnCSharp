using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page71
    {
        delegate int IntOperation(int a, int b);

        static int Add(int a, int b)
        {
            Console.WriteLine("Add called {0} + {1} = ", a, b);
            return a + b;
        }

        static int Subtract(int a, int b)
        {
            Console.WriteLine("Subtract called {0} - {1} = " , a, b);
            return a - b;
        }

        public static void main()
        {
            var op = new IntOperation(Add);
            Console.WriteLine(op(2,2));

            op = Subtract;
            Console.WriteLine(op(2,2));

        }
    }
}

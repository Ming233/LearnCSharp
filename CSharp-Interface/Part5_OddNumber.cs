using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Interface
{
    class Part5_OddNumber
    {
        public static void Part5_OddNumber_Main()
        {
            Console.WriteLine("Odd Numbers:");

            var generator = new OddGenerator();
            foreach (var odd in generator)
            {
                if (odd > 50)
                    break;
                Console.WriteLine(odd);
            }

            Console.Read();
        }
    }

    public class OddGenerator : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            int i = 1;
            yield return i;
            while (true)
            {
                i += 2;
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Console.WriteLine("demo");
            return this.GetEnumerator();
        }
    }
}

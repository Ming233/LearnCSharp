using System;

namespace CSharp_Generics
{
    public class Part4_DataSructures
    {
        public static void Part4_DataSructures_Main()
        {
            var buffer = new CircularBuffer<double>(capacity: 3);
            buffer.ItemDiscarded += ItemDiscarded;

            ProcessInput(buffer);



            //Dynamic convert type
            //var asString = buffer.AsEnumerableOf<string>();
            //foreach (var item in asString)
            //{
            //    Console.WriteLine(item);
            //}

            buffer.Dump(d => Console.WriteLine(d));

            ProcessBuffer(buffer);
        }

        static void ItemDiscarded(object sender,
            ItemDiscardedEventArgs<double> e)
        {
            Console.WriteLine("Buffer full. Discarding {0} New item is {1}",
                    e.ItemDiscarded, e.NewItem
                );
        }

        private static void ProcessBuffer(IBuffer<double> buffer)
        {
            var sum = 0.0;
            Console.WriteLine("Buffer: ");
            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }
            Console.WriteLine(sum);
        }

        private static void ProcessInput(IBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }
                break;
            }
        }
    }
}

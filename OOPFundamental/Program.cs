using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFundamental
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer();
            Customer customer2 = new Customer();

            // Static member cannot be run by object. Check code inside.
            Customer.InstanceCount += 1;

            Console.WriteLine(Customer.InstanceCount);
            Console.ReadLine();


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Collection_Beginning
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"P:\CSharpLearning\CSharp-Collection-Beginning\Pop by Largest Final.csv";

            //Part3_TopTenPops.Part3_TopTenPops_Main(filePath);

            //Part4_ReadAllCountries.Part4_ReadAllCountries_Main(filePath);

            Part5_ReadAllCountries_Dictionary.Part5_ReadAllCountries_Dictionary_Main(filePath);

            Console.ReadLine();

        }
    }
}

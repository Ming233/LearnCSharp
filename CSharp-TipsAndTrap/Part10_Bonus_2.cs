using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CSharp_TipsAndTrap
{
    class Part10_Bonus_2
    {
        static void Part10_Bonus_2_Main()
        {
            Processor processor = new Processor();

            try
            {
                List<Part10_Person> people = processor.Process("Names.txt", "missing.txt");

                foreach (var person in people)
                {
                    WriteLine($"{person.Name},{person.Age}");
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }


            WriteLine();
            WriteLine("Press enter to exit");
            ReadLine();
        }
    }

    class Processor
    {
        public List<Part10_Person> Process(string nameFilePath, string ageFilePath)
        {
            string[] names = LoadFileContents(nameFilePath);
            string[] ages = LoadFileContents(ageFilePath);

            return names.Zip(ages,
                (name, age) => new Part10_Person { Name = name, Age = int.Parse(age) }).ToList();
        }

        private string[] LoadFileContents(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    class Part10_Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

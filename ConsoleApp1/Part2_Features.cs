using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQFundamental
{
    public class Part2_Features
    {
        public static void Part2_Features_Main()
        {
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };
            Action<int> write = x => Console.WriteLine(x);

            Console.WriteLine("Func+Action");
            write(square(add(3, 5)));


            var developers = new Employee[]
            {
                new Employee { Id = 1, Name= "Scott" },
                new Employee { Id = 2, Name= "Chris" }
            };

            var sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            //Lambda
            var query = developers.Where(e => e.Name.Length == 5)
                                  .OrderBy(e => e.Name);

            //Query
            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;

            Console.WriteLine("Number of : " + developers.Count());


            Console.WriteLine("Write Name with LAMdba ");
            //foreach (var employee in developers.Where(e => e.Name.StartsWith("S")))
            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("Write Name with ");
            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("Another way to get the S name, but not useful");
            foreach (var employee in developers.Where(NameStartsWithS))
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
    }

    public static class MyLinq
    {
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            var count = 0;
            foreach (var item in sequence)
            {
                count += 1;
            }
            return count;
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

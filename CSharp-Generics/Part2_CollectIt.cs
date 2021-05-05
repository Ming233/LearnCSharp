using System;
using System.Collections.Generic;

namespace CSharp_Generics
{
    public class Part2_CollectIt
    {
        public static void CollectItMain()
        {
            var employeesByName = new SortedList<string, List<Employee>>();

            employeesByName.Add("Sales", new List<Employee> { new Employee(), new Employee(), new Employee() });
            employeesByName.Add("Engineering", new List<Employee> { new Employee(), new Employee() });

            foreach (var item in employeesByName)
            {
                Console.WriteLine("The count of employees for {0} is {1}",
                            item.Key, item.Value.Count
                        );
            }

        }

    }


    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

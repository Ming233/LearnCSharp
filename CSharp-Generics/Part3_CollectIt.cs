using System;
using System.Collections.Generic;

namespace CSharp_Generics3
{
    class Part3_CollectIt
    {
        public static void Part3_CollectIt_Main()
        {
            var departments = new DepartmentCollection();

            departments.Add("Sales", new Employee { Name = "Joy" })
                       .Add("Sales", new Employee { Name = "Dani" })
                       .Add("Sales", new Employee { Name = "Dani" });


            departments.Add("Engineering", new Employee { Name = "Scott" })
                       .Add("Engineering", new Employee { Name = "Alex" })
                       .Add("Engineering", new Employee { Name = "Dani" });



            foreach (var department in departments)
            {
                Console.WriteLine(department.Key);
                foreach (var employee in department.Value)
                {
                    Console.WriteLine("\t" + employee.Name);
                }
            }

        }
    }

    public class EmployeeComparer : IEqualityComparer<Employee>,
                                IComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return String.Equals(x.Name, y.Name);
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Name.GetHashCode();
        }

        public int Compare(Employee x, Employee y)
        {
            return String.Compare(x.Name, y.Name);
        }
    }

    public class DepartmentCollection :
        SortedDictionary<string, SortedSet<Employee>>
    {
        public DepartmentCollection Add(string departmentName, Employee employee)
        {
            //if department not exist, then add department first
            if (!ContainsKey(departmentName))
            {
                Add(departmentName, new SortedSet<Employee>(new EmployeeComparer()));
            }
            this[departmentName].Add(employee);
            return this;
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
    }
}

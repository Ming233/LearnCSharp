using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSharp_Generics
{
    class Part5_QueryIt
    {
        public static void Part5_QueryIt_Main()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeDb>());

            using (IRepository<Employee> employeeRepository = new SqlRepository<Employee>(new EmployeeDb()))
            {
                AddEmployees(employeeRepository);
                AddManagers(employeeRepository);
                CountEmployees(employeeRepository);
                QueryEmployees(employeeRepository);
                DumpPeople(employeeRepository);

                //This works fine as well
                Console.WriteLine("IEnumerable");
                IEnumerable<Person> temp = employeeRepository.FindAll();
                foreach (var employee in temp)
                {
                    Console.WriteLine(employee.Name);
                }
            }
        }

        private static void AddManagers(IWriteOnlyRepository<Manager> employeeRepository)
        {
            Console.WriteLine("Add Manager");
            employeeRepository.Add(new Manager { Name = "Alex" });
            employeeRepository.Commit();
        }

        private static void DumpPeople(IReadOnlyRepository<Person> employeeRepository)
        {
            Console.WriteLine("Give me all the employee and write out all employees - Dump People Method");
            var employees = employeeRepository.FindAll();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployees(IRepository<Employee> employeeRepository)
        {
            Console.WriteLine("Find Employee by ID");
            var employee = employeeRepository.FindById(1);
            Console.WriteLine(employee.Name);
        }

        private static void CountEmployees(IRepository<Employee> employeeRepository)
        {

            Console.WriteLine("Count Employee");
            Console.WriteLine(employeeRepository.FindAll().Count());
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            Console.WriteLine("Add Employees");
            employeeRepository.Add(new Employee { Name = "Scott" });
            employeeRepository.Add(new Employee { Name = "Chris" });
            employeeRepository.Add(new Employee { Name = "Ming" });
            employeeRepository.Add(new Employee { Name = "Tom" });
            employeeRepository.Commit();
        }
    }
}

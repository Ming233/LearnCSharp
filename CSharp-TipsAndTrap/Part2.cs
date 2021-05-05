using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static System.Console;

namespace CSharp_TipsAndTrap
{
    class Part2
    {

        public static void Part2_Main()
        {
            var employee = new Employee();

            employee.FirstName = GetFirstName();
            employee.EmployeeCode = GetEmployeeCode();
            employee.ProductivityRating = GetProductivity();
            employee.DateOfBirth = GetDateOfBirth();
            employee.Salary = GetSalary();
            employee.Id = GenerateRandomId();
            employee.WorkDays = GenerateDefaultWorkDays();
            GetSkillsFor(employee);
            GetBioFor(employee);

            DisplayEmployee(employee);

            WriteLine();
            WriteLine("Press enter to exit.");
            ReadLine();
        }

        class Employee
        {
            public string FirstName { get; set; }
            public char EmployeeCode { get; set; }
            public int ProductivityRating { get; set; }
            public List<string> Skills { get; } = new List<string>();
            public string Bio { get; set; }
            public DateTime DateOfBirth { get; set; }
            public BigInteger Salary { get; set; }
            public int Id { get; set; }
            public List<int> WorkDays { get; set; }
        }
        private static string GetFirstName()
        {
            while (true)
            {
                WriteLine("Please enter first name");

                string firstName = ReadLine();

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    WriteLine("ERROR: Invalid first name");
                }
                else
                {
                    return firstName;
                }
            }
        }

        private static char GetEmployeeCode()
        {
            while (true)
            {
                WriteLine("Please enter employee code");

                char employeeCode = ReadLine().First(); // Additional validation omitted
                //employeeCode = (char)888;

                UnicodeCategory ucCategory = char.GetUnicodeCategory(employeeCode);

                bool isValidUnicode = ucCategory != UnicodeCategory.OtherNotAssigned;

                if (!isValidUnicode)
                {
                    WriteLine();
                    WriteLine("ERROR: Invalid employee code (invalid character)");
                }
                else
                {
                    return employeeCode;
                }
            }
        }

        private static int GetProductivity()
        {
            WriteLine("Please enter productivity rating (-100 to 100) enter 0 for new employees");

            int rating = int.Parse(ReadLine()); // Additional validation omitted

            return rating;
        }

        private static DateTime GetDateOfBirth()
        {
            WriteLine("Please enter date of birth");

            string input = ReadLine();

            // DateTime dob = DateTime.Parse(input);
            DateTime dob = DateTime.ParseExact(input, "MM/dd/yyyy", null);

            DateTime d1 = DateTime.Parse("01/12/2000");
            DateTime d2 = DateTime.Parse("01/12/2000", null, DateTimeStyles.AssumeUniversal);
            DateTime d3 = DateTime.Parse("01/12/2000", null, DateTimeStyles.AssumeLocal);
            DateTime d4 = DateTime.Parse("13:30:00"); // Defaults to DateTimeStyles.None
            DateTime d5 = DateTime.Parse("13:30:00", null, DateTimeStyles.NoCurrentDateDefault);

            return dob;
        }
        private static BigInteger GetSalary()
        {
            WriteLine("Please enter salary");

            string input = ReadLine(); // error checking code omitted

            //int value = int.Parse(input);

            BigInteger value = BigInteger.Parse(input);

            value++;
            value--;
            value = value * 2;

            //var biggest = Math.Max(0, value);
            var biggest = BigInteger.Max(0, value);

            return value;
        }

        private static int GenerateRandomId()
        {
            using (RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];

                rnd.GetBytes(randomBytes);

                int result = BitConverter.ToInt32(randomBytes, 0);

                return result;
            }


            //Random rnd = new Random(); // system clock as seed value
            //Random rnd2 = new Random(42); // explicit seed value
            //Random rnd3 = new Random(42); // explicit seed value

            //int x = rnd2.Next();
            //int y = rnd3.Next();
            //int z = rnd.Next();

            //Random r1 = new Random();
            //Random r2 = new Random();

            //int r1Value = r1.Next();
            //int r2Value = r2.Next();


            //return rnd.Next();
        }
        private static List<int> GenerateDefaultWorkDays()
        {
            //var days = new List<int>();

            //for (int i = 1; i <= 5; i++)
            //{
            //    days.Add(i);
            //}

            //return days;

            return Enumerable.Range(1, 5).Select(x => x * 2).ToList();
        }

        private static void GetSkillsFor(Employee employee)
        {
            // Simulate getting skills from user-input
            employee.Skills.Add("C#");
            employee.Skills.Add("HTML");
            employee.Skills.Add("SQL");
            employee.Skills.Add("JSON");
        }

        private static void GetBioFor(Employee employee)
        {
            // Simulate getting bio from user-input
            employee.Bio = "A darn hard working employee, dash it the best we have.";
        }

        private static void DisplayEmployee(Employee employee)
        {
            WriteLine("Employee Details");
            WriteLine("----------------");
            WriteLine();

            //WriteLine("First Name: " + employee.FirstName + " Employee Code: " + employee.EmployeeCode);
            //string line = string.Format("First Name: {0} Employee Code: {1}", 
            //                            employee.FirstName, 
            //                            employee.EmployeeCode);
            //WriteLine(line);

            //WriteLine("First Name: {0} Employee Code: {1}", employee.FirstName, employee.EmployeeCode);

            //line = $"First Name: {employee.FirstName} Employee Code: {employee.EmployeeCode}";
            //WriteLine(line);

            WriteLine($"First Name: {employee.FirstName} Employee Code: {employee.EmployeeCode}");

            string theHarderWay = "First Name: " + employee.FirstName.PadRight(20) +
                                 " Employee Code: " + employee.EmployeeCode.ToString().PadRight(5);

            WriteLine(theHarderWay);

            string easier = string.Format("First Name: {0,-20} Employee Code: {1,-5}",
                                          employee.FirstName,
                                          employee.EmployeeCode);
            WriteLine(easier);

            WriteLine($"First Name: {employee.FirstName,-20} Employee Code: {employee.EmployeeCode,-5}");
            WriteLine($"First Name: {employee.FirstName,20} Employee Code: {employee.EmployeeCode,5}");

            WriteLine($"Productivity rating: {employee.ProductivityRating}");

            const string threePartFormat = "(good employee) #;(bad employee) -#;(new employee - no productivity recorded yet)";
            WriteLine(employee.ProductivityRating.ToString(threePartFormat));


            string skills = "";
            foreach (var skill in employee.Skills)
            {
                skills += $"{skill}, ";
            }

            WriteLine($"Skills: {skills}"); // ignore trailing comma

            var sb = new StringBuilder();

            foreach (var skill in employee.Skills)
            {
                sb.Append(skill);
                sb.Append(", ");
            }

            //WriteLine($"Skills: {sb.ToString()}");
            WriteLine($"Skills: {sb}");

            string prod = string.Format(new EmployeeProductivityFormatProvider(),
                                        "Productivity rating: {0}",
                                        employee.ProductivityRating);
            WriteLine(prod);

            WriteLine($"First Name: {employee.FirstName}");


            WriteLine($"DOB: {employee.DateOfBirth}");

            WriteLine($"Salary: {employee.Salary}");

            WriteLine($"Id: {employee.Id}");

            WriteLine($"Work Days: {string.Join(",", employee.WorkDays)}");
        }

        private static bool IsAllWhiteSpace(string s)
        {
            if (s.Replace(" ", "").Length == 0) // doesn't take into accounts tabs
            {
                return true;
            }

            return false;
        }
    }

        class EmployeeProductivityFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            int rating = (int)arg;

            if (rating == 0)
            {
                return $"{rating} (new employee)";
            }

            if (rating > 0)
            {
                return $"{rating} (good employee)";
            }

            return $"{rating} (bad employee)";
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }
    }
}

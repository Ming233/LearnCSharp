using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CSharp_TipsAndTrap
{


    partial class Part6_MethodTakesArgs
    {

        static void Part6_MethodTakesArgs_Main()
        {
            Project project1 = new Project { Name = "Better UI" };
            // Project project2 = new AgileProject { Name = "Better UI" };


            List<Person> team = new List<Person>
            {
                new Person("Sarah"),
                new Person("Amrit", 22),
                new Person("Anna", 42, "female")
            };

            DisplayTeam(team);

            WriteLine();
            DisplayPerson("The team:", team[0]);


            WriteLine();
            Display("First person in team", team[0]);

            WriteLine();
            Display("Project and leader", project1, team[0]);

            //WriteLine();
            //Display("Project and team", new object[] { project1, team[0], team[1], team[2] });

            WriteLine();
            Display("Project and team", project1, team[0], team[1], team[2], "hello");

            WriteLine();
            Display("The team", team.ToArray());

            WriteLine();
            WriteLine("Press enter to exit.");
            ReadLine();
        }

        private static void DisplayTeam(List<Person> team)
        {
            WriteLine("Team");
            WriteLine("----");
            foreach (var person in team)
            {
                WriteLine($"{person.Name,-20} {person.Age,-5} {person.Gender,-10}");
            }
        }

        private static void DisplayPerson(string title, Person person)
        {
            WriteLine(title);
            WriteLine($"{person.Name,-20} {person.Age,-5} {person.Gender,-10}");
        }

        private static void Display(string title, object o)
        {
            WriteLine(title);
            WriteLine(o);
        }

        private static void Display(string title, object o, object o2)
        {
            WriteLine(title);
            WriteLine(o);
            WriteLine(o2);
        }

        //private static void Display(string title, object[] objects)
        //{
        //    WriteLine(title);

        //    foreach (var o in objects)
        //    {
        //        WriteLine(o);
        //    }
        //}

        private static void Display(string title, params object[] objects)
        {
            WriteLine(title);

            foreach (var o in objects)
            {
                WriteLine(o);
            }
        }

    }

    class Person
    {
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }

        private const string DefaultGender = "default";

        //public Person(string name)
        //{
        //    Name = name;
        //    Age = -1;
        //    Gender = DefaultGender;
        //}

        //public Person(string name, int age)
        //{
        //    Name = name;
        //    Age = age;
        //    Gender = DefaultGender;
        //}

        public Person(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public Person(string name) : this(name, -1, DefaultGender) { }

        public Person(string name, int age) : this(name, age, DefaultGender) { }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Project
    {
        public string Name { get; set; }

        public Clock TimeLeft { get; set; } // Analog
        public DigitalClock TimeElapsed { get; set; } // Digital

        public Project()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Clock
    {
    }

    public class DigitalClock
    {
    }
}

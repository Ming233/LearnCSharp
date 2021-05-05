using System;
using System.Collections.Generic;

namespace CSharp_Generics
{
    public class Part2_Collection
    {
        public static void Tryictionary()
        {
            //play around the first, after, last, remove, etc
            Dictionary<string, Employee> employeeByname = new Dictionary<string, Employee>();
            employeeByname.Add("Scott", new Employee { Name = "Scott" });
            employeeByname.Add("Alex", new Employee { Name = "Alex" });
            employeeByname.Add("Ming", new Employee { Name = "Ming" });

            var scott = employeeByname["Scott"];


            foreach (var item in employeeByname)
            {
                Console.WriteLine("{0}:{1}", item.Key, item.Value.Name);
            }

            foreach (var item in employeeByname.Values)
            {
                Console.WriteLine("{0}", item.Name);
            }
        }

        public static void LinkedListCompare()
        {
            //play around the first, after, last, remove, etc
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddLast(4);

            var first = list.First;
            list.AddAfter(first, 5);
            list.AddBefore(first, 10);
            list.RemoveLast();

            var node = list.First;
            while (node != null)
            {
                Console.WriteLine(node.Value);
            }
        }

        public static void HashSetCompare()
        {
            //Hash set won't duplicate values
            HashSet<Employee> set = new HashSet<Employee>();
            var employee = new Employee { Name = "Scott" };
            set.Add(employee);
            set.Add(employee);
            //this will add to hashset
            set.Add(new Employee { Name = "Scott" });


            foreach (var item in set)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void Queue()
        {
            //first in first out
            Queue<Employee> line = new Queue<Employee>();
            line.Enqueue(new Employee { Name = "Scott" });
            line.Enqueue(new Employee { Name = "Ming" });
            line.Enqueue(new Employee { Name = "Alex" });

            while (line.Count > 0)
            {
                var employee = line.Dequeue();
                Console.WriteLine(employee.Name);
            }
        }

        public static void Stack()
        {
            //last in first out
            Stack<Employee> stack = new Stack<Employee>();
            stack.Push(new Employee { Name = "Scott" });
            stack.Push(new Employee { Name = "Ming" });
            stack.Push(new Employee { Name = "Alex" });

            while (stack.Count > 0)
            {
                var employee = stack.Pop();
                Console.WriteLine(employee.Name);
            }
        }
    }
}

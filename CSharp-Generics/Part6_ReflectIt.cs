using System;
using System.Collections.Generic;

namespace CSharp_Generics
{
    public class Part6_ReflectIt
    {
        public static void Part6_ReflectIt_Main()
        {
            var employeeList = CreateCollection(typeof(List<>), typeof(Employee_ReflectIt));
            Console.Write(employeeList.GetType().Name);
            var genericArguments = employeeList.GetType().GenericTypeArguments;
            foreach (var arg in genericArguments)
            {
                Console.Write("[{0}]", arg.Name);
            }
            Console.WriteLine();

            var employee = new Employee_ReflectIt();
            var employeeType = typeof(Employee_ReflectIt);
            var methodInfo = employeeType.GetMethod("Speak");
            methodInfo = methodInfo.MakeGenericMethod(typeof(DateTime));
            methodInfo.Invoke(employee, null);
        }

        private static object CreateCollection(Type collectionType, Type itemType)
        {
            var closedType = collectionType.MakeGenericType(itemType);
            return Activator.CreateInstance(closedType);
        }
    }
    public class Employee_ReflectIt
    {
        public string Name { get; set; }

        public void Speak<T>()
        {
            Console.WriteLine(typeof(T).Name);
        }
    }
}

using System;

namespace CSharp_Generics
{
    public class Part7_Tips
    {
        public static void Part7_Tips_Main()
        {
            //Generalt Constructor will add 1 each time. because it is using that static int
            var a = new Item1();
            var b = new Item1();
            var c = new Item1();
            Console.WriteLine("Method 1");
            Console.WriteLine(Item1.InstanceCount);

            //This contconstructor run sperartely. 
            var e = new Item2<int>();
            var f = new Item2<int>();
            var g = new Item2<string>();
            Console.WriteLine("Method 2");
            Console.WriteLine(Item2<int>.InstanceCount);
            Console.WriteLine(Item2<string>.InstanceCount);

            //This method count together because the Generic T
            var h = new Item3<int>();
            var i = new Item3<int>();
            var j = new Item3<string>();
            Console.WriteLine("Method 3");
            Console.WriteLine(Item3<int>.InstanceCount);
            Console.WriteLine(Item3.InstanceCount);

        }
    }


    public class Item1
    {
        public Item1()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount;
    }

    public class Item2<T>
    {
        public Item2()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount;
    }


    public class Item3<T> : Item3
    {
    }

    public class Item3
    {
        public Item3()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount;
    }

    #region Math
    class MathProblems
    {
        static void DoIt(string[] args)
        {
            var numbers = new double[] { 1, 2, 3, 4, 5, 6 };
            var result = SampledAverage(numbers);
            Console.WriteLine(result);
        }

        private static double SampledAverage(double[] numbers)
        {
            var count = 0;
            var sum = 0.0;
            for (int i = 0; i < numbers.Length; i += 2)
            {
                sum += numbers[i];
                count += 1;
            }
            return sum / count;
        }
    }
    #endregion

    #region enum
    public enum Steps
    {
        Step1,
        Step2,
        Step3
    }

    public static class StringExtensions
    {
        public static TEnum ParseEnum<TEnum>(this string value)
            where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }

    class Demo
    {
        static void DoIt()
        {
            var input = "Step1";
            var value = input.ParseEnum<Steps>();
            Console.WriteLine(value);

            //(Steps)Enum.Parse(typeof(Steps), input);

        }
    }
    #endregion

}

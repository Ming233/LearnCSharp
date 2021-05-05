using System;

namespace TestNewFeature
{

    public class ParticeDelegateThreeTypes
    {
        public static void PrintStuff(string someinformation)
        {
            Console.WriteLine(someinformation);
            //define an action, take d and do something with d.
            Action<bool> print = d => Console.WriteLine(d);
            //get a value d and use this value to time itself.
            Func<double, double> square = d => d * d;
            //get two values and use these two values to pluse togehter.
            Func<double, double, double> add = (x, y) => x + y;
            //ditermind this d value is greater or lower than ten.
            Predicate<double> isLessThenThen = d => d < 10;

            print(isLessThenThen(square(add(3, 5))));

        }

        public static void TryAction()
        {
            //Always return void, take 2-16 parameter
            Action<bool> print = d => Console.WriteLine("Trying action");
        }

        public static void TryFunc()
        {
            //Always return last parameter, take 2-16 parameter
            Func<double, double> square = d => d * d;
            Func<double, double, double> add = (x, y) => x + y;

        }

        public static void TryPredicate()
        {
            //Always return true or false
            Predicate<double> isLessThenThen = d => d < 10;
        }

        public static DateTime TryConverter(double someDoubleData)
        {
            //Always return true or false
            Converter<double, DateTime> converter = d => new DateTime(2010, 1, 1);

            DateTime returnData;

            return returnData = converter(someDoubleData);

        }
    }
}

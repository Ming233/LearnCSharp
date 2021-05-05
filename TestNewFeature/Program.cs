using System;

namespace TestNewFeature
{
    class Program : ParticeInterface
    {

        static void Main(string[] args)
        {
            //AbstructMethod();

            //Interface tesing
            //I3rdLevelInterface iImp = new Program();
            //iImp.execute3rdMethod();
            //iImp.execute2ndMethod();
            //iImp.execute1stMethod();

            //printer cousoleout = new printer(consolewrite);
            //cousoleout("printer ");


            //ParticeDelegateThreeTypes.PrintStuff("Three types delegate");

            //DateTime testDemo = ParticeDelegateThreeTypes.TryConverter(22.2);
            //Console.WriteLine(testDemo);

            My_book book = new My_book();
            book.IntFirst = 1;
            book.IntSecond = 2;
            book.IntThird = 3;
            Calculator cal = new Calculator();
            var result = cal.Accumulate(book);
            Calculator cal2 = new Calculator();
            var result2 = cal2.Accumulate(book);
            Console.WriteLine(result2.Max);

            Console.ReadLine();
        }

        static void ConsoleWrite(object data)
        {
            Console.WriteLine(data);
        }

        public static void AbstructMethod()
        {
            //Not working yet
            //    public readonly A1stAbstruct _loader;
            //public readonly List<ListofData> _listofData;
            //_loader = new A2ndAbstuct(_listofData);
        }

        public void execute1stMethod()
        {
            Console.WriteLine("1st MethodToImplement() called.");
        }

        public void execute2ndMethod()
        {
            Console.WriteLine("2nd MethodToImplement() called.");
        }

        public void execute3rdMethod()
        {
            Console.WriteLine("3rd MethodToImplement() called.");
        }


        public class Calculator
        {
            public Calculator()
            {
                Max = Int32.MinValue;
                Min = Int32.MaxValue;
            }

            public Calculator Accumulate(My_book somdData)
            {
                Count += 1;
                Total += somdData.IntFirst + somdData.IntSecond;
                Max = Math.Max(Max, somdData.IntFirst);
                Min = Math.Min(Min, somdData.IntSecond);
                return this;
            }

            public Calculator Compute()
            {
                Average = Total / Count;
                return this;
            }

            public int Max { get; set; }
            public int Min { get; set; }
            public int Total { get; set; }
            public int Count { get; set; }
            public double Average { get; set; }

        }

        public class My_book
        {
            public int IntFirst { get; set; }
            public int IntSecond { get; set; }
            public int IntThird { get; set; }
            public string StringFirst { get; set; }
            public double DoubleFirst { get; set; }
        }

    }
}

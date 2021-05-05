using System;
using System.Diagnostics;

namespace CSharpFunddamental2
{
    public class Timekeeper
    {
        public TimeSpan Measure(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            action();
            return watch.Elapsed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DatesAndTimeInDotNet
{
    public class Part3_Arithmetic
    {
        private static Calendar calendar = CultureInfo.InvariantCulture.Calendar;

        public static void Part3_Arithmetic_Main()
        {
            #region Calculating time difference and working with TimeSpan 
            var start = DateTimeOffset.UtcNow;
            var end = start.AddSeconds(45);

            TimeSpan difference = end - start;

            difference = difference.Multiply(2);

            Console.WriteLine(difference.TotalMinutes);
            #endregion

            #region Getting the week number 
            start = new DateTimeOffset(2007, 12, 31, 0, 0, 0, TimeSpan.Zero);

            var week = calendar.GetWeekOfYear(start.DateTime,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);

            Console.WriteLine(week);

            // Only for .NET Core 3.0
            // var isoWeek = ISOWeek.GetWeekOfYear(start.DateTime);

            // Console.WriteLine(isoWeek);

            var isoWeekHack = GetIso8601WeekOfYear(start.DateTime);

            Console.WriteLine(isoWeekHack);
            #endregion

            #region Extending a date 
            var contractDate = new DateTimeOffset(2019, 7, 1, 0, 0, 0, TimeSpan.Zero);

            contractDate = contractDate.AddMonths(6).AddTicks(-1);

            Console.WriteLine(contractDate);

            contractDate = new DateTimeOffset(2020, 2, 29, 0, 0, 0, TimeSpan.Zero);

            Console.WriteLine(contractDate);

            contractDate = contractDate.AddMonths(1);

            Console.WriteLine(contractDate);
            #endregion
        }

        public static DateTimeOffset ExtendContract(DateTimeOffset current, int months)
        {
            Console.WriteLine(current);
            var future = current.AddMonths(months).AddTicks(-1);
            Console.WriteLine(future);


            return new DateTimeOffset(future.Year,
                future.Month,
                DateTime.DaysInMonth(future.Year, future.Month),
                23, 59, 59,
                current.Offset);
        }

        // Code snippet from: https://blogs.msdn.microsoft.com/shawnste/2006/01/24/iso-8601-week-of-year-format-in-microsoft-net/
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }
    }
}

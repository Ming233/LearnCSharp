using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DatesAndTimeInDotNet
{
    public class Part2_StockAnalyzer
    {
        public static void Part2_StockAnalyzer_Main()
        {
            #region Parsing a date and time given a specific format
            var date = "9/10/2019 10:00:00 PM";

            var parsedDate = DateTimeOffset.ParseExact(date,
                "M/d/yyyy h:mm:ss tt",
                CultureInfo.InvariantCulture);

            Console.WriteLine("Prase Date: " + parsedDate);
            #endregion

            #region Formatting a date and time as ISO 8601
            Console.WriteLine("Prase Date to string: " + parsedDate.ToString("o"));
            #endregion

            #region Finding time zones for a given offset
            var now = DateTimeOffset.Now;

            Console.WriteLine("Get system time zone");
            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                if (timeZone.GetUtcOffset(now) == now.Offset)
                {
                    Console.WriteLine(timeZone);
                }
            }

            #endregion

            var time = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(10));

            Console.WriteLine("Get system time zone2");
            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                if (timeZone.GetUtcOffset(time) == time.Offset)
                {
                    Console.WriteLine(timeZone);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQFundamental
{
    public class Part3_Queries
    {
        public static void Part3_Queries_Main()
        {
            var numbers = Part3_MyLinq.Random().Where(n => n > 0.5).Take(10).OrderBy(n => n);
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var movies = new List<Part3_Movie>
            {
                new Part3_Movie { Title = "The Dark Knight",   Rating = 8.9f, Year = 2008 },
                new Part3_Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Part3_Movie { Title = "Casablanca",        Rating = 8.5f, Year = 1942 },
                new Part3_Movie { Title = "Star Wars V",       Rating = 8.7f, Year = 1980 }
            };

            var query1 = movies.Where(m => m.Year > 2000);

            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movie;

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }

    static class Part3_MyLinq
    {
        public static IEnumerable<double> Random()
        {
            var random = new Random();
            while (true)
            {
                yield return random.NextDouble();
            }
        }

        private static IEnumerable<T> Part3_Filter<T>(this IEnumerable<T> source,
                                               Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }

    class Part3_Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }

        int _year;
        public int Year
        {
            get
            {
                Console.WriteLine($"Returning {_year} for {Title}");
                return _year;
            }
            set
            {
                _year = value;
            }
        }
    }
}

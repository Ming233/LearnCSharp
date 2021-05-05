using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQFundamental
{
    public class Part4_Queries
    {
        public static void Part4_Queries_Main()
        {
            var numbers = Part4_MyLinq.Random().Where(n => n > 0.5).Take(10).OrderBy(n => n);
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }


            var movies = new List<Part4_Movie>
            {
                new Part4_Movie { Title = "The Dark Knight",   Rating = 8.9f, Year = 2008 },
                new Part4_Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Part4_Movie { Title = "Casablanca",        Rating = 8.5f, Year = 1942 },
                new Part4_Movie { Title = "Star Wars V",       Rating = 8.7f, Year = 1980 }
            };


            Console.WriteLine("Using normal return, but it will loot through all values.");
            IEnumerable<Part4_Movie> query1 = movies.Part4_Filter1(m => m.Year > 2000);
            Console.WriteLine("Using yeild return");
            IEnumerable<Part4_Movie> query2 = movies.Part4_Filter2(m => m.Year > 2000);


            Console.WriteLine("Using query syntax in select");
            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movie;
            var enumerator = query.GetEnumerator();

            var query3 = movies.Part4_Filter2(m => m.Year > 2000).ToList();
            var enumerator3 = query3.GetEnumerator();

            Console.WriteLine("Count Query: " + query.Count());
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }

    public static class Part4_MyLinq
    {

        public static IEnumerable<double> Random()
        {
            var random = new Random();
            while (true)
            {
                yield return random.NextDouble();
            }
        }

        public static IEnumerable<T> Part4_Filter1<T>(this IEnumerable<T> source,
                                               Func<T, bool> predicate)
        {
            var result = new List<T>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        //using yeild return. Yeild will build IErumable automatcailly
        public static IEnumerable<T> Part4_Filter2<T>(this IEnumerable<T> source,
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

    class Part4_Movie
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

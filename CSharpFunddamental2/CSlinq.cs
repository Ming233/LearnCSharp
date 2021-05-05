using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSharpFunddamental2
{
    public class CSlinq
    {
        #region CSlinq start here
        public void CSlinqMethod()
        {
            QueryCities();

            WorkWithFuncs();

            MovieDB db = new MovieDB();


            IEnumerable<Movie> query =
                db.Movies.Where(m => m.Title.StartsWith("Star"))
                    .OrderBy(m => m.ReleaseDate.Year);

            var query2 =
                from m in db.Movies
                where m.Title.StartsWith("L")
                select m;

            //foreach (var movie in query2)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            Console.ReadLine();
        }

        private static void QueryCities()
        {
            IEnumerable<string> cities = new[] { "Guang ZHou", "Hong Kong", "London", "Las Vegas", "Edmonton", "Ghent", "London", "Las Vegas", "Hyderabad" };

            IEnumerable<string> query =
                cities.Where(city => city.StartsWith("G"))
                    .OrderByDescending(city => city.Length);

            foreach (var city in query)
            {
                Console.WriteLine(city);
            }
        }

        private static void WorkWithFuncs()
        {
            //get int and return int
            Expression<Func<int, int>> square = x => x * x;
            //get x & y. then return int
            Func<int, int, int> add = (x, y) => x + y;
            //Action get value, but doesn't return anything
            Action<int> write = x => Console.WriteLine(x);

            //write(square(add(1,3)));
        }
        #endregion 

    }
}

namespace Extensions
{
    public static class FilterExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> input,
            Func<T, bool> predicate)
        {
            foreach (var item in input)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
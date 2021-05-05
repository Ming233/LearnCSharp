using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LINQFundamental
{
    public class Part5_Cars
    {
        public static void Part5_Cars_Main()
        {
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var top = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                             .OrderByDescending(c => c.Name)
                             .Select(c => c)
                             .First();
            Console.WriteLine("Top BMW Car: " + top.Name);

            var JoinQuery1 = from car in cars
                             join manufacturer in manufacturers
                             on car.Manufacturer equals manufacturer.Name
                             orderby car.Combined descending, car.Name ascending
                             select new
                             {
                                 manufacturer.Headquarters,
                                 car.Name,
                                 car.Combined
                             };

            Console.WriteLine("*********************Using Join 1 to get the query********************");
            foreach (var car in JoinQuery1)
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
            Console.WriteLine("*****************End - Using Join 1 to get the query****************");


            var JoinQuery2 =
                cars.Join(manufacturers,
                            c => c.Manufacturer,
                            m => m.Name, (c, m) => new
                            {
                                m.Headquarters,
                                c.Name,
                                c.Combined
                            })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            Console.WriteLine("*********************Using extension syntax to get the query********************");
            foreach (var car in JoinQuery2)
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
            Console.WriteLine("*****************End - Using extension syntax 2 to get the query****************");


            var query =
                from car in cars
                group car by car.Manufacturer into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into result
                orderby result.Max descending
                select result;

            foreach (var result in query)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Avg}");
            }


            var query2 =
                cars.GroupBy(c => c.Manufacturer)
                    .Select(g =>
                    {
                        var results = g.Aggregate(new Part5_CarStatistics(),
                                            (acc, c) => acc.Accumulate(c),
                                            acc => acc.Compute());
                        return new
                        {
                            Name = g.Key,
                            Avg = results.Average,
                            Min = results.Min,
                            Max = results.Max
                        };
                    })
                    .OrderByDescending(r => r.Max);

            foreach (var result in query2)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Avg}");
            }
        }

        private static List<Part5_Car> ProcessCars(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .Part5_ToCar();

            //var query2 =
            //    from line in File.ReadAllLines(path).Skip(1)
            //             Where line.Length > 1
            //             Select Part5_Car.ParseFromCsv(line);


            return query.ToList();
        }

        private static List<Part5_Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                   File.ReadAllLines(path)
                       .Where(l => l.Length > 1)
                       .Select(l =>
                       {
                           var columns = l.Split(',');
                           return new Part5_Manufacturer
                           {
                               Name = columns[0],
                               Headquarters = columns[1],
                               Year = int.Parse(columns[2])
                           };
                       });
            return query.ToList();
        }
    }

    public class Part5_CarStatistics
    {
        public Part5_CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public Part5_CarStatistics Accumulate(Part5_Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public Part5_CarStatistics Compute()
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

    public static class Part5_CarExtensions
    {
        public static IEnumerable<Part5_Car> Part5_ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Part5_Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }

    public class Part5_Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Displacement { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }
    }

    public class Part5_Manufacturer
    {
        public string Name { get; set; }
        public string Headquarters { get; set; }
        public int Year { get; set; }
    }
}

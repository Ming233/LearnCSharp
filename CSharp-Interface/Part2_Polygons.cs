using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Interface
{
    public class Part2_Polygons
    {
        public static void Part2_Polygons_Main()
        {
            var square = new Square(5);
            DisplayPolygon("Square", square);

            var triangle = new Triangle(5);
            DisplayPolygon("Triangle", triangle);

            var octagon = new Octagon(5);
            DisplayPolygon("Octagon", octagon);

            Console.Read();
        }

        public static void DisplayPolygon(string polygonType, dynamic polygon)
        {
            try
            {
                Console.WriteLine($"{polygonType} Number of Sides: {polygon.NumberOfSides}");
                Console.WriteLine($"{polygonType} Side Length: {polygon.SideLength}");
                Console.WriteLine($"{polygonType} Perimeter: {polygon.GetPerimeter()}");
                Console.WriteLine($"{polygonType} Area: {Math.Round(polygon.GetArea(), 2)}");
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Exception was thrown while trying to process {polygonType}:\n   {ex.GetType().Name}");
                Console.WriteLine();
            }
        }
    }
}

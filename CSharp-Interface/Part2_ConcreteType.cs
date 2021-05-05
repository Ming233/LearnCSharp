using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Interface
{
    //Concrete Type Polygon
    public class Square : ConcreteRegularPolygon
    {
        public Square(int length) :
            base(4, length)
        { }

        //public override double GetArea()
        //{
        //    return SideLength * SideLength;
        //}
    }

    public class ConcreteRegularPolygon
    {
        public int NumberOfSides { get; set; }
        public int SideLength { get; set; }

        public ConcreteRegularPolygon(int sides, int length)
        {
            NumberOfSides = sides;
            SideLength = length;
        }

        public double GetPerimeter()
        {
            return NumberOfSides * SideLength;
        }

        public virtual double GetArea()
        {
            Console.WriteLine("haha");
            throw new NotImplementedException();
        }
    }
}

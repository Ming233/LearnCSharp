using System;
using NUnit.Framework;

namespace ErrorHandlingWithException.Tests.DotNetFramework
{
    public class CalculatorShould_NUnit
    {
        [Test]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Part7_Calculator();

            Assert.That(() => sut.Calculate(1, 1, "+"),
                Throws.TypeOf<Part7_CalculationOperationNotSupportedException>());

            Assert.That(() => sut.Calculate(1, 1, "+"),
                Throws.TypeOf<Part7_CalculationOperationNotSupportedException>()
                      .With
                      .Property("Operation").EqualTo("+"));

            //Assert.That(() => sut.Calculate(1, 1, "+"), 
            //    Throws.TypeOf<CalculationException>());

            Assert.That(() => sut.Calculate(1, 1, "+"),
                Throws.InstanceOf<Part7_CalculationException>());


            Assert.Throws<Part7_CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            var ex = Assert.Throws<Part7_CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.That(ex.Operation, Is.EqualTo("+"));
        }
    }
}

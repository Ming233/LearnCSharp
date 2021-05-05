using ErrorHandlingWithException;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorHandllingWithException.Tests
{
    [TestClass]
    public class CalculatorShould
    {
        [TestMethod]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Part7_Calculator();

            Assert.ThrowsException<Part7_CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            //Assert.ThrowsException<CalculationException>(
            //  () => sut.Calculate(1, 1, "+"));


            //Check Operation
            var ex = Assert.ThrowsException<Part7_CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.AreEqual("+", ex.Operation);
        }
    }
}

using Xunit;

namespace ErrorHandlingWithException.Tests.DotNetFramework
{
    public class CalculatorShould_XUnit
    {
        [Fact]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Part7_Calculator();

            Assert.Throws<Part7_CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            //Assert.Throws<CalculationException>(
            //    () => sut.Calculate(1, 1, "+"));

            Assert.ThrowsAny<Part7_CalculationException>(
                () => sut.Calculate(1, 1, "+"));

            var ex = Assert.Throws<Part7_CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.Equal("+", ex.Operation);
        }
    }
}

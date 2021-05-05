using System;
using Xunit;

namespace CSharp_TipsAndTrap.Part4
{
    public class ObjectReferenceEqualsExample
    {
        [Fact]
        public void ExampleWhereReferenceTypeUsesValueEqualitySemantics()
        {
            Uri a = new Uri("https://pluralsight.com");
            Uri b = new Uri("https://pluralsight.com");

            var areEqual = a == b;

            bool isSameReference = object.ReferenceEquals(a, b);

            b = a;

            isSameReference = object.ReferenceEquals(a, b);
        }
    }


}

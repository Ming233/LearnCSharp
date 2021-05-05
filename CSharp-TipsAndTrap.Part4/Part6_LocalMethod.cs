using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

namespace CSharp_TipsAndTrap.Part4
{
    class Part6_LocalMethod
    {
        public List<string> ToUpperAndWithLength(List<string> stringsToProcess)
        {
            var results = new List<string>();

            foreach (var s in stringsToProcess)
            {
                //var result = Process(s);

                //results.Add($"{result.Length}-{result.UppercaseVersion}");

                Process(s);
            }

            return results;

            //This is local method which will only be used once and connect to local directly.
            void Process(string s)
            {
                results.Add($"{s.Length}-{s.ToUpperInvariant()}");
            }
        }

        //private (int Length, string UppercaseVersion) Process(string s)
        //{
        //    return (s.Length, s.ToUpperInvariant());
        //}

    }

    public class Part6_LocalMethodShould
    {
        [Fact]
        public void ReturnLengthsAndUppercaseStrings()
        {
            var sut = new StringProcessor();

            var inputStrings = new List<string>
            {
                "Hello",
                "Welcome",
                "Howdy"
            };

            var results = sut.ToUpperAndWithLength(inputStrings);

            Assert.Equal(3, results.Count);

            Assert.Equal("5-HELLO", results[0]);
            Assert.Equal("7-WELCOME", results[1]);
            Assert.Equal("5-HOWDY", results[2]);
        }
    }
}

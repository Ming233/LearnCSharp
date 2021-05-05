using System;
using System.Collections.Generic;
using Xunit;

namespace CSharp_TipsAndTrap.Part4
{
    public class AdvancedStringProcessorV1Should
    {
        private AdvancedStringProcessorV1 sut = null;
        private List<string> inputStrings;

        public AdvancedStringProcessorV1Should()
        {
            sut = new AdvancedStringProcessorV1();
            inputStrings = new List<string> { "Hello", "Welcome", "Howdy" };
        }

        [Fact]
        public void ProcessNone()
        {
            var results = sut.Process(inputStrings, StringProcessingOptionsV1.None);

            Assert.Equal(3, results.Count);

            Assert.Equal("Hello", results[0]);
            Assert.Equal("Welcome", results[1]);
            Assert.Equal("Howdy", results[2]);
        }

        [Fact]
        public void ProcessAddLength()
        {
            var results = sut.Process(inputStrings, StringProcessingOptionsV1.AddLength);

            Assert.Equal(3, results.Count);

            Assert.Equal("5-Hello", results[0]);
            Assert.Equal("7-Welcome", results[1]);
            Assert.Equal("5-Howdy", results[2]);
        }

        [Fact]
        public void ProcessConvertToUppercase()
        {
            var results = sut.Process(inputStrings,
                                      StringProcessingOptionsV1.ConvertToUppercase);

            Assert.Equal(3, results.Count);

            Assert.Equal("HELLO", results[0]);
            Assert.Equal("WELCOME", results[1]);
            Assert.Equal("HOWDY", results[2]);
        }
    }



    public enum StringProcessingOptionsV1
    {
        None,
        ConvertToUppercase,
        AddLength
    }

    public class AdvancedStringProcessorV1
    {
        public List<string> Process(List<string> stringsToProcess,
                                    StringProcessingOptionsV1 options)
        {
            var results = new List<string>();

            foreach (var s in stringsToProcess)
            {
                switch (options)
                {
                    case StringProcessingOptionsV1.None:
                        results.Add(s);
                        break;
                    case StringProcessingOptionsV1.ConvertToUppercase:
                        results.Add(s.ToUpperInvariant());
                        break;
                    case StringProcessingOptionsV1.AddLength:
                        results.Add($"{s.Length}-{s}");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return results;
        }
    }
}

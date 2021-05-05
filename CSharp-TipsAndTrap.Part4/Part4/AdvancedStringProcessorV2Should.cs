using System.Collections.Generic;
using Xunit;
using System;

namespace CSharp_TipsAndTrap.Part4
{
    public class AdvancedStringProcessorV2Should
    {
        private AdvancedStringProcessorV2 sut = null;
        private List<string> inputStrings;

        public AdvancedStringProcessorV2Should()
        {
            sut = new AdvancedStringProcessorV2();
            inputStrings = new List<string> { "Hello", "Welcome", "Howdy" };
        }

        [Fact]
        public void ProcessNone()
        {
            var results = sut.Process(inputStrings, StringProcessingOptionsV2.None);

            Assert.Equal(3, results.Count);

            Assert.Equal("Hello", results[0]);
            Assert.Equal("Welcome", results[1]);
            Assert.Equal("Howdy", results[2]);
        }

        [Fact]
        public void ProcessAddLength()
        {
            var results = sut.Process(inputStrings, StringProcessingOptionsV2.AddLength);

            Assert.Equal(3, results.Count);

            Assert.Equal("5-Hello", results[0]);
            Assert.Equal("7-Welcome", results[1]);
            Assert.Equal("5-Howdy", results[2]);
        }

        [Fact]
        public void ProcessConvertToUppercase()
        {
            var results = sut.Process(inputStrings,
                                      StringProcessingOptionsV2.ConvertToUppercase);

            Assert.Equal(3, results.Count);

            Assert.Equal("HELLO", results[0]);
            Assert.Equal("WELCOME", results[1]);
            Assert.Equal("HOWDY", results[2]);
        }

        [Fact]
        public void ProcessConvertToUppercaseAndAddLength()
        {
            var results = sut.Process(inputStrings, StringProcessingOptionsV2.All);

            Assert.Equal(3, results.Count);

            Assert.Equal("5-HELLO", results[0]);
            Assert.Equal("7-WELCOME", results[1]);
            Assert.Equal("5-HOWDY", results[2]);
        }
    }

    [Flags]
    public enum StringProcessingOptionsV2
    {
        None = 0,
        ConvertToUppercase = 1,
        AddLength = 2,

        All = ConvertToUppercase | AddLength
    }
    public class AdvancedStringProcessorV2
    {
        public List<string> Process(List<string> stringsToProcess,
                                    StringProcessingOptionsV2 options)
        {
            bool noProcessingIsRequired = options == StringProcessingOptionsV2.None;
            bool conversionToUppercaseIsRequired =
                (options & StringProcessingOptionsV2.ConvertToUppercase) != 0;
            bool addingLengthIsRequired =
                options.HasFlag(StringProcessingOptionsV2.AddLength);

            var processedStrings = new List<string>();

            foreach (var originalString in stringsToProcess)
            {
                string temp = "";

                if (noProcessingIsRequired)
                {
                    processedStrings.Add(originalString);
                    continue;
                }

                if (addingLengthIsRequired)
                {
                    temp += $"{originalString.Length}-";
                }

                if (conversionToUppercaseIsRequired)
                {
                    temp += originalString.ToUpperInvariant();
                }
                else
                {
                    temp += originalString;
                }

                processedStrings.Add(temp);
            }

            return processedStrings;
        }
    }
}

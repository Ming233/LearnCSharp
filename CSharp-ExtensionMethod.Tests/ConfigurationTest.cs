using CSharp_ExtensionMethod.Stand;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_ExtensionMethod.Tests
{
    class ConfigurationTests
    {
        [Test]
        public void IsLoaded()
        {
            //...
            IConfiguration config = null;
            Assert.IsFalse(config.IsLoaded());
        }

        [Test]
        public void AddStandardProviders()
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddStandardProviders().Build();
            Assert.AreEqual(4, config.Providers.Count());
            Assert.IsTrue(config.IsLoaded());
        }
    }

    public class EnumerableTests
    {
        IEnumerable<string> _strings;

        [SetUp]
        public void Setup()
        {
            _strings = new List<string> { "a", "b", "c" };
        }

        [Test]
        public void Count()
        {
            Assert.AreEqual(3, _strings.Count());

            var list = new StringList();
            Assert.AreEqual(0, list.Count());
        }
    }

    public class StringList : List<string>
    {
    }

    class TargetTests
    {
        [Test]
        public void StandardizedId()
        {
            var obj = new Target("id01");
            Assert.AreEqual("ID01", obj.GetStandardizedId());
        }
    }
}
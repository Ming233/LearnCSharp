using NUnit.Framework;
using System;
using Moq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace CSharp_ExtensionMethod.Tests
{
    public class LoginFailedEventTests
    {
        IMessageQueue _queue;

        [SetUp]
        public void Setup()
        {
            _queue = new Mock<IMessageQueue>().Object;
        }

        [Test]
        public void LoginFailed()
        {
            var loginFailed = new LoginFailedEvent
            {
                UserName = "sixeyed",
                FailedAt = DateTime.Now
            };
            var message = loginFailed.Wrap();
            _queue.Publish(message);
        }
    }

    public class Envelope
    {
        public string Subject { get; set; }

        public byte[] Body { get; set; }
    }
    public interface IMessageQueue
    {
        void Publish(Envelope message);
    }
    public interface IMessage
    {
    }
    public class LoginFailedEvent : IMessage
    {
        public string UserName { get; set; }

        public DateTime FailedAt { get; set; }
    }

    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Returns whether any configuration settings have been loaded
        /// </summary>
        /// <param name="config">Configuration object</param>
        /// <returns>True if any settings loaded</returns>
        public static bool IsLoaded(this IConfiguration config)
        {
            return config != null && config.AsEnumerable().Any();
        }

        /// <summary>
        /// Adds the standard config providers - JSON file, environment variables, config override & secrets override
        /// </summary>
        /// <param name="configBuilder">Configuration builder</param>
        /// <returns>Configuration builder</returns>
        public static IConfigurationBuilder AddStandardProviders(this IConfigurationBuilder configBuilder)
        {
            return configBuilder.AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .AddJsonFile("config/config.json", optional: true)
                                .AddJsonFile("secrets/secrets.json", optional: true);
        }
    }

    public static class MessageExtensions
    {
        /// <summary>
        /// Serliazes a message body and wraps it in an envelope for sending
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="message">Message to wrap</param>
        /// <returns>Wrapped message</returns>
        public static Envelope Wrap<TBody>(this TBody message)
            where TBody : IMessage
        {
            var json = JsonConvert.SerializeObject(message);
            return new Envelope
            {
                Subject = message.GetType().FullName,
                Body = Encoding.Unicode.GetBytes(json)
            };
        }
    }
}

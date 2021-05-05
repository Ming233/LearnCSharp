using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WorkingWithNull_Core
{
   public  class Part5_CS8NullDemos
    {
        public static void Part5_CS8NullDemos_Main()
        {

            Message message = new Message
            {
                Text = null!,
                From = null
            };

            MessagePopulator.Populate(message);

            Console.WriteLine(message.Text);
            Console.WriteLine(message.From);
            Console.WriteLine(message.From!.Length);
            Console.WriteLine(message.ToUpperFrom());

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
    static class MessagePopulator
    {
        public static void Populate(Message message)
        {
            message.GetType().InvokeMember("From",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                Type.DefaultBinder, message, new[] { "Jason (set using reflection)" });
        }
    }
    class Message
    {
        public string? From { get; set; }
        public string Text { get; set; } = "";
        public string? ToUpperFrom() => From?.ToUpperInvariant();
    }
}

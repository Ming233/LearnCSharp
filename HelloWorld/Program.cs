using System;
using System.Speech.Synthesis;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SayHello();
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Hello, World");//This speaker need comma to work.


            Console.ReadLine();
        }

        //public static void SayHello()
        //{
        //    DateTime date = new DateTime(2020, 1, 01);
        //    string name = " Ming ";
        //    name.Trim();

        //    Console.WriteLine("Hello World!" + name + " Created on " + DateTime.Now + " for testing");
        //    Console.ReadLine();
        //}

        //This one cannot be used as method
        public void SayHello()
        {
            DateTime date = new DateTime(2020, 1, 01);
            string name = " Ming ";
            name.Trim();

            Console.WriteLine("Hello World!" + name + " Created on " + DateTime.Now + " for testing");
            Console.ReadLine();
        }

    }
}

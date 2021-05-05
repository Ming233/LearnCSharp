using System.Speech.Synthesis;

namespace CSharpFundamental1
{
    public static class SpeechSpeaker
    {
        //This is static method
        public static void Speaker()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Hello, World");//This speaker need comma to work.
        }
    }
}

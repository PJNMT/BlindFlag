using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Game
{
    class Program
    {
        static void Synthesis(string S)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SelectVoice("Microsoft Hortense Desktop");
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak(S);
        }

        static void Main() 
        {
            StT speech = new StT(new Choices("tribord", "babord", "a", "couvert", "coco", "ok", "tous"));
            string n = speech.GetSpeech(5000);
            Console.WriteLine(n);
            Synthesis(n);
        }
    }
}
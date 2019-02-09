using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

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
            StT speech = new StT();
            string n = speech.GetSpeech(5000);
            Console.WriteLine(n);
            Synthesis(n);
        }
    }
}
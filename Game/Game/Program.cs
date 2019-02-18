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
            // TODO
        }
    }
}
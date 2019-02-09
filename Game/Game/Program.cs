using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Game
{
    class Program
    {
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
        }

        static void RV()
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("fr-FR")))
            {
                recognizer.LoadGrammar(new DictationGrammar());

                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                recognizer.SetInputToDefaultAudioDevice();

                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        static void SV(string S)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();

            synth.SetOutputToDefaultAudioDevice();

            synth.Speak(S);
        }



        static void Main(string[] args)
        {
            SV("Bonjour les amis !");
            RV();
        }
  

    }

    internal class SpeechRecognizedEventArgs
    {
    }
}
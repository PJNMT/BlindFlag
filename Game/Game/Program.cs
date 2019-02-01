using System;
using System.Speech.Recognition;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
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
  
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
        }
    }
}
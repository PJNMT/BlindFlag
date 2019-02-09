using System;
using System.Speech.Recognition;
using System.Threading;

namespace Game
{
    class StT
    {
        private static string speech;
        private GrammarBuilder Word;
        private static Grammar Dico;

        public StT()
        {
            speech = "";            
            Word = new GrammarBuilder(new Choices("tribord", "babord", "a", "couvert", "coco"));
            Word.Culture = new System.Globalization.CultureInfo("fr-FR");
            Dico = new Grammar(Word);
        }

        public string GetSpeech(int time)
        {
            speech = "";
            RV(time);
            return speech;
        }

        private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            speech += " " + e.Result.Text;
            Console.WriteLine("Texte reconu: " + speech);            
        }

        private static void RV(int time)
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("fr-FR")))
            {
                recognizer.LoadGrammar(Dico);
                recognizer.SpeechRecognized += recognizer_SpeechRecognized;
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                Thread.Sleep(time);
            }
        }
    }
}

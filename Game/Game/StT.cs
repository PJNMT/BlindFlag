using System;
using System.Speech.Recognition;
using System.Threading;

namespace Game
{


    /*
     MANUEL D'UTILISATION

        - Créer un nouvel objet StT : StT MyRecognitionObject = new StT(Choices("...", "...", "...", etc));
        (les "..." dans le Choices sont les mots que le StT pourra reconnaitre)

        - Lancer la reconnaissance vocale et récuperer le string : string MySpeech = MyRecognitionObject.GetSpeech(time);
        (time est un entier qui représente le temps que vous voulez accorder au StT pour reconnaitre se que le joueur dit. /!\ time est en seconde /!\)
    */




    class StT
    {
        private static string speech;
        private GrammarBuilder Word;
        private static Grammar Dico;

        public StT(Choices WordsRecognition)
        {
            speech = "";            
            Word = new GrammarBuilder(WordsRecognition);
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

        private static void RV(int time) // time est en secondes
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("fr-FR")))
            {
                recognizer.LoadGrammar(Dico);
                recognizer.SpeechRecognized += recognizer_SpeechRecognized;
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                {
                    waithandler.WaitOne(TimeSpan.FromSeconds(1));
                    time -= 1;
                } while (time > 0);
            }
        }
    }
}

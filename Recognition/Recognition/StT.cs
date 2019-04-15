using System;
using System.IO;
using System.Speech.Recognition;
using System.Threading;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Recognition
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
        private static bool nb;

        private static TcpClient tcpclnt;
        private static ASCIIEncoding asen;
        private static Stream stm;
        private static byte[] ba;

        public StT(Choices WordsRecognition)
        {
            speech = "";

            Word = new GrammarBuilder(WordsRecognition);
            Word.Culture = new System.Globalization.CultureInfo("fr-FR");
            Dico = new Grammar(Word);

            try
            {
                tcpclnt = new TcpClient();
                asen = new ASCIIEncoding();

                tcpclnt.Connect("localhost", 8052);
                Console.WriteLine("Connecting.....");

                stm = tcpclnt.GetStream();                
                Console.WriteLine("Connected");

                Console.WriteLine();
                Console.WriteLine("Parlez...");
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        public string GetSpeech(int time)
        {
            speech = "";
            nb = time == 0;
            RV(time);
            return speech;
        }

        private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            speech = e.Result.Text;
            Console.WriteLine(speech);

            try
            {
                ba = asen.GetBytes(speech);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);
                Console.WriteLine("Done!");

                int wait = 2;

                EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                {
                    waithandler.WaitOne(TimeSpan.FromSeconds(1));
                    wait -= 1;
                } while (wait > 0);

                Console.WriteLine();
                Console.WriteLine("Parlez...");
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        private static void RV(int time) // time est en secondes
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("fr-FR")))
            {

                recognizer.LoadGrammar(Dico);
                recognizer.SpeechRecognized += recognizer_SpeechRecognized;
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                if (!nb)
                {
                    EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                    {
                        waithandler.WaitOne(TimeSpan.FromSeconds(1));
                        time -= 1;
                    } while (time > 0);
                }
                else
                {
                    EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                    {
                        waithandler.WaitOne(TimeSpan.FromSeconds(1));
                    } while (true);
                }

                tcpclnt.Close();
            }
        }
    }
}

using System;
using System.IO;
using System.Speech.Recognition;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
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
        static TcpListener tcpListener;
        static Thread tcpListenerThread;
        static TcpClient connectedTcpClient;
        
        private static string speech;
        private GrammarBuilder Word;
        private static Grammar Dico;
        private static int mode;

        private static TcpClient tcpclnt;
        private static ASCIIEncoding asen;
        private static Stream stm;
        private static byte[] ba;

        private static int WaitTime;
        
        static WindowsMicrophoneMuteLibrary.WindowsMicMute micMute;

        public StT(Choices WordsRecognition = null)
        {
            speech = "";

            if (WordsRecognition != null)
            {
                Word = new GrammarBuilder(WordsRecognition);
                Word.Culture = new System.Globalization.CultureInfo("fr-FR");
                Dico = new Grammar(Word);
            }
            else
            {
                Dico = new DictationGrammar();
                WaitTime = 5;
                Word = null;
            }
            
            micMute = new WindowsMicrophoneMuteLibrary.WindowsMicMute();

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
                
                
                tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
                tcpListenerThread.IsBackground = true;
                tcpListenerThread.Start();
            }
            catch
            {
                Environment.Exit(0);
            }
        }
        
        private static void ListenForIncommingRequests()
        {
            // création du serveur TCP
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8051);
            tcpListener.Start();

            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);

                            speech = Encoding.ASCII.GetString(incommingData);

                            if (speech == "Mute") micMute.MuteMic();
                            else micMute.UnMuteMic();
                            
                            Console.WriteLine("MUTE/UNMUTE");
                        }
                    }
                }
            }
        }

        public void GetSpeech(int time)
        {
            speech = "";
            mode = (time == 0) ? 0 : 1;
            mode = (Word == null) ? 2 : mode;
            RV(time);
        }

        private static void Transmition(string str)
        {
            try
            {
                ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);
                Console.WriteLine("Done!");

                int wait = 1;

                EventWaitHandle waithandler =
                    new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString());
                do
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

        private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (mode != 2)
            {
                speech = e.Result.Text;
                Console.WriteLine(speech);

                Transmition(speech);
            }
            else
            {
                speech += e.Result.Text + " ";
                Console.WriteLine(speech);
                WaitTime = 5;
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

                if (mode == 1)
                {
                    EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                    {
                        waithandler.WaitOne(TimeSpan.FromSeconds(1));
                        time -= 1;
                    } while (time > 0);
                }
                else if (mode == 0)
                {
                    EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                    {
                        waithandler.WaitOne(TimeSpan.FromSeconds(1));
                    } while (true);
                }
                else
                {
                    EventWaitHandle waithandler = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString()); do
                    {
                        waithandler.WaitOne(TimeSpan.FromSeconds(1));
                        WaitTime -= 1;
                    } while (WaitTime > 0);
                    
                    Transmition(speech);
                }
                
                Transmition("ENDOFTRANSMITION");
                tcpclnt.Close();
            }
        }
    }
}

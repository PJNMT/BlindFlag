using System;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO;

namespace Recognition
{

    /*
     MANUEL D'UTILISATION

        - Reconnaisance vocale:  . Recognition.exe recognition time_s word_1 word_2 ... word_n
                                 . On récupère le résultat de la reconnaissance vocale dans le fichier result.txt (dans le même dossier que Recognition.exe)

        - Synthèse vocale :  Recognition.exe synthesis text
    */

    internal class Program
    {
        static void Synthesis(string S)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SelectVoice("Microsoft Hortense Desktop");
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak(S);
        }

        public static void Main(string[] args)
        {
            int len = args.Length;
            
            if (len < 1) throw new Exception("Forgot arguments");

            if (args[0] == "synthesis")
            {
                string voice = "";
                for (int i = 1; i < len; i++)
                {
                    voice += args[i] + " ";
                }
                Synthesis(voice);
            }
            else if (args[0] == "recognition")
            {
                if (len < 3)
                {
                    throw new Exception("Nothing to recognize (args.Length < 3)");
                }
                else
                {
                    int time_s;

                    try
                    {
                        time_s = Int32.Parse(args[1]);
                    }
                    catch
                    {
                        throw new Exception("int time (args[1])");
                    }

                    Choices KeyWords = new Choices();

                    for (int i = 2; i < len; i++)
                    {
                        KeyWords.Add(args[i]);
                    }

                    StT Recognizer = new StT(KeyWords);

                    Recognizer.GetSpeech(time_s);
                }
            }            
        }
    }
}
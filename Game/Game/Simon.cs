using System;
using System.Media;
using System.Threading;

namespace Game
{
    public struct Simon
    {
        public void SoundDisplay(string file)
        {
            SoundDisplay mélodie = new SoundDisplay("C:\\Users\\Jeanne\\Desktop\\MUTINY\\MUSIQUE\\" +file + ".wav");    // /!\ Jeanne, le chemin du fichier sur ton PC n'est pas le même sur tout les PC /!\
            int i = 1;
            int time;
            bool correct = true;
            int SoundTime = Int32.Parse(file);
            
            while (i<SoundTime && correct)
            {
                time = i * 1000;
                mélodie.PlaySound();
                Thread.Sleep(time);
                correct = CorrectSound();
                i += 1;
            }
        }

        private bool CorrectSound()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
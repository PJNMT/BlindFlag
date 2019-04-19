using System;
using System.Media;
using System.Threading;

namespace Game
{
    public struct Simon
    {
        public void SoundDisplay(Morceau morceau)
        {
            SoundDisplay mélodie = new SoundDisplay(morceau.Getfile() + ".wav");    
            int i = 1;
            int time;
            bool correct = true;
            int SoundTime = Int32.Parse(morceau.GetTime());
            
            while (i<SoundTime && correct)
            {
                time = i * 1000;
                mélodie.PlaySoundTimer(time);
                correct = CorrectSound();
                i += 1;
            }

            if (i == SoundTime)
            {
                morceau.Debloc();
            }
        }

        private bool CorrectSound()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
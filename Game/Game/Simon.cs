using System.Media;
using System.Threading;

namespace Game
{
    public struct Simon
    {
        public void SoundDisplay(string file)
        {
            SoundDisplay mélodie = new SoundDisplay(file);
            int i = 1;
            int time;
            bool correct = true;
            while (i<SoundTime && correct)
            {
                time = i * 1000;
                mélodie.PlaySound();
                Thread.Sleep(time);
                correct = CorrectSound();
            }
        }

        public bool CorrectSound(var)
        {
            
        }
        
        
    }
}
using System;
using System.Media;
   
 
    namespace Game
    {
        public class SoundDisplay
        {
            private string file;
            private SoundPlayer player;



            public SoundDisplay(string file)
            {
                this.file = file;
                this.player = new SoundPlayer(this.file);
                
                
            }

            public void loadsound()
            {
                player.Load();
            }
            
            

            public void PlaySound()
            {
                SoundDisplay mélodie = new SoundDisplay(file);
                loadsound();
                if (player.IsLoadCompleted)
                {
                    player.Play();
                }
            }

            public void StopSound()
            {
                player.Stop();
            }

            


        }

    }

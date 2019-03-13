using System;
using System.Media;
using System.Threading;


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
                loadsound();
                if (player.IsLoadCompleted)
                {
                    player.Play();
                }
            }
            
            public void PlaySoundTimer(int timer)
                        {
                            loadsound();
                            if (player.IsLoadCompleted)
                            {
                                player.Play();
                                Thread.Sleep(timer);
                            }
                        }
            
            

            public void StopSound()
            {
                player.Stop();
            }

            
        }

    }

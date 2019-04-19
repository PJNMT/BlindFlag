using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Xml.Schema;


namespace Game
    {
        public class SoundDisplay
        {
            private string file;
            private static SoundPlayer player;
            

            public SoundDisplay(string file)
            {
                this.file = file;
                player = new SoundPlayer(this.file);
                
            }

            public void loadsound()
            {
                try
                {
                    player.Load();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ficher audio n'existe pas");
                    throw;
                }
                
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
                                Thread.Sleep(timer*1000);
                            }
                        }
            
            

            public void StopSound()
            {
                player.Stop();
            }

            
        }

        public class Morceau
        {
            private string time;
            private string file;
            private bool debloq;


            public Morceau()
            {
                string chemin;
                debloq = false;
                using (StreamReader file = new StreamReader(this.file))
                {
                    this.file = file.ReadLine();
                    time = file.ReadLine();

                }
            }

            public string Getfile()
            {
                return file;
            }

            public string GetTime()
            {
                return time;
            }
            
            public void Debloc()
            {
                debloq = true;
            }
                
        }
        

    }

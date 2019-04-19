using System.Threading;
using Game;

namespace Simon
{
    internal class Program
    {
        //Premier argument est l'emplacement du fichier wav, le deuxième est le tps de lecture
        // /!\ s'il n'y pas de temps de lecture il faut etre un thread.Sleep apres le programme
        public static void Main(string[] args)
        {
            SoundDisplay stream = new SoundDisplay(args[0]);

            if (args.Length >1)
            {
                stream.PlaySoundTimer(int.Parse(args[1]));
            }
            else
            {
                stream.PlaySound();
                
            }
            
        }
    }
}
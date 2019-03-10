using System;

namespace Game
{
    public class Bar
    {
        private Random r = new Random();
        protected int peoples { get; set; } 
        protected enum Choose_Action
        {
            None,
            Play_Simon,
            Pay_Out,
            I_am_drinking_every_day
        }

        public static void launch_bar(Captain captain, int statut 0)
        {
            StT recognition = new StT(new Choices("quitter", "simon", "payer une tournée", "recruter"));
            string choice;

            choice = recognition.GetSpeech(5);
            
            //TODO
            //Coco donne le nombre d'équipage.
            
            

        }

        public Bar()
        {
            this.peoples = r.Next(20) + 10;
        }
    }
}
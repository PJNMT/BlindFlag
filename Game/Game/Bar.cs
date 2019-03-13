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
            //Coco donne le nombre de membres d'équipage.

            while (choice!= "quitter" || choice != "simon" || choice!="payer une tournée" || choice != "recruter") //vérifie les options proposées
            {
                //TODO
                //Coco ne comprend pas, donc redemande
                choice = recognition.GetSpeech(5);
            }

            if (choice != "quitter")
            {
                switch (choice)
                {
                    case "simon":
                        //TODO
                        //lancer simon
                        break;
                    case "payer une tournée":
                        break;
                    case "recruter":
                        //TODO
                        //lancer recrutement
                        break;
                
                }
            }
            else if (choice == "quitter")
            {
                //TODO
                //Coco: demande validation
                choice = recognition.GetSpeech(5);
                validation(choice);
            }
            else
            {
                launch_bar(captain, 1)
            }

        }
        
        private void validation(string choice)
        {
            StT recognition = new StT(new Choices("oui","ouais","nan","non","attends"));

            if (choice == "oui"||choice == "ouais")
            {
                Harbor.launch_harbor(captain, 1);
            }
            else
            {
                launch_bar(captain, 1);
            }
        }

        public Bar()
        {
            this.peoples = r.Next(20) + 10;
        }
    }
}
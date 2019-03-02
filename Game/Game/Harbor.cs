using System.Speech.Recognition;

namespace Game
{
    public class Harbor : Place
    {
        protected bool Got_Bar { get; }
        protected bool Got_ShipShop{ get; }
        protected bool Got_OldBudy { get; }

        protected Bar bar { get; }
        protected ShipShop SShop { get; }
        protected OldTimer OldBudy { get; }
        
        protected enum Choice_Arrival
        {
            None,
            Go_To_Bar, // = equivalent vocal
            Go_To_The_ShipShop, // idem
            Share_The_Loot, // idem
            Departure, // idem
        
        }
        
        
        

        /*public Harbor(bool bar = true, bool Shop = true, bool OldB = true)
        {
            Got_Bar = bar;
            Got_ShipShop = Shop;
            Got_OldBudy = OldB;

            if (Got_Bar)
            {
                this.bar = new Bar();
            }

            if (Got_OldBudy)
            {
                OldBudy = new OldTimer();
            }

            if (Got_ShipShop)
            {
                SShop = new ShipShop();

            }
        }*/
        public Harbor(){}
        
        public static void launch_harbor(Captain captain, int status = 0) // Status initialement à 0
        {
            // TODO
            // si status == 0 : COCO explique l'on est au port
            // sinon : COCO demande si on veux faire autre chose

            StT recognition = new StT(new Choices("bar","magasin","butin","partage","quitter"));
            string choice;
            int quit = 0;

            // TODO
            // COCO dit notre argent total et notre butin aver etat navire + equipage 
            // COCO propose : partager butin, reparer navire, s'occuper de l'equipage (taverne), quitter
            choice = recognition.GetSpeech(5);

            while (choice != "bar" || choice != "magasin" || choice != "butin" || choice != "partage" || choice != "quitter")
            {
                // COCO n'a pas bien compris et repropose : partager butin, reparer navire, s'occuper de l'equipage (taverne), quitter
                choice = recognition.GetSpeech(5);
            }

            if (choice != "quitter")
            {
                switch (choice)
                {
                    case "bar":
                    case "taverne":
                        int cost = 0;
                        // TODO 
                        // COCO :  'en route mauvaise troupe'
                        // aller au bar
                        /*launch_bar(captain,0)*/
                        break;

                    case "magasin":
                        //TODO
                        ShipShop.launch_shop(captain);
                        break;

                    case "butin":
                    case "partage":
                        //TODO
                        //partager butin
                        
                        break;
                }
            }
            else if (choice == "quiter" || quit == 1)
            {
                // TODO
                // COCO : demande validation
                validation();
                // repart en pleine mer
            }
            else
            {
                launch_Harbor(captain, 1);
            }
        }

        private void validation(int attente = 2)
        {
            StT recognition = new StT(new Choices("oui","ouais","nan","non","attends"));
            string choice;

            if (attente > 0)
            {
                //TODO
                //COCO demande validation
                
                choice = recognition.GetSpeech(5);
                if (choive == "non" || choive == "nan")
                {
                    //TODO
                    //launch_harbor(capitainejoueur,1)
                }

                if (choice == "attends")
                {
                    attente += 5;
                }
                
                if (choive != "oui" || choive != "ouais")
                {
                    attente -= 1;
                    validation(attente);
                }
            }
        }
        
    }
}
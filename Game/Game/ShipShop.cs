using System.Speech.Recognition;

namespace Game
{
    public class ShipShop
    {
        public ShipShop(){}

        private void repair(int cost, Captain captain)
        {
            Ship ship = captain.batiment;
            ship.Gold -= cost;
            ship.HP = ship.Max_HP;
        }

        private int up_captain(Captain captain)
        {
            StT recognition = new StT(new Choices("fusil", "épée", "quiter"));
            string choice;

            // TODO
            // marchand dit notre argent total
            // marchand propose : fusil, épée, quiter
            choice = recognition.GetSpeech(5);

            while (choice != "fusil" || choice != "épée" || choice != "quiter")
            {
                // marchand n'a pas bien compris et repropose : fusil, épée, quiter
                choice = recognition.GetSpeech(5);
            }

            if (choice != "quiter")
            {
                switch (choice)
                {
                    case "fusil":
                        // TODO
                        // calcul du prix de l'amelioration
                        // ssi assez argent => marchand dit le prix et propose une validation
                        // sinon => marchand dit qu'on a pas assez d'argent
                        break;

                    case "épée":
                        // TODO
                        // calcul du prix de l'amelioration
                        // ssi assez argent => marchand dit le prix et propose une validation
                        // sinon => marchand dit qu'on a pas assez d'argent
                        break;
                }

                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int up_ship(Captain captain)
        {
            StT recognition = new StT(new Choices("canon", "voiles", "calle", "quartier", "quiter"));
            string choice;

            // TODO
            // marchand dit notre argent total
            // marchand propose : fusil, épée, quiter
            choice = recognition.GetSpeech(5);

            while (choice != "canon" || choice != "voiles" || choice != "calle" || choice != "quartier" || choice != "quiter")
            {
                // marchand n'a pas bien compris et repropose : canon, voiles, calle, quartier, quiter
                choice = recognition.GetSpeech(5);
            }

            if (choice != "quiter")
            {
                switch (choice)
                {
                    case "canon":
                        // TODO
                        // calcul du prix de l'amelioration
                        // ssi assez argent => marchand dit le prix et propose une validation
                        // sinon => marchand dit qu'on a pas assez d'argent
                        break;

                    case "voiles":
                        // TODO
                        // calcul du prix de l'amelioration
                        // ssi assez argent => marchand dit le prix et propose une validation
                        // sinon => marchand dit qu'on a pas assez d'argent
                        break;

                    case "calle":
                        // TODO
                        // calcul du prix de l'amelioration
                        // ssi assez argent => marchand dit le prix et propose une validation
                        // sinon => marchand dit qu'on a pas assez d'argent
                        break;

                    case "quartier":
                        // TODO
                        // calcul du prix de l'amelioration
                        // ssi assez argent => marchand dit le prix et propose une validation
                        // sinon => marchand dit qu'on a pas assez d'argent
                        break;
                }

                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static void launch_shop(Captain captain, int status = 0) // Status initialement à 0
        {
            // TODO
            // si status == 0 : marchand dit bonjour
            // sinon : marchand demande si on veux faire autre chose

            StT recognition = new StT(new Choices("réparer", "équipement", "navire", "quiter"));
            string choice;
            int quit = 0;

            // TODO
            // marchand dit notre argent total
            // marchand propose : réparer, équipement, navire, quiter
            choice = recognition.GetSpeech(5);

            while (choice != "réparer" || choice != "équipement" || choice != "navire" || choice != "quiter")
            {
                // marchand n'a pas bien compris et repropose : réparer, équipement, navire, quiter
                choice = recognition.GetSpeech(5);
            }

            if (choice != "quiter")
            {
                switch (choice)
                {
                    case "réparer":
                        int cost = 0;
                        // TODO 
                        // Calcul du coût de la réparation du navire
                        repair(cost, captain);
                        break;

                    case "équipement":
                        quit = up_captain(captain);
                        break;

                    case "navire":
                        quit = up_ship(captain);
                        break;
                }
            }
            else if (choice == "quiter" || quit == 1)
            {
                // TODO
                // marchand dit aurevoir et merci
            }
            else
            {
                launch_shop(captain, 1);
            }
        }
    }
}
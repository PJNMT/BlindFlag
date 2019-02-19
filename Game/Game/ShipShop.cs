using System.Speech.Recognition;

namespace Game
{
    public class ShipShop
    {
        public ShipShop() { }

        private void repair(int cost, Captain captain)
        {
            Ship ship = captain.batiment;
            ship.Gold -= cost;
            ship.HP = ship.Max_HP;
        }

        private void up_captain(Captain captain, int status = 0)
        {
            StT recognition = new StT(new Choices("fusil", "épée", "quiter"));
            string choice;

            // TODO
            // si status == 0: marchand dit bonjour ...
            // si status != 0: marchand dit merci d'avoir achter chez nous ...
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

                up_captain(captain, 1);
            }

            // TODO
            // marchand dit aurevoir ...
        }
    }
}
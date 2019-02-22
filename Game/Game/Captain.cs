using System;

namespace Game
{
    public class Captain
    {
        protected int HP{ get; set; }
        protected int Reputation{ get; set; }
        private string Name{ get; }
        protected int Money{ get; set; }

        protected int Lvl{ get; set; }
        public Ship batiment { get; }
        protected int Gun_atk{ get; set; }
        protected int Saber_atk{ get; set; }
        
        // pour le combat, à voir si besoin ou non (getposx() et getposy() associés
        protected int PosX{ get; set; }
        protected int PosY{ get; set; }
        protected int PosZ{ get; set; } // le capitaine se baisse ou non. valeur 1 ou 0 EXCLUSIVEMENT.

        public Captain(string Name, int Lvl, Ship batiment, int PosX, int PosY, int PosZ)
        {
            this.Name = Name;
            this.Lvl = Lvl;
            this.batiment = batiment;
            this.PosX = PosX;
            this.PosY = PosY;
            this.PosZ = PosZ;

            HP = (int) (Math.Pow(1.5f, this.Lvl) * 100);
            Reputation = 50;
            Money = 0;
            Gun_atk = (int) (Math.Pow(1.5f, this.Lvl) * 15);
            Saber_atk = (int) (Math.Pow(1.5f, this.Lvl) * 10);
        }
    }
}
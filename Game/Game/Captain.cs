namespace Game
{
    public class Captain
    {
        private int HP;
        private int Reputation;
        private string Name;
        private int Money;
        
        /*pour le combat, à voir si besoin ou non (getposx() et getposy() associés*/
        protected int PosX;
        protected int PosY;
        protected int PosZ;

        public Captain(int HP, int Reputation, string Name, int Money, int PosX, int PosY, int PosZ)
        {
            this.HP = HP;
            this.Reputation = Reputation;
            this.Name = Name;
            this.Money = Money;
            this.PosX = PosX;
            this.PosY = PosY;
            this.PosZ = PosZ; //le capitaine se baisse ou non. valeur 1 ou 0 EXCLUSIVEMENT.
        }
        
        

        public int GetHP()
        {
            return HP;
        }
        
        public int GetReputation()
        {
            return Reputation;
        }

        public int GetMoney()
        {
            return Money;
        }

        public string GetName()
        {
            return Name;
        }
        
        public int GetPosX()
        {
            return PosX;
        }
        
        public int GetPosY()
        {
            return PosY;
        }

        public int GetPosZ()
        {
            return PosZ;
        }
    
    }
}
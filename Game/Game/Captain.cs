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

        public Captain(int HP, int Reputation, string Name, int Money, int PosX, int Posy)
        {
            this.HP = HP;
            this.Reputation = Reputation;
            this.Name = Name;
            this.Money = Money;
            this.PosX = PosX;
            this.PosY = Posy;
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
    
    }
}
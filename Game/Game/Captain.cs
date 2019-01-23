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
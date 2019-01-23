namespace Game
{
    public abstract class Ship
    {
        protected int Gold;
        
        protected int Level;
        protected int XP;
        
        protected int HP;
        protected int Attack_pts;
        protected int Speed;
        protected int Nb_Max_Crew;
        protected int Nb_Crew;

        protected (int, int) (PosX, PosY);


        public (int, int) GetPos()
        {
            return (posX, PosY);
        }
        
        public int GetHP()
        {
            return HP;
        }

        public int GetAttack()
        {
            return Attack_pts;
        }

        public int GetSpeed()
        {
            return Speed;
        }

        public int GetGold()
        {
            return Gold;
        }

        public int GetCrew()
        {
            return Nb_Crew;
        }

        public int GetMaxCrew()
        {
            return Nb_Max_Crew;
        }

        public int GetLvl()
        {
            return Level;
        }

        public int GetXP()
        {
            return XP;
        }
    }
}
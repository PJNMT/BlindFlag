namespace Game
{
    public abstract class Ship
    {
        protected bool Hostility { get; set; }

        protected string Name{ get; }
        public int Gold{ get; set; }
        public int Max_Gold { get; set; }

        protected int Level{ get; set; }
        protected int XP{ get; set; }
        
        public int HP{ get; set; }
        public int Max_HP{ get; set; }
        protected int Attack_pts{ get; set; }
        protected int Speed { get; set; }

        protected int Nb_Max_Crew{ get; set; }
        protected int Nb_Crew{ get; set; }

        protected int PosX{ get; set; }
        protected int PosY{ get; set; }
    }
}
namespace Game
{
    public class Pirate : Ship
    {
        protected bool Hostile;
        
        public Pirate(int Gold, int Level, int HP, int Attack_pts, int Speed, int Crew)
        {
            this.Gold = Gold;
            this.Level = Level;
            this.HP = HP;
            this.Attack_pts = Attack_pts;
            this.Speed = Speed;
            this.Nb_Crew = Crew;
        }
    }
}
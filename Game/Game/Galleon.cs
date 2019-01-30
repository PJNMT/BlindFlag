namespace Game
{
    public class Galleon : Ship
    {
        public Galleon(int Gold, int Level, int HP, int Attack_pts, int Speed, int Crew)
        {
            this.Gold = Gold;
            this.Level = Level;
            this.HP = HP;
            this.Attack_pts = Attack_pts;
            this.Speed = Speed;
            Nb_Crew = Crew;
        }
    }
}
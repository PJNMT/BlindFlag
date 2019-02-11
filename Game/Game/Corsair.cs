using System;

namespace Game
{
    public class Corsair : Ship
    {
        public Corsair(string name, int level, bool hostile, int x, int y)
        {
            this.Hostility = hostile;
            name = name;
            Level = level;
            XP = 0;
            HP = 100;
            Attack_pts = level * 10;
            Speed = level % 10;

            Nb_Max_Crew = (int)(Math.Pow((int)(10 * 1.5f), level));
            Nb_Crew = Nb_Max_Crew;

            PosX = x;
            PosY = y;
        }
    }
}
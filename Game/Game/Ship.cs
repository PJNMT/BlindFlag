using System;
using System.Net.NetworkInformation;

namespace Game
{
    public abstract class Ship
    {
        protected bool Hostility { get; set; }

        protected string Name{ get; set; }
        protected int Gold{ get; set; }
        
        protected int Level{ get; set; }
        protected int XP{ get; set; }
        
        protected int HP{ get; set; }
        protected int Attack_pts{ get; set; }
        protected int Speed { get; set; }

        protected int Nb_Max_Crew{ get; set; }
        protected int Nb_Crew{ get; set; }

        protected int PosX{ get; set; }
        protected int PosY{ get; set; }

        


       }
}
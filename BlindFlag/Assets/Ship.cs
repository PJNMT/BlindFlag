using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class BlindShip : Ship
{
    private int test;

    public BlindShip()
    {
        this.Gold = 0;
        this.Level = 1;
        this.HP = 100;
        this.Attack_pts = 10;
        this.Speed = 10;
        Nb_Crew = 5;
        Nb_Max_Crew = 10;
        XP = 0;
    }
}

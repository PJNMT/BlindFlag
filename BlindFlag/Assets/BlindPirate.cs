using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindPirate : captain
{
    protected int HP{ get; set; }
    protected int Reputation{ get; set; }
    private string Name{ get; set; }
    public static int Money{ get; set; }

    protected int Lvl{ get; set; }
    public Ship batiment { get; set; }
    protected int Gun_atk{ get; set; }
    protected int Saber_atk{ get; set; }
        
    // pour le combat, à voir si besoin ou non (getposx() et getposy() associés
    protected int PosX{ get; set; }
    protected int PosY{ get; set; }
    protected int PosZ{ get; set; }
    // Start is called before the first frame update
    void Start()
    {
       
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Gagneror(int or)
    {
        Money += or;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlindShip_Stat : MonoBehaviour
{
    public static int Money;
    public static int Max_Money;

    public static int HP;
    public static int Max_HP;
    
    public static int Lvl;
    private static int Max_Lvl = 50;
    
    public static int XP;
    private static int Max_XP;
    
    public static int Damage;
    public static int Max_Damage;

    public static int Crew;
    public static int Max_Crew;

    public static int Shield;
    private static int Max_Shield;

    void Start()
    {
        Lvl = 0;
        Money = 0;
        Crew = 0;
        Damage = 0;
        Shield = 0;

        GetLVL();
    }

    private void Update()
    {
        if (HP <= 0) BlindCaptain_Stat.Dead();
        if (XP >= Max_XP) GetLVL();
        if (Lvl > Max_Lvl) Lvl = Max_Lvl;
        if (Money > Max_Money) Money = Max_Money;
        if (Crew > Max_Crew) Crew = Max_Crew;
        if (Damage > Max_Damage) Damage = Max_Damage;
    }

    void GetLVL()
    {
        Lvl += 1;
        SetStat();
        Damage += 3;
        Shield += 5;
    }

    public static void SetStat()
    {
        XP = 0;
        Max_XP = Lvl * 100;
        
        Max_HP = Lvl * 100;
        HP = Max_HP;
        
        Max_Money = Lvl * 1000;

        Max_Crew = Lvl * 2;

        Max_Damage = Lvl * 6;

        Max_Shield = Lvl * 10;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cannonball_E(Clone)")
        {
            Debug.Log("on est touche");
            Destroy(other.gameObject, 0f);
            HP -= AI_enemy.Damage;
        }
    }
    
    
    
    private void OnGUI()
    {
        GUI.Box(new Rect(25, 10, 150, 25), "BlindShip's STATS");
        GUI.Box(new Rect(25, 36, 150, 25), "HP: " + HP + " / " + Max_HP);
        GUI.Box(new Rect(25, 62, 150, 25), "Lvl: " + Lvl);
        GUI.Box(new Rect(25, 88, 150, 25), "XP: " + XP + " / " + Max_XP);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlindShip_Stat : MonoBehaviour
{
    public static int Money;
    public static int Max_Money;

    public static int HP = 100;
    public static int Max_HP;
    
    public static int Lvl;
    private static int Max_Lvl = 50;
    
    public static int XP;
    public static int Max_XP;
    
    public static int Damage;
    public static int Max_Damage;

    public static int Crew;
    public static int Max_Crew;

    public static int Shield;
    private static int Max_Shield;

    private static GameObject BlindShip;

    void Start()
    {
        BlindShip = gameObject;
    }

    private void Update()
    {
        if (HP <= 0) BlindCaptain_Stat.Dead();
        if (XP >= Max_XP) GetLVL();
        if (Lvl > Max_Lvl) Lvl = Max_Lvl;
        if (Money > Max_Money) Money = Max_Money;
        if (Crew > Max_Crew) Crew = Max_Crew;
        if (Damage > Max_Damage) Damage = Max_Damage;
        if (Shield > Max_Shield) Shield = Max_Shield;
    }

    public static void Dead()
    {
        Destroy(BlindShip, 0f);
    }

    public static void GetLVL()
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
}

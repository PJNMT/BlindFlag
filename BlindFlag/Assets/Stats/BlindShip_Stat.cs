using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlindShip_Stat : MonoBehaviour
{
    public static int Lvl = 1;
    private static int Max_Lvl = 50;
    
    public static int Money = 0;
    public static int Max_Money = Lvl * 1000;

    public static int HP = Lvl * 100;
    public static int Max_HP = Lvl * 100;
    
    public static int XP = 0;
    public static int Max_XP = Lvl * 100;
    
    public static int Damage = 6;
    public static int Max_Damage = Lvl * 10;

    public static int Crew = 2;
    public static int Max_Crew = Lvl * 2;

    public static int Shield = 10;
    private static int Max_Shield = Lvl * 10;
    
    public static int SceneLoad = 0; // 0: Navigation, 1: SeaBattle, 2: Port, 3: Taverne, 4: Simon, 5: Combat, 6: ShipShop, 7: ChasseAuTresor  

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

        Max_Damage = Lvl * 10;

        Max_Shield = Lvl * 10;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlindCaptain_Stat : MonoBehaviour
{
    public static int HP;
    private static int Max_HP;
    
    public static int XP;
    private static int Max_XP;
    
    public static int Lvl;
    private static int Max_Lvl = 50;

    public static int GunDamage;
    public static int Max_GunDamage;
    
    public static int SwordDamage;
    public static int Max_SwordDamage;

    public static int Reputation;
    public static string Name;

    void Start()
    {
        Lvl = 0;
        Reputation = 0;
        Name = "BlindCaptain";
        GunDamage = 0;
        SwordDamage = 0;
        GetLVL();
    }

    private void Update()
    {
        if (HP <= 0) Dead();
        if (XP >= Max_XP) GetLVL();
        if (Lvl > Max_Lvl) Lvl = Max_Lvl;
        if (GunDamage > Max_GunDamage) GunDamage = Max_GunDamage;
        if (SwordDamage > Max_SwordDamage) SwordDamage = Max_SwordDamage;
    }

    void GetLVL()
    {
        Lvl += 1;
        SetStat();
        SwordDamage += 10;
        GunDamage += 30;
    }

    static void SetStat()
    {
        XP = 0;
        Max_XP = Lvl * 100;
        
        Max_HP = Lvl * 100;
        HP = Max_HP;
        
        Max_GunDamage = Lvl * 15;
        
        Max_SwordDamage = Lvl * 40;
    }

    public static void Dead()
    {
        SetStat();
        BlindShip_Stat.SetStat();
        BlindShip_Stat.Money -= BlindShip_Stat.Money / 5;
        SceneManager.LoadScene("navi");
    }
}

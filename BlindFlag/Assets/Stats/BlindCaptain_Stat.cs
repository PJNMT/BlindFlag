using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlindCaptain_Stat : MonoBehaviour
{
    public static int Lvl = 1;
    private static int Max_Lvl = 50;
    
    public static int HP = Lvl * 100;
    public static int Max_HP = Lvl * 100;
    
    public static int XP = 0;
    public static int Max_XP = Lvl * 100;

    public static int GunDamage = 40;
    public static int Max_GunDamage = Lvl * 40;
    
    public static int SwordDamage = 15;
    public static int Max_SwordDamage = Lvl * 15;

    public static int Reputation = 0;
    public static string Name = "BlindPirate";

    private void Update()
    {
        if (HP <= 0) Dead();
        if (XP >= Max_XP) GetLVL();
        if (Lvl > Max_Lvl) Lvl = Max_Lvl;
        if (GunDamage > Max_GunDamage) GunDamage = Max_GunDamage;
        if (SwordDamage > Max_SwordDamage) SwordDamage = Max_SwordDamage;
    }

    public static void GetLVL()
    {
        Lvl += 1;
        SetStat();
        SwordDamage += 10;
        GunDamage += 30;
    }

    public static void SetStat()
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
        BlindShip_Stat.Dead();
        Scene S = SceneManager.GetActiveScene();
        SceneManager.LoadScene("navi");
        BlindShip_Stat.SceneLoad = 0;
        SceneManager.UnloadSceneAsync(S);
    }
}

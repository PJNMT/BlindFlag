using System;
using System.Collections;
using System.Collections.Generic;
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

    public static int NbEnigme = 1;

    public static bool chasseautresor = false;
    
    [SerializeField]
    private AudioClip level;
    [SerializeField]
    private AudioClip gundamage;
    [SerializeField]
    private AudioClip sworddamage;

    private static AudioClip L;
    private static AudioClip G;
    private static AudioClip S;
    
    public static Dictionary<string, bool> Tuto = new Dictionary<string, bool>()
    {
        {"SeaBattle", false},
        {"ChasseAuTresor", false},
        {"Combat", false},
        {"Coco", false},
    };

    private void Start()
    {
        L = level;
        G = gundamage;
        S = sworddamage;
    }

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
        BlindShip_Stat.SceneLoad = (int) LoadScene.Scene.Navigation;
        SceneManager.LoadScene("navi");
    }
    
   public static void AddStat(AudioSource audioSource, string stat)
    {
        audioSource.PlayOneShot(L);
        switch (stat)
        {
            case  "level":
                Lvl += 1;
                Synthesis.synthesis("Vous avez gagné 1 niveau");
                break;
            case "reputation":
                Reputation += 5;
                Synthesis.synthesis("Vous avez gagné 5 points de réputation");
                break;
            case "XP":
                XP += 100;
                Synthesis.synthesis("Vous avez gagné 100 XP");
                break;
            case "HP":
                HP = Max_HP;
                Synthesis.synthesis("Vos HP sont à leur maximum, Capitaine, vous êtes en pleine forme !");
                break;
        }
        
    }

    public static void AddSworddamage(AudioSource audioSource, int added)
    {
        audioSource.PlayOneShot(S);
        SwordDamage += added;
    }
    
    public static void AddGundamage(AudioSource audioSource, int added)
    {
        audioSource.PlayOneShot(G);
        GunDamage += added;
    }
    
    
    //information pr cbt contre clairvoyant
    public static int nb_ennemy_defeated = 0;
    public static bool do_seabattle = false;
    public static bool in_clairvoyant = false;
}


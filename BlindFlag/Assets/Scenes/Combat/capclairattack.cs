using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class capclairattack : MonoBehaviour
{
    public static int gun_atk;
    public static int saber_atk;
    private int lvl;
    
    public AudioClip FinJeu;
    public AudioClip Mort1;
    public AudioClip Mort2;

    private static int XP;
    private static int Money;

    private static int IA_atk;
    private static float IA_HP;
    private AudioSource Audio;

    public AudioClip death_ennemy;

    public static KeyCode swordatk = KeyCode.Space;
    public static KeyCode gunatk = KeyCode.KeypadEnter;

    // Start is called before the first frame update
    void Start()
    {
        Thread.Sleep(2200);
        gun_atk = BlindCaptain_Stat.GunDamage; //initialise stats capitaine
        saber_atk = BlindCaptain_Stat.SwordDamage;
        lvl = BlindCaptain_Stat.Lvl;

        Money = 2000;
        XP = 0;

        if (BlindCaptain_Stat.start_clairvoyant)
        {
            IA_HP = StatClairvoyant.Clairvoyant_HP;
            IA_HP = 250;
        }
        else
        {
            IA_HP = 5000;
            IA_atk = 150;
        }

    }

    void Dead() //give money and xp to capitain
    {
        BlindCaptain_Stat.nb_ennemy_defeated += 1; //augmente le nb d'ennemis defaits
        Thread.Sleep(1000);
        BlindCaptain_Stat.XP += XP;
        BlindShip_Stat.Money += Money;
        GetComponent<AudioSource>().PlayOneShot(death_ennemy);
        Thread.Sleep(2000);
        LoadScene.Load(LoadScene.Scene.Navigation, LoadScene.Scene.Combat);
    }

    // Update is called once per frame
    void Update()
    {
        if (BlindCaptain_Stat.start_clairvoyant) //définit les variable du "big boss"
        {
            if (IA_HP<(IA_HP/2))
            {
                IA_atk *= 2;
            }
        }
        
        if (IA_HP <= 0) Dead(); //check if IA dead or no
    }
}

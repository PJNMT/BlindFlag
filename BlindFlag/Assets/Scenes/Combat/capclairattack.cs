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
        if (BlindCaptain_Stat.start_clairvoyant)
        {
            StatClairvoyant.nb_death += 1;
            Death();
        }
        if (IA_HP <= 0) Dead(); //check if IA dead or no
    }
    
    void Death()
    {
        if (StatClairvoyant.nb_death == 3)
        {
            Audio.PlayOneShot(FinJeu);
            Thread.Sleep(2000);
            LoadScene.Load(LoadScene.Scene.Navigation, LoadScene.Scene.END);
        }
        else
        {
            if (StatClairvoyant.nb_death == 1)
            {
                Audio.PlayOneShot(Mort1);
            }
            else
            {
                Audio.PlayOneShot(Mort2);
            }
            
            Thread.Sleep(2000);
            float ClaiHP = StatClairvoyant.Clairvoyant_HP;
            StatClairvoyant.Clairvoyant_HP = ClaiHP * 0.9f;
            Thread.Sleep(2000);
            LoadScene.Load(LoadScene.Scene.ENDCOMBAT, LoadScene.Scene.ENDCOMBAT);
        }
    }
}

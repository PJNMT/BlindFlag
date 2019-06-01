using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class captainattack : MonoBehaviour
{
    public static int gun_atk;
    public static int saber_atk;
    private int lvl;

    private static int XP;
    private static int Money;

    private int IA_lvl;
    public static int IA_atk;
    public static int IA_HP;

    public AudioClip death_ennemy;

    public static KeyCode swordatk = KeyCode.Space;
    public static KeyCode gunatk = KeyCode.KeypadEnter;

    // Start is called before the first frame update
    void Start()
    {
        gun_atk = BlindCaptain_Stat.GunDamage; //initialise stats capitaine
        saber_atk = BlindCaptain_Stat.SwordDamage;
        lvl = BlindCaptain_Stat.Lvl;

        if (lvl < 6) IA_lvl = Random.Range(1, lvl); //Determine niveau aleatoire IA
        else IA_lvl = Random.Range(lvl - 5, lvl + 5);

        XP = (IA_lvl * 100) / Random.Range(2, 10); //determine XP et Money to earn
        Money = (IA_lvl * 1000) / Random.Range(2, 50);

        IA_HP = IA_lvl * 100; //determine stats IA
        IA_atk = IA_lvl * 3;

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
        if (IA_HP <= 0) Dead(); //check if IA dead or no
    }
}

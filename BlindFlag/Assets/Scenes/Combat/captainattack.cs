using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class captainattack : MonoBehaviour
{
    private int gun_atk;
    private int saber_atk;
    private int lvl;

    private static int XP;
    private static int Money;

    private int IA_lvl;
    private int IA_atk;
    private int IA_HP;
    
    private bool do_swordok=true;
    private bool do_gunok=true;

    private KeyCode swordatk;
    private KeyCode gunatk;

    // Start is called before the first frame update
    void Start()
    {
        
        swordatk = KeyCode.Space;
        gunatk = KeyCode.KeypadEnter;
        
        gun_atk = BlindCaptain_Stat.GunDamage;
        saber_atk = BlindCaptain_Stat.SwordDamage;
        lvl = BlindCaptain_Stat.Lvl;

        if (lvl < 6) IA_lvl = Random.Range(1, lvl);
        else IA_lvl = Random.Range(lvl - 5, lvl + 5);

        XP = (IA_lvl * 100) / Random.Range(2, 10);
        Money = (IA_lvl * 1000) / Random.Range(2, 50);

        IA_HP = IA_lvl * 100;

    }
    
    IEnumerator launch_gun() //imite le temps de recharge d'une arme
    {
        IA_HP -= gun_atk;
        Debug.Log(IA_HP);
        yield return new WaitForSeconds(10f);
        do_gunok = true;
    }

    IEnumerator launch_sword() //imite le temps qu'il faut à l'arme pour revenir
    {
        IA_HP -= saber_atk;
        Debug.Log(IA_HP);
        yield return new WaitForSeconds(1f);
        do_swordok = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "Ennemy")
        {
            if (do_gunok && Input.GetKeyDown(gunatk))
            {
                do_gunok = false;
                StartCoroutine("launch_gun");
            }
            if (do_swordok && Input.GetKeyDown(swordatk))
            {
                do_swordok = false;
                StartCoroutine("launch_sword");
            }
        }
    }

    void Dead()
    {
        BlindCaptain_Stat.XP += XP;
        BlindShip_Stat.Money += Money;
        Destroy(gameObject,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IA_HP <= 0) Dead();
    }
}

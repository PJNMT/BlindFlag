using System.Collections;
using System.Collections.Generic;
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
    private GameObject target;
    
    private bool do_swordok;
    private bool do_gunok;

    IEnumerator launch_gun()
    {
        yield return new WaitForSeconds();
    }

    // Start is called before the first frame update
    void Start()
    {
        gun_atk = BlindCaptain_Stat.GunDamage;
        saber_atk = BlindCaptain_Stat.SwordDamage;
        lvl = BlindCaptain_Stat.Lvl;

        if (lvl < 6) IA_lvl = Random.Range(1, lvl);
        else IA_lvl = Random.Range(lvl - 5, lvl + 5);

        XP = (IA_lvl * 100) / Random.Range(2, 10);
        Money = (IA_lvl * 1000) / Random.Range(2, 50);

        IA_HP = IA_lvl * 100;

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

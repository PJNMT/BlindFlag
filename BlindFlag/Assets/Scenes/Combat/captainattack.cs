using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class captainattack : MonoBehaviour
{
    private int gun_atk;
    private int saber_atk;
    private int HP;

    // Start is called before the first frame update
    void Start()
    {
        gun_atk = BlindCaptain_Stat.GunDamage;
        saber_atk = BlindCaptain_Stat.SwordDamage;
        HP = BlindCaptain_Stat.HP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

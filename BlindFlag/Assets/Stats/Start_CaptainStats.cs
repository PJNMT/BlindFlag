﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_CaptainStats : MonoBehaviour
{
    void Start()
    {
        BlindCaptain_Stat.Lvl = 0;
        BlindCaptain_Stat.Reputation = 0;
        BlindCaptain_Stat.Name = "BlindCaptain";
        BlindCaptain_Stat.GunDamage = 0;
        BlindCaptain_Stat.SwordDamage = 0;
        BlindCaptain_Stat.GetLVL();
    }
}
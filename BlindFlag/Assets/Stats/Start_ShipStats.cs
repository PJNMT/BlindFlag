using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_ShipStats : MonoBehaviour
{
    void Start()
    {
        BlindShip_Stat.Lvl = 0;
        BlindShip_Stat.Money = 0;
        BlindShip_Stat.Crew = 0;
        BlindShip_Stat.Damage = 0;
        BlindShip_Stat.Shield = 0;
        BlindShip_Stat.GetLVL();
    }
}

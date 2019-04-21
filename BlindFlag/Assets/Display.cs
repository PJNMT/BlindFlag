using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Box(new Rect(25, 10, 150, 25), "BlindShip's STATS");
        GUI.Box(new Rect(25, 36, 150, 25), "HP: " + BlindShip_Stat.HP + " / " + BlindShip_Stat.Max_HP);
        GUI.Box(new Rect(25, 62, 150, 25), "Lvl: " + BlindShip_Stat.Lvl);
        GUI.Box(new Rect(25, 88, 150, 25), "XP: " + BlindShip_Stat.XP + " / " + BlindShip_Stat.Max_XP);
        GUI.Box(new Rect(25, 114, 150, 25), "Money: " + BlindShip_Stat.Money + " / " + BlindShip_Stat.Max_Money);
    }
}

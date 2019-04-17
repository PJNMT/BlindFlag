using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_enemy : MonoBehaviour
{
    private static int HP;
    private static int Money;
    private static int XP;
    private static int Lvl;
    public static int Damage;
    private static int BlindShip_LVL;

    
    // Start is called before the first frame update
    void Start()
    {
        BlindShip_LVL = BlindShip_Stat.Lvl;
        
        if (BlindShip_LVL < 6) Lvl = Random.Range(1, BlindShip_LVL + 5);
        else Lvl = Random.Range(BlindShip_LVL - 5, BlindShip_LVL + 5);

        Damage = Lvl * 10;
        
        XP = (Lvl * 100)/Random.Range(2, 10);
        Money = (Lvl * 1000)/Random.Range(2, 50);

        HP = Lvl * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Dead();
    }

    void Dead()
    {
        BlindShip_Stat.Money += Money;
        BlindCaptain_Stat.XP += XP;
        Destroy(gameObject, 0f);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cannonball(Clone)")
        {
            Debug.Log("vous l'avez touche");
            HP -= BlindShip_Stat.Damage;
            Destroy(other.gameObject, 0f);
        }
    }
}

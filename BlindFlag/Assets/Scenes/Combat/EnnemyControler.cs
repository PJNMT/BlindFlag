using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnnemyControler : MonoBehaviour
{
    private float x_rand;
    private float z_rand;
    Random random = new Random();
    private int IA_damage;
    private Transform target;
    private GameObject player;
    private bool do_changepos = true;
    private int rand_attack;
    private int HP;
    private bool do_attack = true;

    // Start is called before the first frame update
    void Start()
    {
        IA_damage = captainattack.IA_atk;
        transform.position.Set(0, 1, 0);
        HP = BlindCaptain_Stat.HP;
        player = GameObject.FindWithTag("Captain");
        target = player.transform;
    }

    IEnumerator launch_gun() //imite le temps de recharge d'une arme
    {
        captainattack.IA_HP -= captainattack.gun_atk;
        Debug.Log(captainattack.IA_HP);
        yield return new WaitForSeconds(10f);
        captainattack.do_gunok = true;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Projectile")
        {
            if (captainattack.do_gunok)
            {
                captainattack.do_gunok = false;
                StartCoroutine("launch_gun");
            }
        }
    }

    void ChangePosition() //changement de position aleatoire IA
    {
        var newposition = Random.insideUnitCircle * 5;
        transform.position = new Vector3(newposition.x, 1, newposition.y);
        do_changepos = false;
    }
    
    IEnumerator ChangeIAposition()
    {
        if (do_changepos)
        {
            ChangePosition();
            yield return new WaitForSeconds(120f);
            do_changepos = true;
        }
    }

    void IA_attack() //attaque de IA
    {
        rand_attack = Random.Range(0, 1);
        if (rand_attack == 1) HP -= IA_damage;
        do_attack = false;
    }

    IEnumerator IA_Damage()
    {
        if (do_attack)
        {
            IA_attack();
            yield return new WaitForSeconds(20f);
            do_attack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) BlindCaptain_Stat.Dead();

        if (Vector3.Distance(target.position, target.position)<2)
        {
            StartCoroutine("ChangeIAposition");
            StartCoroutine("IA_attack");
        }
    }

}

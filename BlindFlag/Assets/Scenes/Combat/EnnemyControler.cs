using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnnemyControler : MonoBehaviour
{
    private float x_rand;
    private float z_rand;
    private Transform target;
    public GameObject player;
    private bool do_changepos = true;

    private int rand_attack;
    private int rand_soundatk;

    private int HP;
    private bool do_attack = true;

    public AudioClip Atk_IA1;
    public AudioClip Atk_IA2;
    public AudioClip Atk_IA3;

    private AudioClip[] Sons;

    public AudioClip TutoCombat;

    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(0, 1, 0); //place ennemy vers le centre du palteau
        HP = BlindCaptain_Stat.HP;

        target = player.transform;
        Sons = new[] { Atk_IA1, Atk_IA2, Atk_IA3};
        
        if (!BlindCaptain_Stat.Tuto["Combat"])
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => GetComponent<AudioSource>().PlayOneShot(TutoCombat));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoCombat.length * 1000 + 500));

            BlindCaptain_Stat.Tuto["Combat"] = true;
        }
    }

    private void OnCollisionEnter(Collision other) //verifie si un projectile entre dans collider zone ennemy
    {
        /*Debug.Log(other.gameObject.name +"collide with ennemy");*/
        if (other.gameObject.name == "Projectile(Clone)")
        {
            captainattack.IA_HP -= captainattack.gun_atk;
            /*Debug.Log("IA_HP = " + captainattack.IA_HP);*/
        }
    }

    void ChangePosition() //changement de position aleatoire IA
    {
        var newx = Random.Range(target.transform.position.x - 3, target.position.x + 3);
        var newz = Random.Range(target.transform.position.z - 3, target.position.z + 3); ;
        transform.position = new Vector3(newx, 1, newz);
        do_changepos = false;
    }

    IEnumerator ChangeIAposition()
    {
        if (do_changepos)
        {
            ChangePosition();
            yield return new WaitForSeconds(10f);
            do_changepos = true;
        }
    }

    void IA_attack() //attaque de IA
    {
        rand_soundatk = Random.Range(0, Sons.Length);
        GetComponent<AudioSource>().PlayOneShot(Sons[rand_soundatk]);
        rand_attack = Random.Range(0, 2); //determine if IA attack or no (attack every 20sec)
        if (rand_attack == 1) HP -= captainattack.IA_atk;
        do_attack = false;
        Debug.Log("CaptHP = " + HP);

    }

    IEnumerator IA_Damage() //coroutine set as the IA attack every 20sec
    {
        if (do_attack)
        {
            IA_attack();
            yield return new WaitForSeconds(5f);
            do_attack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) BlindCaptain_Stat.Dead(); //check if capitain dead or no

        if (Vector3.Distance(transform.position, target.position) < 5) //check if capitaine close to ennemy and if yes, launch attack and coroutine to change position
        {
            StartCoroutine(IA_Damage());
        }
        StartCoroutine("ChangeIAposition");
    }

}

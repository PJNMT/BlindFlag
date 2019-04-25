using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{

    public GameObject Projectile;
    public int Force = 50;
    public AudioClip tir;
    public AudioClip recharge;

    private bool do_gunok = true;

    IEnumerator launch_gun() //imite le temps de recharge d'une arme
    {
        Debug.Log("IA_HP =" + captainattack.IA_HP); //Debug
        GetComponent<AudioSource>().PlayOneShot(recharge);
        yield return new WaitForSeconds(10f);
        do_gunok = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(captainattack.gunatk) && do_gunok)
        {
            GetComponent<AudioSource>().PlayOneShot(tir);
            GameObject Bullet = Instantiate(Projectile, transform.position, Quaternion.identity); //create a new bullet
            Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * Force; //apply a force on it      

            Destroy(Bullet, 5f);

            do_gunok = false;
            StartCoroutine("launch_gun");
        }
    }
}

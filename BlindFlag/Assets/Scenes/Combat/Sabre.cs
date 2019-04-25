using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabre : MonoBehaviour
{
    private AudioClip[] Sons;
    public AudioClip sabre1;
    public AudioClip sabre2;
    public AudioClip sabre3;
    public AudioClip sabre4;
    public AudioClip degainer;

    private bool do_swordok = true;

    // Start is called before the first frame update
    void Start()
    {
        Sons = new[] { sabre1, sabre2, sabre3, sabre4 };

    }

    void OnCollisionStay(Collision other)
    {
        /*Debug.Log(other.gameObject.name + "collide with Sabre");*/
        if (other.gameObject.name == "Ennemy") //verifie que le sabre est bien dans la zone ennemy
        {
            if (do_swordok && Input.GetKeyDown(captainattack.swordatk))
            {
                do_swordok = false;
                StartCoroutine("launch_sword");
            }
        }
    }

    IEnumerator launch_sword() //imite le temps qu'il faut à l'arme pour revenir
    {
        captainattack.IA_HP -= captainattack.saber_atk;
        Debug.Log("IA_HP =" + captainattack.IA_HP);

        int randomsond = Random.Range(0, Sons.Length);
        GetComponent<AudioSource>().PlayOneShot(Sons[randomsond]);
        yield return new WaitForSeconds(1f);
        do_swordok = true;
    }

    void Update()
    {

    }
}

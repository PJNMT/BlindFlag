using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cannonball_controller : MonoBehaviour
{
    public float moveSpeed = 200f;
    public Vector3 vect = Vector3.forward;
    public AudioClip plouf;
    public AudioClip boom;

    private bool play = false;
    
    // Update is called once per frame
    void Update()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Translate(vect * moveSpeed * Time.deltaTime));
        play = GetComponent<AudioSource>().isPlaying && play;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "sea")
        {
            if (play == false)
            {
                Debug.Log("Plouf");
                GetComponent<AudioSource>().volume = 0.05f;
                GetComponent<AudioSource>().PlayOneShot(plouf);
                Destroy(gameObject, plouf.length + 0.5f);
            }
        }
        
        if (gameObject.name == "Cannonball_E(Clone)" && other.gameObject.name == "BlindPirate")
        {
            play = true;
            Debug.Log("vous etes touche");
            BlindShip_Stat.HP -= AI_enemy.Damage;
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().PlayOneShot(boom);
            Destroy(gameObject, boom.length + 0.5f);
        }
        
        if (gameObject.name == "Cannonball(Clone)" && other.gameObject.name == "Enemy")
        {
            play = true;
            Debug.Log("vous l'avez touche");
            AI_enemy.HP -= BlindShip_Stat.Damage;
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().PlayOneShot(boom);
            Destroy(gameObject, boom.length + 0.5f);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;


public class cubeindicator : MonoBehaviour
{
    public AudioClip[] indications;
    public AudioSource a;

    public Transform tresor;
    public Transform player;
    private float x;
    private float z;

    private bool sedeplacer;
    public int soundtimer;
    public int maxtimer;
    
        
    // Start is called before the first frame update
    void Start()
    {
        //Récupère le controller de l'objet joueur
        // et la position du trésor dans UNITY
        
        transform.position = new Vector3(41f,2f,0f);
        x = 41f;
        z = 0f;
        a = this.GetComponent<AudioSource>();

        sedeplacer = true;
        soundtimer = maxtimer;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(sedeplacer);
        
        
        if (other.gameObject.name == "tresor")
        {
            sedeplacer = false;
        }
        
        
        else
        {
            if (sedeplacer)
            {
                Debug.Log(x);
                Debug.Log(tresor.position.x);
                if (this.x > tresor.position.x + 5)
                {
                    x -= 5f;
                    transform.position = new Vector3(x, 2f, z);
                }
                
                
                else
                {
                    
                    if (this.z > tresor.position.z)
                    { z -= 5f;}
                    else
                    { z += 5f; }

                    transform.position = new Vector3(x, 2f, z);
                }
            }
        }
        
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (sedeplacer)
        {


            if (soundtimer < 0)
            {


                Debug.Log(player.position.x - x);
                if (player.position.x - x > 15 || Math.Abs(player.position.z - z) > 15)
                {
                    a.clip = indications[1];
                    a.Play();

                }
                else
                {
                    a.clip = indications[0];
                    a.Play();
                }

                soundtimer = maxtimer;
            }
            else
            {
                soundtimer -= 1;
            }

        }

    }


}

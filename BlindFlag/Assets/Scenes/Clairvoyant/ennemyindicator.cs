using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;


public class ennemyindicator : MonoBehaviour
{
    public AudioClip[] indications;
    private AudioSource a;

    public GameObject target;
    public GameObject player;
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
       
        transform.position = new Vector3(50f,2f,10f);
        x = 50f;
        z = 10f;
        a = GetComponent<AudioSource>();

        sedeplacer = true;
        soundtimer = maxtimer;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "tresor")
        {
            sedeplacer = false;
        }
        
        
        else
        {
            if (sedeplacer)
            {
                if (this.z < target.transform.position.z)
                {
                    if (this.z+5 > target.transform.position.z)
                    {
                        z += target.transform.position.z - this.z;
                    }
                    else
                    {
                        z += 5f; 
                    }
                    
                    transform.position = new Vector3(x, 2f, z);
                }
                
                else
                {
                    if (this.x > target.transform.position.x)
                    {
                        if ((this.x-5 < target.transform.position.x))
                        {
                            x -= x - target.transform.position.x;
                        }
                        else
                        {
                            x -= 5f; 
                        }
                        
                    }
                    else
                    {
                        if ((this.x+5 > target.transform.position.x))
                        {
                            x += target.transform.position.x-x;
                        }
                        else
                        {
                            x += 5f; 
                        }
                        
                    }

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


                if (player.transform.position.x - x > 15 || Math.Abs(player.transform.position.z - z) > 15)
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;


public class cubeindicator : MonoBehaviour
{
    private float[] tresorposition;
    private cubecontroller cube;
    public AudioClip[] indications;
    public AudioSource a;

    private float x;
    private float z;
        
    // Start is called before the first frame update
    void Start()
    {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      cube = player.GetComponent<cubecontroller>();                   //Récupère le controller de l'objet joueur
        
      tresor tresor = GameObject.FindObjectOfType<tresor>();          //Trouve l'objet trésor
      tresorposition = tresor.Getposition();
        
        transform.position = new Vector3(25.5f,2f,48f);
        x = 25.5f;
        z = 48f;
        indications = new AudioClip[2];
        a = GameObject.FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (x > tresorposition[0] - 3 && x<tresorposition[0]+3 && z > tresorposition[1] - 3 && z<tresorposition[1]+3)
        {
            transform.position = new Vector3(tresorposition[0], 0f, tresorposition[1]);
        }
        else
        {
            if (Math.Abs(cube.x - x) > 15 || Math.Abs(cube.z - z) > 15)
            {
                a.clip = indications[1];
                a.Play();
            }
            else
            {
                if (Math.Abs(cube.x - x) < 5 || Math.Abs(cube.z - z) < 5)
                {
                    Move();
                    a.clip = indications[0];
                    a.Play();
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

    }


    void Move()
    {
        if (x < tresorposition[0])
        {
            x += 10;
        }
        else
        {
            if (x > tresorposition[0])
            {
                x -= 10;
            }
            else
            {
                if (z < tresorposition[0])
                {
                    z += 10;
                }
                else
                {
                    z -= 10;
                }
            }
        }
    }


}

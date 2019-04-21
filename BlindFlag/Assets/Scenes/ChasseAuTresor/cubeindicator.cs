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

    private float x;
    private float z;
        
    // Start is called before the first frame update
    void Start()
    {
        cube = GetComponent(typeof(cubecontroller)) as cubecontroller;
         tresor tresor = GetComponent(typeof(tresor)) as tresor;
         tresorposition = tresor.Getposition();
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
                Synthesis.synthesis("Vous êtes trop loin");
            }
            else
            {
                if (Math.Abs(cube.x - x) < 5 || Math.Abs(cube.z - z) < 5)
                {
                    Move();
                    Synthesis.synthesis("C'est par ici !");
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

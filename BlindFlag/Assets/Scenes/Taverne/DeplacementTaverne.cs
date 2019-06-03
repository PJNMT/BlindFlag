using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeplacementTaverne : MonoBehaviour
{
    private int x;
    private int z;

    public bool sedeplacer;
    
    private KeyCode intputavant;
    private KeyCode intputarrière;
    private KeyCode intputdroit;
    private KeyCode intputgauche;
    public float moveSpeed;
    public float turnSpeed;

   
    void Start()
    {
        intputarrière = KeyCode.DownArrow;
        intputavant = KeyCode.UpArrow;
        intputdroit = KeyCode.RightArrow;
        intputgauche = KeyCode.LeftArrow;
        
        
    }

    //
    void Update()
    {
        if (sedeplacer)
        {
            
            if (Input.GetKey(intputgauche))
            {
                transform.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);
            }

            if (Input.GetKey(intputdroit))
            {
                transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }

            if (Input.GetKey(intputavant))
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(intputarrière))
            {
                transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
            }

            
        }
    }
}

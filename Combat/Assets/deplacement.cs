using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class deplacement : MonoBehaviour
{
    private int x;
    private int y;
    private int z; //si le capitaine se baisse ou non. valeur 1 ou 0 exclusivement.

    CapsuleCollider playercollider;
    private KeyCode intputavant;
    private KeyCode intputarrière;
    private KeyCode intputdroit;
    private KeyCode intputgauche;
    
    
    
    
    /*public deplacement (int posx, int posy, int posz)
    {
      this.x = posx;
        y = posy;
        z = posz;

    }*/

    // Start is called before the first frame update
    void Start()
    {
        intputarrière = KeyCode.W;
        intputavant = KeyCode.Z;
        intputdroit = KeyCode.S;
        intputgauche = KeyCode.Q;
        
        playercollider = gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //deplacement tourner = new deplacement(Capitaine.GetPosx,Capitaine.GetPosy,Capitaine.GetPosz);
        
        if (Input.GetKeyDown(intputgauche))
        {
            transform.Translate(1, 0, 1);
        }
        if (Input.GetKeyDown(intputdroit))
        {
            transform.Translate(-1,0,-1);
        }
        if (Input.GetKeyDown(intputavant)
        {
            transform.Translate(1, 0, -1);
        }
        if (Input.GetKeyDown(intputarrière))
        {
            transform.Translate(-1, 0, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && ausol())
        {
            transform.Translate(0, 1, 0);
            Thread.Sleep(300);
            transform.Translate(0,-1,0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) )
        {
            transform.Translate(0, -1, 0);
            Thread.Sleep(300);
            transform.Translate(0,1,0);
        }
    }

    bool ausol()
    {
      return    Physics.CheckCapsule(playercollider.bounds.center, new Vector3(playercollider.bounds.center.x, playercollider.bounds.center.y -0.1f, playercollider.bounds.center.z), radius )
    }
    //radius à ajouter

    

    
}

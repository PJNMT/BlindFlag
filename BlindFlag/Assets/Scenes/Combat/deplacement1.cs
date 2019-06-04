using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class deplacement1 : MonoBehaviour
{
    private int x;
    private int y;

    private KeyCode intputavant;
    private KeyCode intputarrière;
    private KeyCode intputdroit;
    private KeyCode intputgauche;
    public float moveSpeed;
    public float turnSpeed;

    private AudioSource _audiosource;
    public AudioClip TutoCombat;

    // Start is called before the first frame update
    void Start()
    {
        intputarrière = KeyCode.DownArrow;
        intputavant = KeyCode.UpArrow;
        intputdroit = KeyCode.RightArrow;
        intputgauche = KeyCode.LeftArrow;
        
        if (!BlindCaptain_Stat.Tuto["Combat"])
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => _audiosource.PlayOneShot(TutoCombat));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoCombat.length * 1000 + 500));

            BlindCaptain_Stat.Tuto["Combat"] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //deplacement tourner = new deplacement(Capitaine.GetPosx,Capitaine.GetPosy,Capitaine.GetPosz);
        
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
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
        }
        if (Input.GetKey(intputarrière))
        {
            transform.Translate(-Vector3.forward*moveSpeed*Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow)) //&& ausol())
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
    
}

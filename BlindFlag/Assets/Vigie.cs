using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Vigie : MonoBehaviour
{
    private Dictionary<GameObject, string> ObjetsVus;
    public List<string> Tags;

    private enum direction
    {
        devant = 0,
        tribord = 1,
        babord = 3,
        derrière = 2
    }
    
    
    private void Start()
    {
        ObjetsVus = new Dictionary<GameObject, string>();
        Tags = new List<string>(){"Ennemy","Ile","Visible"};
        
    }


    private void OnTriggerEnter(Collider other)
    {
        /*if(!Tags.Contains(other.tag)) return;*/
        
        Debug.Log(other.tag + " enter" + Direction(other.gameObject));
        ObjetsVus.Add(other.gameObject,other.tag);
        
        
        
    }

    private direction Direction(GameObject TwT)
    {
        
    }
    
    private double Distance(GameObject O_O)
    {
        return Math.Sqrt(Math.Pow(O_O.transform.position.x - transform.position.x, 2)+
                         Math.Pow(O_O.transform.position.z - transform.position.z, 2));
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (ObjetsVus.ContainsKey(other.gameObject))
        {
            //J'ai perdu 'other' de vu
            //Synthesis.synthesis("J'ai perdu " + other.name + " de vue");
            Debug.Log("perdu " + other.tag);
            
            ObjetsVus.Remove(other.gameObject);
        }
    }
}


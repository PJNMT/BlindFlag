using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Vigie : MonoBehaviour
{
    private Dictionary<GameObject, string> ObjetsVus;
    public List<string> Tags;
    
    private void Start()
    {
        ObjetsVus = new Dictionary<GameObject, string>();
        Tags = new List<string>(){"Ennemy","Ile","Visible"};
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(!Tags.Contains(other.tag)) return;
        
        ObjetsVus.Add(other.gameObject,other.tag);
        
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (ObjetsVus.ContainsKey(other.gameObject))
        {
            //J'ai perdu 'other' de vu
            Synthesis.synthesis("J'ai perdu " + other.name + " de vue");
            
            
            ObjetsVus.Remove(other.gameObject);
        }
            
            
    }
}


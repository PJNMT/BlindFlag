using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vigie : MonoBehaviour
{
    private Dictionary<GameObject, string> ObjetsVus;
    public List<string> Tags;
    private GameObject player;

    
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
        
        foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
        {
            if (o.tag == "player")
            {
                player = o.gameObject;
                break;
            }
                        
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        /*if(!Tags.Contains(other.tag)) return;*/
        
        Debug.Log(other.tag + " enter" + Direction(other.gameObject));
        ObjetsVus.Add(other.gameObject,other.tag);
        
        
        
    }

    private direction Direction(GameObject TwT)
    {
        if (Distance(TwT, 0, -30) >= Distance(TwT, 40, 0) && Distance(TwT, 0, -30) >= Distance(TwT, -40, 0))
            return direction.babord;
        if (Distance(TwT, 0, 30) >= Distance(TwT, 40, 0) && Distance(TwT, 0, 30) >= Distance(TwT, -40, 0))
            return direction.tribord;
        if (Distance(TwT, 0, 30) < Distance(TwT, -40, 0) && Distance(TwT, 0, -30) < Distance(TwT, -40, 0))
            return direction.devant;
        
        

        return direction.derrière;
    }
     
    private double Distance(GameObject oO,int x, int z)
    {
        return Math.Sqrt(Math.Pow(oO.transform.position.x - player.transform.position.x + x, 2)+
                         Math.Pow(oO.transform.position.z - player.transform.position.z + z, 2));
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


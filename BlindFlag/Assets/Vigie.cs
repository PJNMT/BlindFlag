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
        AuNord = 0,
        AlEst = 1,
        AlOuest = 3,
        AuSud = 2
    }
    
    
    private void Start()
    {
        ObjetsVus = new Dictionary<GameObject, string>();
        Tags = new List<string>(){"Ennemy","Ile","Visible"};
        
        foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
        {
            if (o.tag == "player" || o.name == "Ship")
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
        if (Distance(TwT, 0, -100) >= Distance(TwT, 100, 0) && Distance(TwT, 0, -100) >= Distance(TwT, -100, 0))
            return direction.AlOuest;
        if (Distance(TwT, 0, 100) >= Distance(TwT, 100, 0) && Distance(TwT, 0, 100) >= Distance(TwT, -100, 0))
            return direction.AlEst;
        if (Distance(TwT, 0, 100) < Distance(TwT, -100, 0) && Distance(TwT, 0, -100) < Distance(TwT, -100, 0))
            return direction.AuNord;
        
        

        return direction.AuSud;
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


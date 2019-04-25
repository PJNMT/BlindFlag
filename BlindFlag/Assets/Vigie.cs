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
        if(!Tags.Contains(other.tag)) return;
        
        ObjetsVus.Add(other.gameObject,other.tag);
        
        
        
    }

    private direction Direction(GameObject TwT)
    {
        float alpha = this.transform.eulerAngles.y;
        if (TwT.transform.position.x < transform.position.x + 30* Mathf.Sin(alpha) &&
            TwT.transform.position.z < transform.position.z + 30*Mathf.Cos(alpha)) 
            return direction.babord;
        if (TwT.transform.position.x < transform.position.x + 30* Mathf.Sin(alpha) &&
            TwT.transform.position.z >= transform.position.z + 30*Mathf.Cos(alpha)) 
            return direction.devant;
        if (TwT.transform.position.x >= transform.position.x + 30* Mathf.Sin(alpha) &&
            TwT.transform.position.z < transform.position.z + 30*Mathf.Cos(alpha)) 
            return direction.derrière;
        /*if (TwT.transform.position.x >= transform.position.x + 30* Mathf.Sin(alpha) &&
            TwT.transform.position.z >= transform.position.z + 30*Mathf.Cos(alpha))*/
        return direction.tribord;
        
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


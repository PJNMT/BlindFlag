using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Vigie : MonoBehaviour
{
    private string trucAdire = "Chose vers ";
    private void OnTriggerEnter(Collider other)
    {

        
       /* if (other.GetComponent(typeof(WhatToSay)) )
        {
            trucAdire = other.name + " vers ";
            
            bool babord = (other.transform.position.x - this.transform.position.x < -75);
            bool tribord = (other.transform.position.x - this.transform.position.x > 75);
            bool devant = !(babord || tribord) && other.transform.position.z > transform.position.z;
            bool derriere = !(babord || tribord) && other.transform.position.z <= transform.position.z;

            if (transform.rotation.eulerAngles.y > 45 && transform.rotation.eulerAngles.y < 135) ;
        
        
        
        
            if(derriere) trucAdire += " le sud "/*derriere.ToString()*/;
            //if(babord) trucAdire += " l'ouest "/*babord.ToString()*/;
           // if(tribord) trucAdire += " l'est "/*tribord.ToString()*/;
           // if(devant) trucAdire += " le nord "/*devant.ToString()*/;
        
           // Debug.Log(trucAdire);
            //Synthesis.synthesis(trucAdire);
            //trucAdire = other.name + " vers ";

       // }
        
        

        /*bool babord = (other.transform.position.x - this.transform.position.x < -75);
        bool tribord = (other.transform.position.x - this.transform.position.x > 75);
        bool devant = !(babord || tribord) && other.transform.position.z > transform.position.z;
        bool derriere = !(babord || tribord) && other.transform.position.z <= transform.position.z;

        if(transform.rotation.eulerAngles.y > 45 && transform.rotation.eulerAngles.y<135)
        
        
        
        
     /*   if(derriere) trucAdire += " le sud "/*derriere.ToString()*/;
      /*  if(babord) trucAdire += " l'ouest "/*babord.ToString()*/;
       /* if(tribord) trucAdire += " l'est "/*tribord.ToString()*/;
      /*  if(devant) trucAdire += " le nord "/*devant.ToString()*/;
        /*
        Debug.Log(trucAdire);
        trucAdire = "chose a "; */
    }
    
    
}


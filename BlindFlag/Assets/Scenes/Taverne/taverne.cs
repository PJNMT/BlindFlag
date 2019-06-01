using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class taverne : MonoBehaviour
{
    public AudioSource[] _audioSources;
    private bool activated;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You")
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(
                "Que voulez-vous faire Captaine ? Offrir une tournée générale à ces pirates, chercher de nouvelles recrues ou leur montrer qui est le meilleur chanteur " +
                "de chansons pirates de toutes les Caraïbes ?"));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(10000));
            
            
            Recognition.Function Traitement = this.Traitement;
            Recognition.start_recognition(Traitement,
                "chanson chanter jouer boire simon payer tournée recruter recrue nouveau équipage matelot", 60);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Recognition.stop_recognition();
            activated = false;
        }
    }


    void Traitement(string input)
    {
        activated = true;
        Debug.Log(input);
        switch (input)
        {
           
               
           case "simon":
           case "jouer":
           case "chanter":
                   
               UnityMainThreadDispatcher.Instance().Enqueue(() => transform.position = new Vector3(3.64f,1f,3.65f));
               
               break;
           
           case "recruter":
               case "recrue":
               case "nouveau":
                case "matelot" :
               case "équipage":

               int crew_members = 0;
               UnityMainThreadDispatcher.Instance().Enqueue(() => crew_members = Random.Range(0, BlindShip_Stat.Max_Crew - BlindShip_Stat.Crew));
               Synthesis.synthesis("Vous avez recruté"+ crew_members +" membres d'équipage.");
               BlindShip_Stat.Crew += crew_members;
               Thread.Sleep(3000);
               //Menu();
               Debug.Log("menu");
               break;
           
           
           case "payer":
           case "tournée":
           case "boire":
               int available_money = BlindShip_Stat.Money -= (BlindShip_Stat.Crew*20);
               if (available_money<0)
               {
                   BlindCaptain_Stat.Reputation -= 10;
                   Synthesis.synthesis("Vous n'avez pas suffisament d'argent.");
               }
               else
               {
                   BlindCaptain_Stat.Reputation += 10;
                   BlindShip_Stat.Money = available_money;
                   //AudioSource.PlayClipAtPoint();  enthousiasme des pirates
                   Synthesis.synthesis("Vous avez payé avec succès vos pirates! Bien joué !");      
               }
               //OnTriggerEnter(other:);
               break;
           case "quitter":
               LoadScene.Load(LoadScene.Scene.Port, LoadScene.Scene.Taverne);
               break;
           
           default:
           Synthesis.synthesis("Je n'ai pas compris ce que vous avez dit.");
           activated = false;
           //Start();
           break;
                
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taverne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Que voulez-vous faire Captaine ? Offrir une tournée générale à ces pirates, chercher de nouvelles recrues ou leur montrer qui est le meilleur chanteur " +
                            "de chansons pirates de toutes les Caraïbes ?");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(10, "chanson chanter jouer boire simon payer tournée recruter recrue", Traitement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Traitement(string input)
    {
        switch (input)
        {
           
               
           case "simon":
           case "jouer":
           case "chanter":
                   
               transform.position = new Vector3(-3.95f,1f,2.82f);
               break;
           
           case "recruter":
               int crew_members = Random.Range(0, BlindShip_Stat.Max_Crew - BlindShip_Stat.Crew);
               Synthesis.synthesis("Vous avez recruté"+ crew_members +" membres d'équipage.");
               BlindShip_Stat.Crew += crew_members;
               Start();
               break;
           case "payer":
           case "tournée":
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
               Start();
               break;
           
               default:
               Synthesis.synthesis("Je n'ai pas compris ce que vous avez dit.");
               Start();
               break;
                
        }
    }
}

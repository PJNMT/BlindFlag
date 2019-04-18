using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taverne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Que voulez vous faire Captaine ? Offrir une tournée générale à ces pirates ou le montrer qui est le meilleur chanteur" +
                            "de chanson pirate de toutes les caraibes ?");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(20, "chanson chanter jouer boire payer tournée", Traitement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Traitement(string input)
    {
        switch (input)
        {
           case "tournée":
           case "boire":
           case "payer":
               BlindShip_Stat.Money -= 20;
               //AudioSource.PlayClipAtPoint();  enthousiasme des pirates
            break;
           
           case "chanter":
           case "chanson":
           case "jouer":
               transform.position = new Vector3(5,0,5);
               break;
                
        }
    }
}

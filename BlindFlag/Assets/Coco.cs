using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Coco : MonoBehaviour
{  
    public static string speech;
    public static string[] Dico_1;
    public static string[] Dico_2;

    public GameObject Cannonball;
    void Cocotraitement(string word)
    {
        Coco.speech = Coco.speech + word + " ";
        string[] words = Coco.speech.Split(' ');
        
        Debug.Log(Coco.speech);

        if (words.Length > 2)
        {
            if ((Coco.Dico_1).Contains(words[0]) && Coco.Dico_2.Contains(words[1]))
            {

                switch (words[1])
                {
                case   "statistique":
                case "stat":
                 Synthesis.synthesis("Quelle statistique veut tu connaitre ? ton niveau, tes XP, HP, les statistiques de ton navire," +
                                     "tes points de dommages à l'épée ou au pistolet ou tes points de réputation ?");
                 break;
             
             
             case "niveau":
                 Synthesis.synthesis("Blindcaptain niveau"+ BlindCaptain_Stat.Lvl);
                 break;
             
             case "bateau" :
             case "navire":
                 Synthesis.synthesis(" Tu as un bateau niveau " + BlindShip_Stat.Lvl +
                                     " avec " + BlindShip_Stat.Crew + "homme d'équipage." + 
                                     " Il peut contenir au maximum "
                                     + BlindShip_Stat.Max_Crew +
                                     " Ton bateau possède " + BlindShip_Stat.XP + " X P " +
                                      BlindShip_Stat.HP + " H P" + "et peut causer" 
                                     + BlindShip_Stat.Damage + " point de dommage." + " Enfin, il a "
                                     + BlindShip_Stat.Shield + " point de capacité de défense");
                 break;
             
             case "vie":
             case "HP":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.HP + " point de vie." + "Ton bateau à " + BlindShip_Stat.HP);
                 break;
             
             case "réputation":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.Reputation + " point de réputation.");
                 break;
             
             case "XP":
             case "experience":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.XP + " point de d'experience.");
                 break;
             
             case "épée":
             case "sabre":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.SwordDamage + " point de dommmage à l'épée.");
                 break;
             
             case "pistolet":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.GunDamage + " point de dommage au pistolet.");
                 break;
                 
             case "sauver":
             case "sauvegarder":
             case "enregistrer":
                 //TODO: Ajouter le script de save !
                 break;
             
             case "quitter":
                 Synthesis.synthesis("Tu es sur que tu veux quitter sans sauvegarder ? Si oui appuyer sur Espace");
                 if (Input.GetKey(KeyCode.Space))
                 {
                     //Scripte de save
                 }
                 break;
                }
                
                
                
            }

            Coco.speech = "";
        }
        
        else if (!Coco.Dico_1.Contains(words[0])) Coco.speech = "";
    }
    
    void Start()
    {
        speech = "";
        Dico_1 = new[]
        {
            "Coco",
            
        };
        Dico_2 = new[]
        {
            "statistiques", "stats", "bateau", "vie", "HP", "XP", "niveau", "sauver",
            "sauvegarder", "enregistrer", "quitter", "réputation", "navire", "experience","experience"
        };
        
        Recognition.Function Coco = Cocotraitement;
        
        Recognition.start_recognition(0, "statistiques stats bateau vie HP XP niveau sauver sauvegarder" +
                                         "enregistrer quitter réputation navire experience experience", Coco);
    }

   
}

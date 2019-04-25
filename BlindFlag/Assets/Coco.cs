using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class Coco : MonoBehaviour
{  
    public static string speech;
    public static string[] Dico_1;
    public static string[] Dico_2;

    void Cocotraitement(string word)
    {
        Coco.speech = Coco.speech + word + " ";
        string[] words = Coco.speech.Split(' ');
        
        Debug.Log(word);

        if (words.Length > 2)
        {
            Synthesis.synthesis("Coco activé");
            if ((Coco.Dico_1.Contains(words[0])) && Coco.Dico_2.Contains(words[1]))
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
                 Thread.Sleep(10000);
                 break;
             
             
             case "vie":
             case "HP":
                 Synthesis.synthesis("vie");
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.HP + " point de vie." + "Ton bateau à " + BlindShip_Stat.HP);
                 Thread.Sleep(3000);
                 break;
             
             case "réputation":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.Reputation + " point de réputation.");
                 Thread.Sleep(3000);
                 break;
             
             case "XP":
             case "experience":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.XP + " point de d'experience.");
                 Thread.Sleep(3000);
                 break;
             
             case "épée":
             case "sabre":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.SwordDamage + " point de dommmage à l'épée.");
                 Thread.Sleep(3000);
                 break;
             
             case "pistolet":
                 Synthesis.synthesis("Tu as " + BlindCaptain_Stat.GunDamage + " point de dommage au pistolet.");
                 Thread.Sleep(3000);
                 break;
                 
             case "sauver":
             case "sauvegarder":
             case "enregistrer":
                 //script save
                 break;
             
             case "quitter":
                 Synthesis.synthesis("Tu es sur que tu veux quitter sans sauvegarder ? Si oui appuyer sur Espace");
                 Thread.Sleep(4000);
                 if (Input.GetKey(KeyCode.Space))
                 {
                     //Scripte de save
                 }
                 break;
            
            case "gueule":
            case "ferme":
            case "enculé":
            case "connasse":
            case "pute":
            case "salope":
            case "grosse":
                Synthesis.synthesis("Nique ta mère");
                Thread.Sleep(2000);
                break;
                 
             default:
                 Synthesis.synthesis("Je n'ai aps compris ce que vous vouliez, Capitaine");
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
            "Ok", "Coco"
        };
        
        Dico_2 = new[]
        {
            "statistiques", "stats", "bateau", "vie", "HP", "XP", "niveau", "sauver",
            "sauvegarder", "enregistrer", "quitter", "réputation", "navire", "experience","experience", "gueule","ferme",
            "salope","pute","connasse","enculé","grosse"
            
        };
        
        Recognition.Function Coco = Cocotraitement;
        
        Recognition.start_recognition(0, "Ok Coco statistiques stats bateau vie HP XP niveau sauver sauvegarder" +
                                         "enregistrer quitter réputation navire experience experience gueule ferme pute salope" +
                                         "connasse enculé grosse", Coco);
    }

   
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using static UnityEngine.Time;

public class Coco : MonoBehaviour
{
    public static bool TutoON;
    
    void Cocotraitement(string words)
    {
        Debug.Log(words);
        string[] decoupe = words.Split(' ');
        foreach (string word in decoupe)
        {
            switch (word)
            {
               case "niveau":
                    Synthesis.synthesis("Blindcaptain niveau" + BlindCaptain_Stat.Lvl);
                    break;

                case "bateau":
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
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.HP + " point de vie." + "Ton bateau à " +
                                        BlindShip_Stat.HP);
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

            }
            
            
        }

    }
    
    void Start()
    {
        if (TutoON)
        {
            Synthesis.synthesis("Je suis Coco, votre assistant euh votre perroquet Capitaine ! Vous pouvez me demander vos statistiques de Capitaine" +
                                "celles de votre navire. Je peux également vous proposer de sauvegarder votre partie ou aller au menu options sonores"
                                + "Allez y testez moi !");
            TutoON = false;
        }
        
        Recognition.Function OkCoco = TraitementCoco;
        Recognition.start_recognition(OkCoco);
        
    }

    void TraitementCoco(string mot)
    {
        Debug.Log("Coco" + mot);
        if (mot == "Ok Coco")
        {
            Synthesis.synthesis("Coco Activé");
            Time.timeScale = 0.0f;
            
            Recognition.stop_recognition();
            Recognition.Function AnswerCoco = TraitementCoco;
            Recognition.start_recognition(AnswerCoco);
        }
        else
        {
            Recognition.stop_recognition();
            Recognition.Function OkCoco = TraitementCoco;
            Recognition.start_recognition(OkCoco); 
        }
    }

   
}

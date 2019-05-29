using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using static System.Threading.Thread;
using static UnityEngine.Time;

public class Coco : MonoBehaviour
{
    public static bool TutoON;
    public bool activated;
    public bool quit;
    
    
    void Answertraitement(string word)
    {
        Debug.Log(word);
           switch (word)
            {
               case "niveau":
                   
                    Synthesis.synthesis("Blindcaptain niveau" + BlindCaptain_Stat.Lvl);
                    break;

                case "bateau":
                case "navire":
                    
                    Synthesis.synthesis(" Tu as un bateau niveau " + BlindShip_Stat.Lvl +
                                        " avec " + BlindShip_Stat.Crew + " homme d'équipage.  " +
                                        " Il peut contenir au maximum "
                                        + BlindShip_Stat.Max_Crew + "matelots.   " +
                                        " Ton bateau possède " + BlindShip_Stat.XP + " X P " +
                                        BlindShip_Stat.HP + " H P" + "et peut causer"
                                        + BlindShip_Stat.Damage + " point de dommage." + " Enfin, il a "
                                        + BlindShip_Stat.Shield + " point de capacité de défense");
                    Sleep(15000);
                    break;


                case "vie":
                case "HP":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.HP + " point de vie." + "Ton bateau à " +
                                        BlindShip_Stat.HP);
                    Sleep(4000);
                    break;

                case "réputation":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.Reputation + " point de réputation.");
                    Sleep(3000);
                    break;

                case "XP":
                case "experience":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.XP + " point de d'experience.");
                    Sleep(3000);
                    break;

                case "épée":
                case "sabre":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.SwordDamage + " point de dommmage à l'épée.");
                    Sleep(3000);
                    break;

                case "pistolet":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.GunDamage + " point de dommage au pistolet.");
                    Sleep(3000);
                    break;

                case "sauver":
                case "sauvegarder":
                case "enregistrer":
                    Debug.Log("sauver");
                    break;

                case "quitter":
                    Application.Quit(1);
                    break;

               case "non":
               case "rien":
               case "merci":
                   Synthesis.synthesis("Ok Capitaine");
                   quit = true;
                   break;
            }

        if (!quit)
        {
            Synthesis.synthesis("Voulez vous autre chose capitaine ?"); 
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
        Recognition.start_recognition(OkCoco,"Coco",0);

        quit = true;
        
    }

    private void Update()
    {
        if (activated)
        {
            
            Debug.Log("coco activé");
            Recognition.stop_recognition();
            
            UnityMainThreadDispatcher.Instance().Enqueue((() => Paused()));
            
            Recognition.Function AnswerCoco = Answertraitement;
            Debug.Log("nouvelle reco");
            Recognition.start_recognition(AnswerCoco, "non , rien , merci , niveau , bateau , navire , HP , vie , XP , réputation , épée , sabre, pistolet" +
                                                      "sauver , sauvegarder , quitter", 0);
            activated = false;
            Debug.Log("désactivé");
        }

        if (quit)
        {
            Recognition.stop_recognition();
            quit = false;
            Recognition.Function OkCoco = TraitementCoco;
            Recognition.start_recognition(OkCoco,"Coco",0); 
            Play();
        }

    }

    void TraitementCoco(string mot)
    {
        if (mot == "Coco")
        {
            Synthesis.synthesis("Coco Activé");
            activated = true;
        }
       
    }

    public static void Paused()
    {
        Time.timeScale = 0.0f;
    }

    public static void Play()
    {
        Time.timeScale = 1f;
    }
   
}

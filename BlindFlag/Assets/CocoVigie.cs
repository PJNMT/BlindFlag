using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Scenes

{
    public class CocoVigie : MonoBehaviour
    {
    public static Vigie baba;
    public static bool TutoON;
    public bool activated;
    public bool quit;
        
        
    public bool pauseCoco;
        
   static WindowsMicrophoneMuteLibrary.WindowsMicMute micMute;
    
    
    void Answertraitement(string word)
    {
        micMute.MuteMic();
        Debug.Log(word);
        if (pauseCoco)
        {
            switch (word)
            {
                //Coco case
                case "niveau":

                    Synthesis.synthesis("Blindcaptain niveau" + BlindCaptain_Stat.Lvl);
                    
                    Thread.Sleep(5000);
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
                    Thread.Sleep(15000);
                    break;


                case "vie":
                case "HP":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.HP + " point de vie." + "Ton bateau à " +
                                        BlindShip_Stat.HP);
                    Thread.Sleep(4000);
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
                    break;

                case "pistolet":
                    Synthesis.synthesis("Tu as " + BlindCaptain_Stat.GunDamage + " point de dommage au pistolet.");

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
        }
        else
        {

            switch (word)
            {

                //Vigie case
                case "Vois":
                    foreach (KeyValuePair<GameObject, string> pair in baba.ObjetsVus)
                    {
                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            Synthesis.synthesis(pair.Value + " en vue " + baba.Direction(pair.Key.gameObject)));

                    }

                    quit = true;
                    break;

                case "Ennemy":

                    foreach (KeyValuePair<GameObject, string> objetsVu in baba.ObjetsVus)
                    {
                        if (objetsVu.Value == "Ennemy")
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                Synthesis.synthesis(objetsVu.Value + " en vue " +
                                                    baba.Direction(objetsVu.Key.gameObject)));

                        }
                    }

                    quit = true;
                    break;

                case "Ile":
                    Debug.Log("ok captain");
                    foreach (KeyValuePair<GameObject, string> objetsVu in baba.ObjetsVus)
                    {
                        if ((objetsVu.Value == "Port" || objetsVu.Value == "Ile au trésor"))
                        {
                            Debug.Log("ok captain");
                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                Synthesis.synthesis(objetsVu.Value + " en vue " +
                                                    baba.Direction(objetsVu.Key.gameObject)));

                        }
                    }

                    quit = true;
                    break;
            }
        }
        
       //micMute.UnMuteMic(); 
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
        
        Recognition.Function OkCoco = Traitement_Call;
        Recognition.start_recognition(OkCoco,"Coco baba vigie",0);

        baba = GameObject.FindObjectOfType<Vigie>();

        quit = false;
        micMute = new WindowsMicrophoneMuteLibrary.WindowsMicMute();

    }

    private void Update()
    {
        if (activated)
        {
            
            Debug.Log("demande detecté");
            Recognition.stop_recognition();
            
            Recognition.Function AnswerCoco = Answertraitement;
            Debug.Log("nouvelle reco");
            //micMute.UnMuteMic();
            Recognition.start_recognition(AnswerCoco, "non rien merci niveau bateau navire HP vie XP réputation épée sabre pistolet" +
                                                      "sauver sauvegarder quitter Ennemy Ile Vois Bateau ", 0);
            activated = false;
            Debug.Log("désactivé");
        }

        if (quit)
        {
            Recognition.stop_recognition();
            quit = false;
            
            
            Recognition.Function Indication = Traitement_Call;
            Recognition.start_recognition(Indication,"Coco baba vigie",0);

            if (pauseCoco)
            {
                UnityMainThreadDispatcher.Instance().Enqueue((() => Coco.Play()));
            }
            
        }

    }

    void Traitement_Call(string mot)
    {
        
        if (mot == "Coco")
        {
            
            UnityMainThreadDispatcher.Instance().Enqueue(() =>Synthesis.synthesis("Coco Activé"));
            activated = true;
            
            //micMute.MuteMic();
            UnityMainThreadDispatcher.Instance().Enqueue((() => Coco.Paused()));
            pauseCoco = true;
        }

       if (mot == "baba" || mot == "vigie")
        {
            //micMute.MuteMic();
            UnityMainThreadDispatcher.Instance().Enqueue(() =>Synthesis.synthesis("Oui Capitaine ?"));
            Debug.Log("baba");
            activated = true;
        }
       
    }
        
        
    
    
    
  
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coco : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(0,"coco", Traitement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Traitement(string input)
    {
        switch (input)
        {
             case   "coco":
                 Recognition.Function Demande = this.Demande;
                 Recognition.start_recognition(10, "statistiques stats bateau vie niveau sauver sauvegarder enregistrer quitter réputation", Demande);
                 break;
        }
    }


    void Demande(string input)
    {
        switch (input)
        {
             case   "statistique":
             case "stat":
                 Synthesis.synthesis("Quelle statistique veut tu connaitre ? ton niveau, tes XP, HP, les statistiques de ton navire," +
                                     "tes points de dommages à l'épée ou au pistolet ou tes points de réputation ?");
                 Recognition.start_recognition(10,"vie HP XP experience bateau navire épée sabre pistolet dommage réputation niveau", Demande);
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
                 Synthesis.synthesis("Tu es sur que tu veux quitter sans sauvegarder ?");
                 Recognition.Function Reponse = ReponseQuittersanssauver;
                 Recognition.start_recognition(10, "oui non", Reponse);
                 break;
        }
        
    }

    void ReponseQuittersanssauver(string input)
    {
        switch (input)
        {
                case "oui":
                    Application.Quit(1);
                    break;
                case "non":
                    //TODO: Ajouter script save
                    Application.Quit(0);
                    break;
        }
    }
}

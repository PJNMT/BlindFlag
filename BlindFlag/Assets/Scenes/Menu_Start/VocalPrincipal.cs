using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Bienvenue capitaine ! Vous voici dans l'Univers de BlindFlag. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options quitter", 10);
    }

    void Restart()
    {
        Synthesis.synthesis("Je n'ai pas compris votre demande. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options quitter", 10);
    }

    // Update is called once per frame
    void Traitement(string input)
    {
        switch(input)
        {
            /*case "continuer":
                Save.LoadGame();
                break;
            case "commencer":
                
            case "option":
                GameObject.Find("MainMenuPanel").SetActive(false);
                GameObject.Find("OptionPanel").SetActive(true);
                VocalOptions.Start();
            case "quitter":
                QuitOnClick.Quit();
                break;
            default:
                Restart();
                break;*/

        }
    }
}

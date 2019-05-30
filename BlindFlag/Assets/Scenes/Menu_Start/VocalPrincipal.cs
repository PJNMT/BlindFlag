using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalPrincipal : MonoBehaviour
{
    private VocalOptions vocal;
    // Start is called before the first frame update
    void Start()
    {
        vocal = GetComponent<VocalOptions>();
        Synthesis.synthesis("Bienvenue capitaine ! Vous voici dans l'Univers de BlindFlag. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options quitter paramètre paramètres", 10);
    }

    void Restart()
    {
        Synthesis.synthesis("Je n'ai pas compris votre demande. Voici vos commandes : continuer, commencer, options, option, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options quitter", 10);
    }

    // Update is called once per frame
    void Traitement(string input)
    {
        Debug.Log(input);
        switch(input)
        {
            case "continuer":
                Save.LoadGame();
                break;
            case "commencer":
                
            case "option":
            case "options":
            case "paramètres":
            case "paramètre":
                GameObject.Find("MainMenuPanel").SetActive(false);
                GameObject.Find("OptionPanel").SetActive(true);
                vocal.enabled = true;
                break;
            case "quitter":
                QuitOnClick.Quit();
                break;
            default:
                Restart();
                break;

        }
    }
}

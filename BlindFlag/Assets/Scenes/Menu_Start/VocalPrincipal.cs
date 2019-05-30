using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VocalPrincipal : MonoBehaviour
{
    private VocalOptions vocal;

    private Start_ShipStats shipstats;

    private Start_CaptainStats captstats;
    // Start is called before the first frame update
    void Start()
    {
        vocal = GetComponent<VocalOptions>();
        Synthesis.synthesis("Bienvenue capitaine ! Vous voici dans l'Univers de BlindFlag. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options quitter paramètre paramètres", 20);
    }

    void Restart()
    {
        Synthesis.synthesis("Je n'ai pas compris votre demande. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options option paramètre paramètres quitter", 10);
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
                SceneManager.LoadScene(7);
                shipstats.enabled = true;
                captstats.enabled = true;
                break;
            case "option":
            case "options":
            case "paramètres":
            case "paramètre":
                GameObject.Find("MainMenuPanel").SetActive(false);
                GameObject.Find("OptionPanel").SetActive(true);
                VocalOptions.is_menu = true;
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

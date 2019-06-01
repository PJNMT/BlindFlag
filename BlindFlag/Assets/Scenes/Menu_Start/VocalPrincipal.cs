using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VocalPrincipal : MonoBehaviour
{
    private Start_ShipStats shipstats;
    public GameObject Optionpanel;

    private Start_CaptainStats captstats;
    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Bienvenue capitaine ! Vous voici dans l'Univers de BlindFlag. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Thread.Sleep(7000);
        Recognition.start_recognition(Traitement, "continuer commencer options quitter parametre parametres", 200);
    }

    void Restart()
    {
        Synthesis.synthesis("Je n'ai pas compris votre demande. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "continuer commencer options option parametre parametres quitter", 10);
    }

    // Update is called once per frame
    void Traitement(string input)
    {
        switch(input)
        {
            case "continuer":
                Save.LoadGame();
                break;
            case "commencer":
                UnityMainThreadDispatcher.Instance().Enqueue(() => LoadScene.Load((LoadScene.Scene) 7, LoadScene.Scene.START));
                shipstats.enabled = true;
                captstats.enabled = true;
                break;
            case "option":
            case "options":
            case "parametres":
            case "parametre":
                UnityMainThreadDispatcher.Instance().Enqueue(() => Optionpanel.SetActive(true));
                VocalOptions.is_menu = true;
                UnityMainThreadDispatcher.Instance().Enqueue(() => GameObject.Find("MainMenuPanel").SetActive(false));
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

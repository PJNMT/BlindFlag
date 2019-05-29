using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Le menu d'options vous permet de changer le volume des effets suivants : sons, musiques, voix. Que voulez-vous modifier ?");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "son musique voix quitter", 10);
    }

    void Restart()
    {
        Synthesis.synthesis("Je n'ai pas compris votre demande. Voici vos commandes : son, musique, voix, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(Traitement, "son musique voix quitter", 10);
    }

    // Update is called once per frame
    void Traitement(string input)
    {
        switch(input)
        {
            /*case "son":
                SettingsMenu.SetLevel(setlvl + 10);
                break;
            case "musique":
            //start a new game
            case "voix":
            //lance le menu option
            case "quitter":
                GameObject.Find("MainMenuPanel").SetActive(true);
                GameObject.Find("OptionPanel").SetActive(false);
                VocalPrincipal.Start();
                break;
            default:
                Restart();
                break;*/

        }
    }
}

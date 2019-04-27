using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Starting : MonoBehaviour
{

    public VideoPlayer videovague;
    public RawImage rawimage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
        Synthesis.synthesis("Bienvenue capitaine ! Vous voici dans l'Univers de BlindFlag. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(10, "continuer commencer options quitter", Traitement);
    }

    IEnumerator PlayVideo()
    {
        videovague.Prepare();
        WaitForSeconds waitforscds = new WaitForSeconds(1f);
        while (!videovague.isPrepared)
        {
            yield return waitforscds;
            break;
        }
        rawimage.texture = videovague.texture;
        videovague.Play();

    }

    void Restart()
    {
        Synthesis.synthesis("Je n'ai pas compris votre demande. Voici vos commandes : continuer, commencer, options, quitter. Je vous écoute.");
        Recognition.Function Traitement = this.Traitement;
        Recognition.start_recognition(10, "continuer commencer options quitter", Traitement);
    }

    // Update is called once per frame
    void Traitement(string input)
    {
        switch(input)
        {
            case "continuer":
                //load la partie save
            case "commencer":
                //start a new game
            case "option":
                //lance le menu option
            case "quitter":
                Application.Quit();
                break;
            default:
                Restart();
                break;

        }
    }
}

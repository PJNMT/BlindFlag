using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class VocalOptions : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static bool is_menu = false;

    private float setlvl;

    private string barremixer;

    private Coco coco;
    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Le menu d'options vous permet de changer le volume des effets suivants : sons, musiques, voix. Que voulez-vous modifier ?");
        Recognition.Function Traitement = this.Traitement;
        Thread.Sleep(3000);
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
        Debug.Log(input);
        string GroupMix = "";
        
        switch(input)
        {
            case "son":
                barremixer = "SoundVol";
                Synthesis.synthesis("Voulez-vous augmenter ou diminuer le son?");
                Recognition.Function Traitement1 = this.Traitementplusmoins;
                Recognition.start_recognition(Traitement1, "plus moins augmenter diminuer", 10);
                break;
            case "musique":
                barremixer = "MusicVol";
                Synthesis.synthesis("Voulez-vous augmenter ou diminuer la musique?");
                Recognition.Function Traitement2 = this.Traitementplusmoins;
                Recognition.start_recognition(Traitement2, "plus moins augmenter diminuer", 10);
                break;
            case "voix":
                barremixer = "VoicesVol";
                Synthesis.synthesis("Voulez-vous augmenter ou diminuer le son?");
                Recognition.Function Traitement3 = this.Traitementplusmoins;
                Recognition.start_recognition(Traitement3, "plus moins augmenter diminuer", 10);
                break;
            case "quitter":
                if (is_menu)
                {
                    GameObject.Find("MainMenuPanel").SetActive(true);
                    GameObject.Find("OptionPanel").SetActive(false);
                    is_menu = false;
                }
                else
                {
                    coco.enabled = true;
                }
                break;
            default:
                Restart();
                break;

        }
    }

    void Traitementplusmoins(string input)
    {
        switch (input)
        {
            case "plus":
            case"augmenter":
                audioMixer.GetFloat(barremixer, out setlvl);
                audioMixer.SetFloat(barremixer, setlvl + 0.1f);
                break;
            case "moins":
            case "diminuer":
                audioMixer.GetFloat(barremixer, out setlvl);
                audioMixer.SetFloat(barremixer, setlvl + 0.1f);
                break;
            default:
                Synthesis.synthesis("Je n'ai pas compris. Voulez-vous augmenter ou diminuer le son?");
                Recognition.Function Traitement3 = this.Traitementplusmoins;
                Recognition.start_recognition(Traitement3, "plus moins augmenter diminuer", 10);
                break;
        }
    }
}

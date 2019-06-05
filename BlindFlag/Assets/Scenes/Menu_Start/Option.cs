using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    public AudioMixer audioMixer;

    private float setlvl;
    private string barremixer;
    
    public GameObject OptionMenu;
    public GameObject MainMenu;

    public AudioClip UpDown;
    public AudioClip WhatDoUWant;
    public AudioClip Menu;
    
    private AudioSource Audio;
    
    public static bool relaunch;
    
    
    // Start is called before the first frame update
    void Start()
    {
        OptionLaunch();
    }

    void OptionLaunch()
    {
        Audio = GetComponent<AudioSource>();
        relaunch = false;
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Menu));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Menu.length * 1000 + 500));

        Launch();
    }

    private void Update()
    {
        if (relaunch) OptionLaunch();
    }

    void Launch()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WhatDoUWant));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WhatDoUWant.length * 1000 + 500));

        Recognition.Function Func = Choice;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "stop retour rien aucun quitter son musique voix effets"));
    }

    void LaunchChange()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(UpDown));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) UpDown.length * 1000 + 500)); 
        
        Recognition.Function Func = UpOrDown;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "stop rien retour aucun quitter augmenter monter plus diminuer baisser moins"));
    }

    void Choice(string speech)
    {
        if (speech != "quitter" && speech != "stop" && speech != "retour" && speech != "rien" && speech != "aucun")
        {
            switch (speech)
            {
                case "son":
                    barremixer = "Sounds";
                    break;

                case "musique":
                    barremixer = "Music";
                    break;

                case "voix":
                    barremixer = "Voices";
                    break;
                case "effets":
                    barremixer = "Effets";
                    break;
            }
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchChange());
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => OptionMenu.SetActive(false));
            UnityMainThreadDispatcher.Instance().Enqueue(() => MainMenu.SetActive(true));

            UnityMainThreadDispatcher.Instance().Enqueue(() => Main.relaunch = true);
        }
    }

    void UpOrDown(string speech)
    {
        if (speech != "quitter" && speech != "stop" && speech != "retour" && speech != "rien" && speech != "aucun")
        {
            switch (speech)
            {
                case "monter":
                case "plus":
                case "augmenter":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => audioMixer.GetFloat(barremixer, out setlvl));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => audioMixer.SetFloat(barremixer, setlvl + 0.2f));
                    break;

                case "baisser":
                case "moins":
                case "diminuer":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => audioMixer.GetFloat(barremixer, out setlvl));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => audioMixer.SetFloat(barremixer, setlvl - 0.2f));
                    break;
            }
        }

        UnityMainThreadDispatcher.Instance().Enqueue(() => Launch());
    }
}

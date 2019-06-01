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
    
    
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Menu));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Menu.length * 1000 + 500));

        Launch();
    }

    void Launch()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WhatDoUWant));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WhatDoUWant.length * 1000 + 500));

        Recognition.Function Func = Choice;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "quitter son musique voix"));
    }

    void LaunchChange()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(UpDown));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) UpDown.length * 1000 + 500)); 
        
        Recognition.Function Func = UpOrDown;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "augmenter monter plus diminuer baisser moins"));
    }

    void Choice(string speech)
    {
        if (speech != "quitter")
        {
            switch (speech)
            {
                case "son":
                    barremixer = "SoundVol";
                    break;

                case "musique":
                    barremixer = "MusicVol";
                    break;

                case "voix":
                    barremixer = "VoicesVol";
                    break;
            }
            
            LaunchChange();
        }
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => OptionMenu.SetActive(false));
        UnityMainThreadDispatcher.Instance().Enqueue(() => MainMenu.SetActive(true));
    }

    void UpOrDown(string speech)
    {
        switch (speech)
        {
            case "monter":
            case "plus":
            case"augmenter":
                audioMixer.GetFloat(barremixer, out setlvl);
                audioMixer.SetFloat(barremixer, setlvl + 0.1f);
                break;
            
            case "baisser":
            case "moins":
            case "diminuer":
                audioMixer.GetFloat(barremixer, out setlvl);
                audioMixer.SetFloat(barremixer, setlvl + 0.1f);
                break;
        }
        
        Launch();
    }
}

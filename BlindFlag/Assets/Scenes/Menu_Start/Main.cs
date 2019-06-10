using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject MainMenu;

    public AudioClip Hello;
    public AudioClip GoodBye;
    public AudioClip NoSave;
    public static AudioClip NoSave1;
    public static AudioClip GoodBye1;

    private static AudioSource Audio;

    public GameObject Canvas;

    public static bool relaunch;

    private bool start;
    
    // Start is called before the first frame update
    void Start()
    {
        int width = 1920; // or something else
        int height= 1080; // or something else
        bool isFullScreen = true; // should be windowed to run in arbitrary resolution
        int desiredFPS = 60; // or something else
 
        UnityMainThreadDispatcher.Instance().Enqueue(() => Screen.SetResolution (width , height, isFullScreen, desiredFPS));

        start = true;
        relaunch = false;
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Canvas.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Canvas.GetComponent<AudioSource>().Play());

        NoSave1 = NoSave;
        GoodBye1 = GoodBye;
    }

    private void Update()
    {
        if (relaunch) Launch();
        if (start)
        {
            start = false;
            Launch();
        }
    }

    public void Launch()
    {
        Audio = GetComponent<AudioSource>();
        relaunch = false;
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Hello));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Hello.length * 1000 + 500));
        
        Recognition.Function Func = Traitement;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "continuer commencer nouvelle option quitter paramaitre"));
    }

    public static void NewGame()
    {
        if (Save.IsThereASave()) {Save.DeleteSave();}
        Start_CaptainStats.Start();
        Start_ShipStats.Start();
        BlindShip_Stat.SceneLoad = (int) LoadScene.Scene.Navigation;
        UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene((int) LoadScene.Scene.Navigation));
        //UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.UnloadScene((int) LoadScene.Scene.START));
    }

    void Traitement(string input)
    {
        Debug.Log(input);
        switch (input)
        {
            case "continuer":
                if (Save.IsThereASave()) Save.LoadGame();
                else {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(NoSave));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) NoSave.length * 1000 + 500));
                    }
                break;

            case "commencer":
            case "nouvelle":
                NewGame();
                break;

            case "option":
            case "paramaitre":
                UnityMainThreadDispatcher.Instance().Enqueue(() => MainMenu.SetActive(false));
                UnityMainThreadDispatcher.Instance().Enqueue(() => OptionMenu.SetActive(true));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Option.relaunch = true);
                break;

            case "quitter":
                UnityMainThreadDispatcher.Instance().Enqueue(() => Quit());
                break;
        }
    }

    public static void NS()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(NoSave1));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) NoSave1.length * 1000 + 500));
    }

    public static void Quit()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(GoodBye1));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) GoodBye1.length * 1000 + 500));
        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}

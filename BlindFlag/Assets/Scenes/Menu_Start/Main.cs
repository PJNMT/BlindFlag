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

    private AudioSource Audio;
    
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Hello));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Hello.length * 1000 + 500));
        
        Recognition.Function Func = Traitement;
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "continuer commencer option quitter parametre"));
    }

    void NewGame()
    {
        Start_CaptainStats.Start();
        Start_ShipStats.Start();
        BlindShip_Stat.SceneLoad = (int) LoadScene.Scene.Navigation;
        UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene((int) LoadScene.Scene.Navigation));
        UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.UnloadSceneAsync((int) LoadScene.Scene.START));
    }

    void Traitement(string input)
    {
        switch (input)
        {
            case "continuer":
                if (Save.IsThereASave()) Save.LoadGame();
                else
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(NoSave));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) NoSave.length * 1000 + 500));
                }
                break;

            case "commencer":
                NewGame();
                break;

            case "option":
            case "parametre":
                UnityMainThreadDispatcher.Instance().Enqueue(() => MainMenu.SetActive(false));
                UnityMainThreadDispatcher.Instance().Enqueue(() => OptionMenu.SetActive(true));
                break;

            case "quitter":
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(GoodBye));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) GoodBye.length * 1000 + 500));
                QuitOnClick.Quit();
                break;
        }
    }
}

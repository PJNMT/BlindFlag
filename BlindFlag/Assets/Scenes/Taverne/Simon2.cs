using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Simon2 : MonoBehaviour
{
    
    public int[] SimonPad;
    public int[] PlayerPad;
    public AudioClip[] SoundPad;
    private AudioSource _AudioSource;
    private int Len;

    public GameObject other;

    private int mise;
    private bool activated;

    public float time;
    public bool continuer;
    public bool won;

    public Dictionary<int, KeyCode> IntToKey;
    
    public GameObject table1;
    public GameObject table2;
    public GameObject bar;
    public GameObject Sol;


    Event e;
    KeyCode LastKeyPressed;
    bool keydown = false;

    private bool EnterPressed;

    public AudioClip vos_gains;
    public AudioClip combien_miser;
    public AudioClip faut_sameliorer;
    public AudioClip êtes_le_meilleur;

    public AudioClip T1;
    public AudioClip T2;
    public AudioClip S1;
    public AudioClip B1;


    
    // Start is called before the first frame update
    void Start()
    {
        IntToKey = new Dictionary<int, KeyCode>();
        IntToKey.Add(1, KeyCode.UpArrow);
        IntToKey.Add(2, KeyCode.LeftArrow);
        IntToKey.Add(3, KeyCode.DownArrow);
        IntToKey.Add(4, KeyCode.RightArrow);


        won = false;
        continuer = true;
        EnterPressed = false;

        Len = 15;
        time = 1;

        PlayerPad = new int[Len];
        SimonPad = new int[Len];

        _AudioSource = other.GetComponent<AudioSource>();

        mise = 0;

        activated = true;
        
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex((int) LoadScene.Scene.Taverne));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You")
        {
            other.GetComponent<Tavern>().sedeplacer = false;
            table1.GetComponent<AudioSource>().Stop();
            table2.GetComponent<AudioSource>().Stop();
            bar.GetComponent<AudioSource>().Stop();
            Sol.GetComponent<AudioSource>().Stop();
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => _AudioSource.PlayOneShot(combien_miser));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) combien_miser.length * 1000 + 500));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Traitement, "trente cinquante cent vingt"));
        }
    }

    bool SimonGame()
    {
        StartCoroutine(Tuto());

        return won;
    }


    IEnumerator Play()
    {
        int rnd;
        int i = 0;
        while (continuer && i < Len)
        {
            Debug.Log(i);
            
            //Choose ramdomly the key added
            rnd = Random.Range(0, 4);
            SimonPad[i] = rnd;
            Debug.Log(i + "Input :" + rnd);

            //Play each sound from the Simon pad
            int nb = 0;
            while (SimonPad[nb] != 0 && nb < Len)
            {
                //Let time between sounds
                yield return new WaitForSeconds(time);

                // Play the sound of the key
                _AudioSource.PlayOneShot(SoundPad[SimonPad[nb]]);

                nb += 1;
            }


            Debug.Log("End Of SimonPad");
            yield return new WaitForSeconds(1);


            //Player pad
            int j = 0;
            while (SimonPad[j] != 0 && j < Len && continuer)
            {
                yield return new WaitUntil(() => keydown);
                keydown = false;
                Debug.Log("Player :" + LastKeyPressed.ToString());

                if (LastKeyPressed != IntToKey[SimonPad[j]])
                {
                    continuer = false;
                    Debug.Log("Wrong Key....");
                }

                j += 1;
            }

            time -= 0.05f;
            i += 1;
        }

        if (i == Len)
        {
            won = true;
            UnityMainThreadDispatcher.Instance().Enqueue(() => _AudioSource.PlayOneShot(êtes_le_meilleur));
            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Thread.Sleep((int) êtes_le_meilleur.length * 1000 + 500));
            UnityMainThreadDispatcher.Instance().Enqueue(() => _AudioSource.PlayOneShot(vos_gains));

            BlindShip_Stat.AddMoney(_AudioSource, mise);
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => _AudioSource.PlayOneShot(faut_sameliorer));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) faut_sameliorer.length * 1000 + 500));
            BlindShip_Stat.Money -= mise;
            other.GetComponent<Tavern>().sedeplacer = true;
        }

       
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(3000));

        UnityMainThreadDispatcher.Instance().Enqueue(() => table1.GetComponent<AudioSource>().clip = T1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => table1.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => table1.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => table2.GetComponent<AudioSource>().clip = T2);
        UnityMainThreadDispatcher.Instance().Enqueue(() => table2.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => table2.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Sol.GetComponent<AudioSource>().clip = S1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Sol.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Sol.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => bar.GetComponent<AudioSource>().clip = B1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => bar.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => bar.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => other.GetComponent<Tavern>().LaunchTavern());
    }


    //Wait for the player to have test the key's sound
    IEnumerator Tuto()
    {
        Synthesis.synthesis("Appuyer sur les flèches pour connaitre leur sons. Pressez Espace pour commencer le jeu");

        yield return new WaitUntil(() => EnterPressed);

        StartCoroutine(Play());
    }

    void Traitement(string chiffre)
    {
        if (activated)
        {
            Debug.Log(chiffre);
            switch (chiffre)
            {
                case "trente":
                    mise = 30;
                    break;
                case "vingt":
                    mise = 20;
                    break;
                case "cent":
                    mise = 100;
                    break;
                case "cinquante":
                    mise = 50;
                    break;
            }

            UnityMainThreadDispatcher.Instance().Enqueue(() => SimonGame());
            activated = false;
        }
    }


    //Treat the sound of the key pressed
    void Sound(KeyCode keyCode)
    {
        if (keyCode == KeyCode.UpArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[0]);
        }

        if (keyCode == KeyCode.DownArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[2]);
        }

        if (keyCode == KeyCode.LeftArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[1]);
        }

        if (keyCode == KeyCode.RightArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[3]);
        }
    }


    //Treat the event Key pressed
    void OnGUI()
    {
        if (!other.GetComponent<Tavern>().sedeplacer)
        {
            e = Event.current;
            if (e.type.Equals(EventType.KeyDown) && !keydown)
            {
                if (e.keyCode == KeyCode.Space)
                {
                    EnterPressed = true;
                }
                else
                {
                    LastKeyPressed = e.keyCode;
                    Sound(LastKeyPressed);
                    keydown = true;
                }
            }

            if (e.type.Equals(EventType.KeyUp))
                keydown = false;


            Debug.Log("Last Key Pressed - " + e.keyCode.ToString());
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Simon2 : MonoBehaviour
{
    public int[] SimonPad;
    public int[] PlayerPad;
    public AudioClip[] SoundPad;
    public AudioSource _AudioSource;
    public int Len;

    private int mise;
    private bool activated;
    
    public float time;
    public bool continuer;
    public bool won;

    public Dictionary<int, KeyCode> IntToKey;
    public Dictionary<string, int> KeyToInt;
    
    Event e;
    KeyCode LastKeyPressed;
    bool keydown = false;

    private bool EnterPressed;
    
    
    // Start is called before the first frame update
    void Start()
    {
       IntToKey = new Dictionary<int, KeyCode>();
       KeyToInt = new Dictionary<string, int>();
        
       IntToKey.Add(1,KeyCode.UpArrow);
       IntToKey.Add(2,KeyCode.LeftArrow);
       IntToKey.Add(3,KeyCode.DownArrow);
       IntToKey.Add(4,KeyCode.RightArrow);
        
        KeyToInt.Add("UpArrow", 1);
        KeyToInt.Add("LeftArrow",2);
        KeyToInt.Add("DownArrow",3);
        KeyToInt.Add("RightArrow",4);
        
        won = false;
        continuer = true;
        EnterPressed = false;

        Len = 15;
        time = 1;
        
        PlayerPad = new int[Len];
        SimonPad = new int[Len];

        _AudioSource = GetComponent<AudioSource>();

        activated = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You")
        {
            
            //UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis("Combien voulez vous parier à ce jeu ? vinGt, trente cinquante ou cent piaice d'or?"));
            //Thread.Sleep(500);
            //Recognition.start_recognition(Traitement,"trente cinquante cent vinGt", 30); 
            
            Debug.Log("début");

            SimonGame();
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
            rnd = Random.Range(1, 5);
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
        }
    }
    
    
    //Wait for the player to have test the key's sound
    IEnumerator Tuto()
    {
        Synthesis.synthesis("Appuyer sur les flèches pour connaitre leur sons. Pressez Espace pour commencer le jeu");
        
        yield return new WaitUntil( () => EnterPressed);

        StartCoroutine(Play());
    }

    void Traitement(string chiffre)
    {
        
        Debug.Log(chiffre);
        switch (chiffre)
        {
            case "trente":
                mise = 30;
                break;
            case "vinGt":
                mise = 20;
                break;
            case "cent":
                mise = 100;
                break;
            case "cinquante":
                mise = 50;
                break;
        }

        activated = true;
    }

    
    
    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Recognition.stop_recognition();
        }
    }
    

    
    //Treat the sound of the key pressed
    void Sound(KeyCode keyCode)
    {
        if (keyCode== KeyCode.UpArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[1]);
        }
        if (keyCode== KeyCode.DownArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[3]);
        }
        if (keyCode== KeyCode.LeftArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[2]);
        }
        if (keyCode== KeyCode.RightArrow)
        {
            _AudioSource.PlayOneShot(SoundPad[4]);
        }
    }

    
    
    //Treat the event Key pressed
    void OnGUI()
    {
        e = Event.current;
        if(e.type.Equals(EventType.KeyDown) && !keydown)
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
      
        if(e.type.Equals(EventType.KeyUp))
            keydown = false;
        
        
        Debug.Log("Last Key Pressed - " + e.keyCode.ToString());
        
    }
}

   







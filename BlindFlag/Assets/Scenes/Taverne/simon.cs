using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using DefaultNamespace;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class     simon : MonoBehaviour
{
    
    public bool correct;
    
    private AudioSource audio;
    private int mise;
    public AudioClip[] musics;

    private string chemintxt = "morceauxsimon.txt";
    public static string[] notes;

    private int i;
    private Tavern taverne;
    private bool activated;

    private RecordMic recorder;

    private bool launch;
    public Music_Recognition _musicRecognition;
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        _musicRecognition = gameObject.AddComponent<Music_Recognition>();
        launch = false;
        Debug.Log("chanter");
        recorder = gameObject.AddComponent<RecordMic>();
        notes = new string[(int)musics[0].length];
        
        /* _musicRecognition = gameObject.AddComponent<Music_Recognition>();
         _musicRecognition.Is_right(_musicRecognition.AnalyzeSound(), "La_", 1.5f);
         taverne = gameObject.GetComponent<taverne>();
         activated = false;
 
         _musicRecognition = gameObject.AddComponent<Music_Recognition>();
 
         audio = GetComponent<AudioSource>();
         audio.clip = musics[0];
 
         i = 0;*/
    }

    // Update is called once per frame

    private void Update()
    {
        if (activated)
        {
            Recognition.stop_recognition();
            activated = false;
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You" && !launch)
        {
            launch = true;
            audio = this.GetComponent<AudioSource>();

            audio.clip = musics[0];
            i = 1;

            correct = true;
            //UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis("Combien voulez vous parier à ce jeu ? vinGt, trente cinquante ou cent ?"));
            //Recognition.start_recognition(Traitement,"trente cinquante cent vinGt", 30); 
            
            Debug.Log("début");

            UnityMainThreadDispatcher.Instance().Enqueue(() => StartCoroutine(Play()));
             
            
        }
    }

    
    void Traitement(string chiffre)
    {
        activated = true;
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
    }


    
    
    IEnumerator Play()
    {
        i = 1;
        using (StreamReader lirenote = new StreamReader(chemintxt))
        {
            
            _musicRecognition.start_recognition();
            
            while (i <= (int) audio.clip.length && correct)
            {
                notes[i] = (char) lirenote.Read() + "" + (char) lirenote.Read() + (char) lirenote.Read();
                    Debug.Log("on play");
                this.audio.Play();

                //Wait for i seconds
                yield return new WaitForSeconds(i);
                audio.Stop();

                UnityMainThreadDispatcher.Instance().Enqueue(() => recorder.Recorder(i));
                yield return new WaitForSeconds(i);
                Microphone.End(Microphone.devices[0]);
                UnityMainThreadDispatcher.Instance().Enqueue(() => StartCoroutine(recorder.PlayBack(i)));
                yield return new WaitForSeconds(i);


                yield return new WaitForSeconds(i);
                Debug.Log("waintin");


                i += 1;
            }

            Debug.Log(i);
            Debug.Log(audio.clip.length);

            if (!correct)
            {
                Synthesis.synthesis("Faudrait sonGer à vous améliorer capitaine.");
                BlindShip_Stat.Money -= mise;

            }
            else
            {
                Synthesis.synthesis("bien joué capitaine !!");
                BlindShip_Stat.AddMoney(audio, mise * 3);
            }
        }

    }
    
    

}

internal class gameObject
{
}

    


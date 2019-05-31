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
using UnityEngine.Audio;
using Debug = UnityEngine.Debug;

public class     simon : MonoBehaviour
{
    
    private bool correct;
    private static Music_Recognition _musicRecognition;

    private AudioSource audio;
    private int mise;
    public AudioClip[] musics;

    private string chemintxt = "morceauxsimon.txt";
    private string[] notes;

    private int i;
    private taverne taverne;
    private bool activated;

    private Record recorder;
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("chanter");
        recorder = gameObject.AddComponent<Record>();
        
        recorder.Recorder(2);
        
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

    private void Reco_Correct()
    {
        for (int j =0; j<i; j++)
        {
            correct = _musicRecognition.Is_right(_musicRecognition.AnalyzeSound(), notes[j], 1.5f);    //Vérifie que chaque note est juste
                        
            //Si une note est mauvaise le jeu s'arrête et le capitaine perd sa mise.
            if (!correct)
            {
                Synthesis.synthesis("Faudrait sonGer à vous améliorer capitaine.");
                BlindShip_Stat.Money -= mise;
                break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        /*if (other.gameObject.name == "You")
        {

            StartCoroutine(Play());

            //correct = _musicRecognition.Is_right(_musicRecognition.AnalyzeSound(), "La_", 1.5f);
            if (correct)
            {
                Debug.Log("ok");
            }
        }


        /*if (other.gameObject.name == "You")
        {
            
            audio = this.GetComponent<AudioSource>();

            audio.clip = musics[0];
            i = 1;

            correct = true;
            //UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis("Combien voulez vous parier à ce jeu ? vinGt, trente cinquante ou cent ?"));
            //Recognition.start_recognition(Traitement,"trente cinquante cent vinGt", 30); 
            
            Debug.Log("début");

            StartCoroutine(Play(i));
             
            
        }*/
    }

    /*void Jeu()
    {
        int i = 0;
        
        using (StreamReader file2 = new StreamReader(chemintxt))
        {
            string a = file2.ReadLine();
            Debug.Log(a);
            notes = a.Split(' '); // ajoute les notes a matcher
            
                while (i < (int)audio.clip.length)
                {
                   
                    //joue l'audio
                    Play(audio,i);
                    
                    /*for (int j =0; j<i; j++)
                    {
                        correct = _musicRecognition.Is_right(_musicRecognition.AnalyzeSound(), notes[j], 1.5f);    //Vérifie que chaque note est juste
                        
                        //Si une note est mauvaise le jeu s'arrête et le capitaine perd sa mise.
                        if (!correct)
                        {
                            Synthesis.synthesis("Faudrait sonGer à vous améliorer capitaine.");
                            BlindShip_Stat.Money -= mise;
                            break;
                        }
                    }

                    i += 1;
                }
            }
        
        if (i == (int)audio.clip.length)
        {
            Synthesis.synthesis("Vous êtes vraiment le meilleur à ce jeu capitaine !");   //si le joueur won, il remporte 4 fois sa mise
            musics.Take(0);
            musics.Append(audio.clip);
            
            BlindShip_Stat.Money += mise*4;
        }
    
}*/

   void Play2(int i)     //joue le morceau un certain temps puis attend 1sec
    {
        Debug.Log("on play");
        UnityMainThreadDispatcher.Instance().Enqueue(() => this.audio.Play());
        new WaitForSeconds(5);
        Debug.Log("waitin");
        UnityMainThreadDispatcher.Instance().Enqueue(() =>this.audio.Stop());
        Debug.Log("stop");
        //Thread.Sleep(1000);
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
        
        while (i <= (int) audio.clip.length)
        {
            Debug.Log("on play");
            this.audio.Play();

            //Wait for i seconds
            yield return new WaitForSeconds(i);
            yield return new WaitUntil(() => !correct);
            Debug.Log("waintin");

            this.audio.Stop();
            Debug.Log("stop");
            i += 1;
        }
        Debug.Log(i);
        Debug.Log(audio.clip.length);

       
    }

}

    


using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using Debug = UnityEngine.Debug;

public class     simon : MonoBehaviour
{
    
    private bool correct;
    //private static Music_Recognition _musicRecognition;

    private AudioSource audio;
    private int mise;
    public AudioClip[] musics;

    private string chemintxt = "morceauxsimon.txt";
    private string[] notes;

    private int i;
    private taverne taverne;
    private bool activated;
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        //Recognition.stop_recognition();
        taverne = gameObject.GetComponent<taverne>();
        activated = false;


        //Jeu();

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
        
        if (other.gameObject.name == "You")
        {
            
            audio = this.GetComponent<AudioSource>();

            audio.clip = musics[0];
            i = 1;

            correct = true;
            //UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis("Combien voulez vous parier à ce jeu ? vinGt, trente cinquante ou cent ?"));
            //Recognition.start_recognition(Traitement,"trente cinquante cent vinGt", 30); 
            
            Debug.Log("début");

            StartCoroutine(Play(i));
            while (i<3)
            {
                Play(i);
                i += 1;
            }
            
        }
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
    
    IEnumerator Play(int i)
    {
        Debug.Log("on play");
        UnityMainThreadDispatcher.Instance().Enqueue(() => this.audio.Play());

        //Wait for 2 seconds
        yield return new WaitForSeconds(i);
        Debug.Log("waintin");

        UnityMainThreadDispatcher.Instance().Enqueue(() =>this.audio.Stop());
        Debug.Log("stop");
    }

}

    


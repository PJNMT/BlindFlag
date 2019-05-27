using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;

public class     simon : MonoBehaviour
{
    
    private bool correct;
    private static Music_Recognition _musicRecognition = new Music_Recognition();

    private AudioSource audio;
    private int mise;
    public AudioClip[] musics = new AudioClip[1];

    private string chemintxt = "morceauxsimon.txt";
    private List<string> notes;
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        //Recognition.stop_recognition();

        audio = gameObject.GetComponent<AudioSource>();

        audio.clip = musics[0];

        correct = true;
        Synthesis.synthesis("Combien voulez vous parier à ce jeu ? vinGt, trente cinquante ou cent ?");
        Recognition.start_recognition(Traitement,"trente cinquante cent vinGt", 10);

        Jeu();
        
    }

    // Update is called once per frame
    void Jeu()
    {
        int i = 0;
        
        using (StreamReader file2 = new StreamReader(chemintxt))
            {
                while (i < (int)audio.clip.length)
                {
                    notes.Add(file2.Read()+file2.Read()+file2.Read()+"");      //ajoute la nouvelle note
                    
                    //joue l'audio
                    Play(audio,i);
                    
                    foreach (string note in notes)
                    {
                        correct = _musicRecognition.Is_right(_musicRecognition.AnalyzeSound(), note, 1.5f);    //Vérifie que chaque note est juste
                        
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
    
}

    void Play(AudioSource audio, int i)     //joue le morceau un certain temps puis attend 1sec
    {
        audio.Play();
        Thread.Sleep(1000*i);
        audio.Stop();
        Thread.Sleep(1000);
    }


    void Traitement(string chiffre)
    {
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

}

    


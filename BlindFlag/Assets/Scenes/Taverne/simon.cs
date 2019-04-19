using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEngine;

public class simon : MonoBehaviour
{
    
    private bool correct;
    private Morceau _morceau;
    private static Music_Recognition _musicRecognition = new Music_Recognition();

    private AudioSource audio;
    private int mise;
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        
        audio = new AudioSource();

        using (StreamReader file = new StreamReader("morceauxsimon.txt"))
        {

            for (int j = 0; j < _morceau.notes.Count; j++)
            {
                file.ReadLine();
            }

            _morceau = new Morceau(file.Read());
        }

        correct = true;
        Synthesis.synthesis("Combien voulez vous parrier à ce jeu ? vinGt, trente cinquante ou cent ?");
        Recognition.start_recognition(10,"trente cinquante cent vinGt", Traitement);

        Jeu();
        
    }

    // Update is called once per frame
    void Jeu()
    {
        int i = 0;
        
        using (StreamReader file2 = new StreamReader(_morceau.chemintxt))
            {
                while (i < _morceau.GetTime())
                {
                    _morceau.notes.Add(file2.Read()+file2.Read()+file2.Read()+"");      //ajoute la nouvelle note
                    
                    //joue l'audio
                    Play(audio,i);
                    
                    foreach (string note in _morceau.notes)
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
        
        if (i == _morceau.GetTime())
        {
            Synthesis.synthesis("Vous êtes vraiment le meilleur à ce jeu capitaine !");   //si le joueur won, il remporte 4 fois sa mise
            _morceau.Debloc();
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

public class Morceau
{
    private int nb;
    private int time;
    private string file = "morceauxsimon.txt";      //contient les 
    private string chemin;
    private bool debloq;
    
    public List<string> notes;
    public string chemintxt;


        public Morceau(int nb)
        {
            debloq = false;
            using (StreamReader file = new StreamReader(this.file))       // crée l'objet morceau à partir de son numéro
            {
                for (int i = 0; i < nb; i++)                      //
                {
                    Console.ReadLine();
                }
                string[] lecture = file.ReadLine().Split('*');
                time = int.Parse(lecture[1]);
                nb = int.Parse(lecture[0]);
                chemin = lecture[2];
                chemintxt = lecture[3];
                notes = new List<string>();

            }
        }

        public string GetChemin()
        {
            return chemin;
        }

        public int GetTime()
        {
            return time;
        }
            
        public void Debloc()
        {
            debloq = true;
            using (StreamWriter écrire = new StreamWriter(file))
            {
                
                écrire.WriteLine("Debloc");
            }
        }
                
    } 
    


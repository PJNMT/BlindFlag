using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class tresor : MonoBehaviour
{
    private static List<int> _path= new List<int>();
    public Enigma _enigma;
    private static string _enigmefile = "enigme.txt";
    private int or;

    public bool continuer = false;
    public bool rightanswer = false;
    
    public float x;
    public float z;
    
    // Place le trésor aléatoirement sur la map
    void Start()
    {
         x = Random.Range(2f, 98f);
         z = Random.Range(30f, 98f);


        transform.position = new Vector3(x, 1f, z);
        
        //crée l'objet enigme choisit et _enigma prends sa valeur
        Generateenigme();
        Debug.Log(_enigma._enigme);
      
    }

   void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "You" || other.name == "speakenigma" || other.name == "Sphere")
        {
            FindObjectOfType<cubecontroller>().sedeplacer = false;
            Synthesis.synthesis("Vous avez trouvez le trésor capitaine ! Mais saurez vous répondre à cette énigme ?");
            Thread.Sleep(6000);
            
            //Reconnaissance de la réponse du joueur
            Recognition.Function Traitement = Optionstreatement;
            
            //Dis l'énigme au joueur
            SpeakEnigma(_enigma);
            string lecturetraitement;
            
            bool continuer = true;
            
            Debug.Log("start");
            Recognition.start_recognition(Traitement, "chat indice aide répète répéter",0);

            while (continuer)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Recognition.Function Traitement2 = Answertraitement;
                    Recognition.stop_recognition();
                    Recognition.start_recognition(Traitement2);  //Reconnait si une réponse est attendue
                    Thread.Sleep(10000);
                    
                    Recognition.stop_recognition();
                    Recognition.start_recognition(Traitement, "chat indice aide répète répéter",0);
                    
                }
            }
            
            
            _path.Add(_enigma._number);
            
            //récupération de l'issue de la réponse
            if (rightanswer)
            {
              
                Synthesis.synthesis("Vous avez gagné " + or + "pièces d'or");
                Thread.Sleep(3000);
                //BlindShip_Stat.AddMoney(new AudioSource(), or);
            }
        }

    }
    
    
    void Generateenigme()
    {
        using (StreamReader read = new StreamReader(_enigmefile))
        {

            for (int i = 0; i < _path.Count; i++)
            {
               read.ReadLine();
            }
            _enigma = gameObject.AddComponent<Enigma>();

            string lecture = read.ReadLine();
            string[] division = lecture.Split(':');

            _enigma._enigme = division[0];
            _enigma._enigme = division[1];
            _enigma._indice = division[2];
            _enigma._answer = division[3];
            or = int.Parse(division[4]);

        }
    
    }

    static void SpeakEnigma(Enigma enigma)
    {
        //Recognition.stop_recognition();
        Synthesis.synthesis(enigma._enigme);
        Thread.Sleep(10000);
    }

    static void SpeakIndice(Enigma enigma)
    {
        //Recognition.stop_recognition();
        Synthesis.synthesis(enigma._indice);
        Thread.Sleep(2000);
    }
    
    void Optionstreatement(string input)
    {
       switch (input)
        {
            case "répète":
            case "répéter":
            {
                tresor.SpeakEnigma(_enigma);
                continuer = true;
                break;
            }

            case "indice":
            case "aide":
            {
               tresor.SpeakIndice(_enigma);
                continuer = true;
                break;
            }

            case "chat":
            {
               continuer = false;
               break;
            }
        }            
        
    }

    void Answertraitement(string input)
    {
        if (input == _enigma._answer)
        {
            continuer = false;
            rightanswer = true;

        }
    }
    
    public float[] Getposition()
    {
        return new[] {x, z};
    }

    private void Lauch_reco(Recognition.Function treatement,string input, int time)
    {
        Recognition.start_recognition(treatement,input,time);
    }

}

public  class Enigma : MonoBehaviour
{
    public int _number;
    public string _enigme;
    public string _indice;
    public string _answer;

    public Enigma(int number, string enigme, string indice, string answer)
    {
        _number = number;
        _enigme = enigme;
        _indice = indice;
        _answer = answer;

    }
    
    
}
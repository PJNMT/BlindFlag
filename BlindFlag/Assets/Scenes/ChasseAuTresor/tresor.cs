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
    
    private float x;
    private float z;
    
    // Place le trésor aléatoirement sur la map
    void Start()
    {
         x = Random.Range(-10.0f, 10.0f);
         z = Random.Range(-10.0f, 10.0f);


        transform.Translate(x, 1f, z);
        
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
            Recognition.Function Traitement = Answertreatement;
            
            //Dis l'énigme au joueur
            Debug.Log("speakenigma");
            SpeakEnigma(_enigma);
            string lecturetraitement;
            
            bool continuer = false;
            
            do
            {
                Recognition.start_recognition(Traitement);  //Reconnait tant qu'une réponse est attendue
             
            } while (continuer);

            _path.Add(_enigma._number);
            
            //récupération de l'issue de la réponse
            if (rightanswer)
            {
              
                Synthesis.synthesis("Vous avez gagné " + or + "pièces d'or");
                Thread.Sleep(3000);
                BlindShip_Stat.Money += or;
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
    
    void Answertreatement(string reponse)
    {
        string[] decoupe = reponse.Split(' ');
        
        if (reponse == _enigma._answer)
        {
            rightanswer = true;
            return;

        }
        else
        {
            foreach (string mot in decoupe)
            {

                if (mot == "répète")
                {
                    tresor.SpeakEnigma(_enigma);
                    break;
                }

                if (mot == "indice")
                {
                    tresor.SpeakIndice(_enigma);
                    break;
                }

                if (reponse == "chat")
                {
                    continuer = false;
                }


            }
        }
    }
    
    public float[] Getposition()
    {
        return new[] {x, z};
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
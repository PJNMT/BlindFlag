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

    public bool continuer;
    public bool recooptions;
    public bool rightanswer;
    public static bool indice;
    
    
    public float x;
    public float z;
    
    // Place le trésor aléatoirement sur la map
    void Start()
    {
         x = Random.Range(2f, 98f);
         z = Random.Range(30f, 98f);

          transform.position = new Vector3(0,0,20);
        //transform.position = new Vector3(x, 1f, z);
        
        //crée l'objet enigme choisit et _enigma prends sa valeur
        Generateenigme();
        Debug.Log(_enigma._enigme);

        continuer = true;
        recooptions = true;
        rightanswer = false;
        indice = false;

    }

   void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "You" || other.name == "Sphere")
        {
            FindObjectOfType<cubecontroller>().sedeplacer = false;
            Synthesis.synthesis("Vous avez trouvez le traisor capitaine ! Mais saurez vous raipondre à cette ainigme ?");
            Thread.Sleep(6000);
            
            //Dis l'énigme au joueur
            Synthesis.synthesis(_enigma._enigme);
            Thread.Sleep(10000);
            
            
            //Ajoute l'énigme comme déjà jouée
           _path.Add(_enigma._number); 
            
            //Commence LA coroutine de reco vocale
            continuer = true;
            StartCoroutine(Reco());

        }

    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            recooptions = false;
        }

        if (rightanswer)
        {
            Recognition.stop_recognition();
            Debug.Log("fin de la reco");
            BlindShip_Stat.Money += or;
            Synthesis.synthesis("Vous avez gagnai capitaine !");
        }
    }

    IEnumerator Reco()
    {
        while (continuer)
        {
            Debug.Log("Waiting for options answer....");
            Recognition.Function Traitement = Optionstreatement;
            Recognition.start_recognition(Traitement, "chat indice aide raipaite raipaiter raientendre ainigme", 0);

            yield return new WaitWhile(() => recooptions);

            Debug.Log("Waitin for answer...");
            Recognition.Function Traitement2 = Answertraitement;
            Recognition.start_recognition(Traitement2);

            Debug.Log("Donne ta raiponse Wolila !");
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
        Synthesis.synthesis(enigma._enigme);
        Thread.Sleep(10000);
    }

    static void SpeakIndice(Enigma enigma)
    {
        if (!indice)
        {
            Synthesis.synthesis(enigma._indice);
            Thread.Sleep(2000);
        }
        
    }
    
    void Optionstreatement(string input)
    {
        Debug.Log(input);
       switch (input)
        {
            case "raipaite":
            case "raipaiter":
            case "raientendre":
            case "ainigme":
            {
                Synthesis.synthesis(_enigma._enigme);
                Thread.Sleep(10000);
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
                Synthesis.synthesis("Vous abandonnez si tôt capitaine ? Dommage...");
                rightanswer = false;
                continuer = false;
               break;
            }
        }            
        
    }

    void Answertraitement(string input)
    {
        Debug.Log(input);
        if (input == _enigma._answer)
        {
            continuer = false;
            rightanswer = true;

        }
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
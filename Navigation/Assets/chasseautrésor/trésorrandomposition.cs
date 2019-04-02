using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class trésor : MonoBehaviour
{
    private int[] _path;
    private static Enigma[] _nodes;
    private Enigma _enigma;
    private string _enigmefile;
    
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);


        transform.Translate(x, 1f, z);
        
        
        _nodes = new Enigma[100];
        using (StreamReader lire = new StreamReader(_enigmefile))
        {
            string a = lire.ReadLine();
            while (a != "end")
            {
                string[] attributs = a.Split(':');
                string[] L = new string[2];
                L[0] = attributs[2];
                L[1] = attributs[3];
                
                
                _nodes[int.Parse(attributs[0])] =  new Enigma(int.Parse(attributs[0]), attributs[1], L,attributs[4]);

                a = lire.ReadLine();

            }
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnTriggerEnter(gameObject.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "captain")
        {
            int a = Search();
            
            if (Generateenigme(a))
            {
                _path[_path.Length] = a;
            }

           
        }

        
    }
    
    private int Search()
    {
        using (StreamReader lire = new StreamReader("file"))
        {
            float nb = Random.Range(0.0f, 100f);

            foreach (int occu in _path)
            {
                if ((int)nb == occu)
                {
                    Search();
                }
            }

            return (int) nb;

        }
    }

    private bool Generateenigme(int nb)
    {
        Enigma thisenigma = _nodes[nb];

        thisenigma._enigme;            //Lire 

        if (whatplayersays == "repeat")
        {
            thisenigma._enigme;
        }

        if (whatplayersays == "indice")
        {
            thisenigma._indice[0];
        }
        
        Thread.Sleep(500);
        thisenigma._answer;
    }

}

public class Enigma : MonoBehaviour
{
    private int _number;
    public string _enigme;
    public string[] _indice;
    public string _answer;

    public Enigma(int number, string enigme, string[] indice, string answer)
    {
        _number = number;
        _enigme = enigme;
        _indice = indice;
        _answer = answer;

    }

}

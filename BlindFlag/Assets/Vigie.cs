using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vigie : MonoBehaviour
{
    public Dictionary<GameObject, string> ObjetsVus;
    public List<string> Tags;
    private GameObject player;

    public static string speech;
    public static string[] Dico_1;
    public static string[] Dico_2;
    
    
    
    public enum direction
    {
        Derriere = 0,
        ATribord = 1,
        ABabord = 3,
        DroitDevant = 2
    }
    
    
    
    private void Start()
    {
        speech = "";
        Dico_1 = new[] {"vigie", "baba"};
        Dico_2 = new[] {"Ile", "Ennemy", "Vois", "Bateau"}; 
        
        ObjetsVus = new Dictionary<GameObject, string>();
        Tags = new List<string>(){"Ennemy","Ile","Visible","Port","Ile au trésor"};

       /* 
        Recognition.Function IlVoit = JeVois;
        
        Recognition.start_recognition(IlVoit, "vigie baba Ile Ennemy Vois Bateau"); // TODO il ne faut pas executer cette ligne lorsqu'on est en battaile navale !!!

        
        foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
        {
            if (o.tag == "player" || o.name == "Ship")
            {
                player = o.gameObject;
                break;
            }
                        
        } */
    }


    private void OnTriggerEnter(Collider other)
    {
        /*if(!Tags.Contains(other.tag)) return;*/

        if (Tags.Contains(other.tag))
        {
            Synthesis.synthesis(other.tag + " en vue " + Direction(other.gameObject));
            Debug.Log(other.tag + " en vue " + Direction(other.gameObject));
            ObjetsVus.Add(other.gameObject, other.tag);
        }


    }

    public direction Direction(GameObject TwT)
    {
        double ang = (GetComponentInParent<Transform>().eulerAngles.y);
        while (ang < -180){ang += 360;}
        while (ang > 180){ang -= 360;}

        ang =  ((ang / 180)*Math.PI);
        
        double ddr = Distance(TwT, ang - Math.PI/2);
        double ddv = Distance(TwT, ang + Math.PI / 2);
        double dbb = Distance(TwT, ang + Math.PI);
        double dtb = Distance(TwT, ang);

        double min = Math.Min(Math.Min(ddr, ddv), Math.Min(dbb, dtb));
        
        if (min == dbb)
            return direction.ABabord;
        if (min == dtb)
            return direction.ATribord;
        if (min == ddv)
            return direction.Derriere;
        return direction.DroitDevant;
    }
     
    private double Distance(GameObject oO, double ang,int x = 100)
    {
        return Math.Sqrt(Math.Pow((oO.transform.position.x - GetComponentInParent<Transform>().position.x - x*Math.Sin(ang)), 2)+
                         Math.Pow((oO.transform.position.z - GetComponentInParent<Transform>().position.z - x*Math.Cos(ang)), 2));
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (ObjetsVus.ContainsKey(other.gameObject))
        {
            //J'ai perdu 'other' de vu
            //Synthesis.synthesis("J'ai perdu " + other.name + " de vue");
            Debug.Log("perdu " + other.tag);
            
            ObjetsVus.Remove(other.gameObject);
        } 
    }

   /* void JeVois(string word)
    {
        speech = speech + word + " ";
        string[] words = speech.Split(' ');
        
        Debug.Log(speech);

        if (words.Length > 2)
        {
            if (Dico_1.Contains(words[0]) && Dico_2.Contains(words[1]))
            {

                Synthesis.synthesis("oui Cap'tain"); 
                
                switch (words[1])
                {
                    case "Vois":
                        foreach (KeyValuePair<GameObject,string> pair in ObjetsVus)
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(pair.Value + " en vue " + Direction(pair.Key.gameObject)));

                        }
                        break;
                    
                    case "Ennemy":
                    case "Bateau":

                        foreach (KeyValuePair<GameObject,string> objetsVu in ObjetsVus)
                        {
                            if (objetsVu.Value == "Ennemy")
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(objetsVu.Value + " en vue " + Direction(objetsVu.Key.gameObject)));

                            }
                        }
                        break;

                    case "Ile":
                        Debug.Log("ok captain");
                        foreach (KeyValuePair<GameObject,string> objetsVu in ObjetsVus)
                        {
                            if ((objetsVu.Value == "Port" || objetsVu.Value == "Ile au trésor"))
                            {
                                Debug.Log("ok captain");
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(objetsVu.Value + " en vue " + Direction(objetsVu.Key.gameObject)));

                            }
                        }
                        
                        break;
                }
            }

            speech = "";
        }
        
        else if (!Dico_1.Contains(words[0])) speech = "";
    
        
    }*/
}


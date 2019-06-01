using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vigie : MonoBehaviour
{
    public AudioClip Babord; //Vigie
    public AudioClip Tribord;
    public AudioClip Back;
    public AudioClip Corsaire;
    public AudioClip Pirates;
    public AudioClip Forward;
    public AudioClip Ship;
    public AudioClip YesCaptain;
    public AudioClip IsleInView;
    public AudioClip Galion;
    public AudioClip Military;
    public AudioClip EnVue;
    
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

    public AudioClip DirectionToAudio(direction D)
    {
        switch (D)
        {
            case direction.Derriere:
                return Back;
            case direction.ABabord:
                return Babord;
            case direction.ATribord:
                return Tribord;
            default:
                return Forward;
        }
    }

    public AudioClip TagToAudio(string tag)
    {
        switch (tag)
        {
            case "Ile":
            case "Ile au trésor":
            case "Port":
                return IsleInView;
            default:
                return Ship;
        }
    }
    
    
    private void Start()
    {
        speech = "";
        Dico_1 = new[] {"vigie", "baba"};
        Dico_2 = new[] {"Ile", "Ennemy", "Vois", "Bateau"}; 
        
        ObjetsVus = new Dictionary<GameObject, string>();
        Tags = new List<string>(){"Ennemy","Ile","Visible","Port","Ile au trésor"};
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Tags.Contains(other.tag))
        {
            //UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(other.tag + " en vue " + Direction(other.gameObject)));
            Coco_Vigie.Audio.PlayOneShot(TagToAudio(other.tag));
            Thread.Sleep((int) (TagToAudio(other.tag).length * 1000 + 500));
            Coco_Vigie.Audio.PlayOneShot(DirectionToAudio(Direction(other.gameObject)));
            Thread.Sleep((int) (DirectionToAudio(Direction(other.gameObject)).length * 1000 + 500));
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
            Debug.Log("perdu " + other.tag);
            
            ObjetsVus.Remove(other.gameObject);
        } 
    }
}


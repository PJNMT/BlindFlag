using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatClairvoyant : MonoBehaviour
{
    private static float Clairvoyant_HP;
    
    public AudioClip FinJeu;
    public AudioClip Mort1;
    public AudioClip Mort2;
    private AudioSource Audio;

    public static int nb_death=0; 
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Clairvoyant_HP = 5500;
        transform.position = new Vector3(0,0,0);
    }

    void Death()
    {
        if (nb_death == 3)
        {
            Audio.PlayOneShot(FinJeu);
            Thread.Sleep(2000);
            LoadScene.Load(LoadScene.Scene.Navigation, LoadScene.Scene.END);
        }
        else
        {
            if (nb_death == 1)
            {
                Audio.PlayOneShot(Mort1);
            }
            else
            {
                Audio.PlayOneShot(Mort2);
            }
            Thread.Sleep(2000);
            float ClaiHP = Clairvoyant_HP;
            Clairvoyant_HP = ClaiHP * 0.9f;
            SceneManager.LoadScene("Combat");
        }
    }

    void Update()
    {
        if (Clairvoyant_HP<=0)
        {
            nb_death += 1;
            Death();
        }
    }

}

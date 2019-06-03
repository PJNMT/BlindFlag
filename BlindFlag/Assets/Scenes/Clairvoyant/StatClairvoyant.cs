using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StatClairvoyant : MonoBehaviour
{
    private static float Clairvoyant_HP;
    
    public AudioClip FinJeu;
    public AudioClip Mort1;
    public AudioClip Mort2;
    private AudioSource Audio;

    private static int nb_death;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Clairvoyant_HP = 5500;
        nb_death = 0;
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
            float ClaiHP = Clairvoyant_HP;
            Clairvoyant_HP = ClaiHP * 0.9f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

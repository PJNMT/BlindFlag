using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip boom1;
    
    void Start()
    {
        transform.GetComponent<AudioSource>().clip = boom1;
        transform.GetComponent<AudioSource>().PlayDelayed(transform.GetComponent<AudioSource>().clip.length); 
        Debug.Log("sound");
    }
}

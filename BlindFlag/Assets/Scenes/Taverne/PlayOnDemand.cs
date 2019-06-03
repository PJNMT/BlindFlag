using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayOnDemand : MonoBehaviour
{
    public bool Ondemand;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        Ondemand = false;
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ondemand)
        {
            Ondemand = false;
            UnityMainThreadDispatcher.Instance().Enqueue(() => AudioSource.PlayOneShot(AudioSource.clip));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) AudioSource.clip.length * 1000 + 500));
        }
    }
}

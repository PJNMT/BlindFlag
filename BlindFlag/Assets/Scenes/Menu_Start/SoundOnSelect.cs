using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoundOnSelect : MonoBehaviour
{
    private AudioSource Audio;
    public AudioClip SoundHover;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void HoverSound()
    {
        Audio.PlayOneShot(SoundHover);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Audio.clip.length * 1000 + 200));
    }
}

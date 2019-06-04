﻿using System.Collections;
using System.Collections.Generic;
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
    }
}

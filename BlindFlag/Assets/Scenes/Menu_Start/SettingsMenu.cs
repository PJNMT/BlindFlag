using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public string audioname;

    public void SetLevel(float setlvl)
    {
        audioMixer.SetFloat(audioname, Mathf.Log10(setlvl)*20);
    }
    
}

using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Record : MonoBehaviour
    {
        //A boolean that flags whether there's a connected microphone  
        private bool micConnected = false;

        //The maximum and minimum available recording frequencies  
        private int minFreq;
        private int maxFreq;

        //A handle to the attached AudioSource  
        private AudioSource goAudioSource;

        //Use this for initialization  
        void Start()
        {
            //Check if there is at least one microphone connected  
            if (Microphone.devices.Length <= 0)
            {
                //Throw a warning message at the console if there isn't  
                Debug.LogWarning("Microphone not connected!");
                return;
            }
            else //At least one microphone is present  
            {
                //Set 'micConnected' to true  
                micConnected = true;

                //Get the default microphone recording capabilities  
                Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

                //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...  
                if (minFreq == 0 && maxFreq == 0)
                {
                    //...meaning 44100 Hz can be used as the recording sampling rate  
                    maxFreq = 44100;
                }

                //Get the attached AudioSource component  
                goAudioSource = this.GetComponent<AudioSource>();
            }
        }
        
       public void Recorder(int time)
        {  
            //If there is a microphone  
            if(micConnected)  
            {  
                //If the audio from any microphone isn't being captured  
                if(!Microphone.IsRecording(null))  
                {  
                    Debug.Log("On recordin");
                    //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource  
                    goAudioSource.clip = Microphone.Start(null, true, time, maxFreq);  
                    
                }  
                else //Recording is in progress  
                {  
                    
                        Microphone.End(null); //Stop the audio recording  
                        goAudioSource.Play(); //Playback the recorded audio  
                    
  
                    Debug.Log("Recording in progress...");  
                }  
            }  
            else // No microphone  
            {  
                //Print a red "Microphone not connected!" message at the center of the screen  
                Debug.Log("Microphone not connected!");  
            }  
  
        }



    }
}
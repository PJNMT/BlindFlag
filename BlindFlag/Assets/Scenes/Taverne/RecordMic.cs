using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace DefaultNamespace
{
    public class RecordMic : MonoBehaviour
    {
        //A boolean that flags whether there's a connected microphone  
        private bool micConnected = false;

        //The maximum and minimum available recording frequencies  
        private int minFreq;
        private int maxFreq;

        //A handle to the attached AudioSource  
        private AudioSource goAudioSource;
        private simon _simon;

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
                _simon = gameObject.GetComponent<simon>();
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
                    goAudioSource.clip = Microphone.Start(Microphone.devices[0], true, time, maxFreq);  
                    
                }  
                else //Recording is in progress  
                {  
                    
                        Microphone.End(null); //Stop the audio recording  
                        
  
                    Debug.Log("Recording in progress...");  
                }  
            }  
            else // No microphone  
            {  
                //Print a red "Microphone not connected!" message at the center of the screen  
                Debug.Log("Microphone not connected!");  
            }  
  
        }
       
       public IEnumerator PlayBack(int time)
       {
           Debug.Log("yo");
           int i = 1;
           float note_user;
           bool IsCorrect = true;

           goAudioSource.volume = 1f;
           goAudioSource.loop = true;
           goAudioSource.Play();
           
           while (i<=time && IsCorrect)
           {
               note_user = 3.0f;
            
               UnityMainThreadDispatcher.Instance().Enqueue(() => note_user = _simon._musicRecognition.AnalyzeSound());
               
               yield return new WaitForSeconds(4);
               
               goAudioSource.Stop();

               IsCorrect =  _simon._musicRecognition.Is_right(note_user, simon.notes[i], 3f);
               

               i += 1;
           }

           _simon.correct = IsCorrect;
       }



    }
}
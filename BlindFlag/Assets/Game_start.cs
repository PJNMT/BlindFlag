using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* if( testé si deja partie existante )
           else
         
         */
        
        
        Synthesis.synthesis("Bonjour capitaine, voulez vous continuez votre aventure ou bien commencer une nouvelle légende");
        
        Recognition.start_recognition(20,"nouvelle continuer commencer",react);
    }


    void react(string s)
    {
        switch (s)
        {
                case "commencer" :
                case "nouvelle" :

                    // load les script de demarrage
                    SceneManager.LoadScene("Port");
                    SceneManager.UnloadSceneAsync("START");
                    break;
                case "continuer" :


                    SceneManager.LoadScene("Port");
                    SceneManager.UnloadSceneAsync("START");
                    
                    break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

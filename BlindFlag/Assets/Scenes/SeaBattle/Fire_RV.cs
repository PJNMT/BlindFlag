using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fire_RV : MonoBehaviour
{
    public static string speech;
    public static string[] Dico_1;
    public static string[] Dico_2;

    public GameObject Cannonball;
    void Fire(string word)
    {
        Fire_RV.speech = Fire_RV.speech + word + " ";
        string[] words = Fire_RV.speech.Split(' ');
        
        Debug.Log(Fire_RV.speech);

        if (words.Length > 2)
        {
            if (Fire_RV.Dico_1.Contains(words[0]) && Fire_RV.Dico_2.Contains(words[1]))
            {

                Vector3 cannonball_pos_1 = new Vector3(0f, 0f, 0f);
                Vector3 cannonball_pos_2 = new Vector3(0f, 0f, 0f);
                Vector3 cannonball_pos_3 = new Vector3(0f, 0f, 0f);
                Vector3 cannonball_pos_4 = new Vector3(0f, 0f, 0f);
                Vector3 cannonball_pos_5 = new Vector3(0f, 0f, 0f);
                Vector3 cannonball_pos_6 = new Vector3(0f, 0f, 0f);
                
                Quaternion cannonball_rot = new Quaternion();
                
                switch (words[1])
                {
                    case "tribord":
                    case "droite":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_1 = transform.Find("Cannon_T (0)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_2 = transform.Find("Cannon_T (1)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_3 = transform.Find("Cannon_T (2)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_4 = transform.Find("Cannon_T (3)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_5 = transform.Find("Cannon_T (4)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_6 = transform.Find("Cannon_T (5)").position);
                
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_rot = transform.rotation);
                        break;

                    case "babord":
                    case "gauche":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_1 = transform.Find("Cannon_B (0)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_2 = transform.Find("Cannon_B (1)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_3 = transform.Find("Cannon_B (2)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_4 = transform.Find("Cannon_B (3)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_5 = transform.Find("Cannon_B (4)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_6 = transform.Find("Cannon_B (5)").position);
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_rot = Quaternion.LookRotation(-transform.forward, Vector3.up));
                        break;
                }
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_1.y = 2);
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_2.y = 2);
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_3.y = 2);
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_4.y = 2);
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_5.y = 2);
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_6.y = 2);
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos_1, cannonball_rot));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos_2, cannonball_rot));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos_3, cannonball_rot));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos_4, cannonball_rot));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos_5, cannonball_rot));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos_6, cannonball_rot));
            }

            Fire_RV.speech = "";
        }
        
        else if (!Fire_RV.Dico_1.Contains(words[0])) Fire_RV.speech = "";
    }

    void Start()
    {
        speech = "";
        Dico_1 = new[]
        {
            "tirer",
            "feu",
            "cannoner"
        };
        Dico_2 = new[]
        {
            "babord",
            "gauche",
            "tribord",
            "droite"
        };
        
        Recognition.Function Fire_RV = Fire;
        
        Recognition.start_recognition(0, "babord tribord droite gauche tirer feu cannoner", Fire_RV);
    }
}

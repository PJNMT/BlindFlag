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

                Vector3 cannonball_pos = new Vector3(0f, 0f, 0f);
                Quaternion cannonball_rot = new Quaternion();
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_rot = transform.rotation);
                
                switch (words[1])
                {
                    case "tribord":
                    case "droite":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos = transform.Find("Cannon_T").position);
                        break;

                    case "babord":
                    case "gauche":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos = transform.Find("Cannon_B").position);
                        UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_rot = Quaternion.LookRotation(-transform.forward, Vector3.up));
                        break;
                }
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos.y = 2);
                UnityMainThreadDispatcher.Instance().Enqueue(() => Instantiate(Cannonball, cannonball_pos, cannonball_rot));
            }

            Fire_RV.speech = "";
        }
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

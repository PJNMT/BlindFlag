using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatClairvoyant : MonoBehaviour
{
    public static float Clairvoyant_HP;

    public static int nb_death=0; 
    // Start is called before the first frame update
    void Start()
    {
        
        Thread.Sleep(5000);
        if (BlindCaptain_Stat.start_clairvoyant)
        {
            Clairvoyant_HP = 5500;
            transform.position = new Vector3(0,0,0);
        }
    }

}

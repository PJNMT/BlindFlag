using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    public static bool has_win = false;
    // Start is called before the first frame update
    void Start()
    {
        BlindCaptain_Stat.in_clairvoyant = true;
        
        if (!BlindCaptain_Stat.do_seabattle)
        {
            LoadScene.Load(LoadScene.Scene.SeaBattle, LoadScene.Scene.ENDCOMBAT);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

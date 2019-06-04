using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    public static bool has_win = false;

    public AudioClip conv_clairvoyant;
    // Start is called before the first frame update
    void Start()
    {
        BlindCaptain_Stat.in_clairvoyant = true;
        
        //load en tout premier la scene de bataille navaille
        if (!BlindCaptain_Stat.do_seabattle)
        {
            LoadScene.Load(LoadScene.Scene.SeaBattle, LoadScene.Scene.ENDCOMBAT);
        }

        //si le joueur a vaincu les deux lieutenants
        if (BlindCaptain_Stat.nb_ennemy_defeated == 2)
        {
            //dit que clairvoyant a debute et lance leur conversation (est cense le faire après avoir attend le capitaine)
            BlindCaptain_Stat.start_clairvoyant = true;
            GetComponent<AudioSource>().PlayOneShot(conv_clairvoyant);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement : MonoBehaviour
{
    private int x;
    private int y;
    private int z; //si le capitaine se baisse ou non. valeur 1 ou 0 exclusivement.

    /*public deplacement (int posx, int posy, int posz)
    {
      this.x = posx;
        y = posy;
        z = posz;

    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //deplacement tourner = new deplacement(Capitaine.GetPosx,Capitaine.GetPosy,Capitaine.GetPosz);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(1, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(-1,0,1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, 1, 0);
        }

    }

    /*void Translate(int x, int y, int z)
    {
        Capitaine.posx += x;
        Capitaine.posy += y;
        Capitaine.posz += z;
    }*/

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class islandposition : MonoBehaviour
{
    public static float TailleMap = 1000f;
    public int nb_ennemi = 1;
    
    // Start is called before the first frame update
    void Start()
    {     
        if (TailleMap < 200) TailleMap = 1000;
        
        
        float rx = Random.Range(-1f, 1f);
        float rz = Random.Range(-1f, 1f);
        
        float x = 0f;
        if (rx > 0) x = Random.Range(TailleMap/2, 200f);
        else x = Random.Range(-TailleMap/2, -200f);
        
        float z = 0;
        if (rz > 0) z = Random.Range(TailleMap/2, 200f);
        else z = Random.Range(-TailleMap/2, -200f);
        transform.position = new Vector3(x, 1f, z);
        
        
    }

    // Update is called once per frame
    void Update()
    { 
        
    }
}

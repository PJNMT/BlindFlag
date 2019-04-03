using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class islandposition : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-70.0f, 70.0f);
        float z = Random.Range(-70.0f, 70.0f);


        transform.Translate(x, 1f, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

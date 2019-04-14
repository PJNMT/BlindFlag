using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class islandposition : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-300.0f, 300.0f);
        float z = Random.Range(-300.0f, 300.0f);


        transform.Translate(x, 1f, z);

        AssetBundle bundle = AssetBundle.LoadFromFile("Blinflag\\Blind");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

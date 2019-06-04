using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;

public class ennemy : MonoBehaviour
{
    private int x;

    private int z;
    // Start is called before the first frame update
    void Start()
    {
        Thread.Sleep(2200);
        //determine random position de IA sur ile
        x = Random.Range(-55, 55);
        z = Random.Range(-55, 55);
        
        transform.position = new Vector3(x,1, z);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You")
        {
            LoadScene.Load(LoadScene.Scene.ENDCOMBAT, LoadScene.Scene.END);
        }
    }
}

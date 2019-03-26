using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyControler : MonoBehaviour
{
    private float x_rand;
    private float z_rand;
    Random random = new Random();

    private Vector3 position;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        position.Set(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float rng = random;
        while ((x_rand>5) || (z_rand>5) || (x_rand<-5) || (z_rand<-5) || (position != target.position) )
        {
            x_rand = 
            position.Set(x_rand, 0, z_rand);
        }
    }
}

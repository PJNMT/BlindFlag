using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyControler : MonoBehaviour
{
    private float x_rand;
    private float z_rand;
    Random random = new Random();

    private Vector3 position;

    private Transform target;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        position.Set(0, 1, 0);
        player = GameObject.FindWithTag("Captain");
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var newposition = Random.insideUnitCircle * 5;
        position.Set(newposition.x, 1, newposition.y);

        /*float rng = random;
        while ((x_rand>5) || (z_rand>5) || (x_rand<-5) || (z_rand<-5) || (position != target.position) )
        {
            x_rand = 
            position.Set(x_rand, 0, z_rand);
        }*/
    }
}

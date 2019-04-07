using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyControler : MonoBehaviour
{
    private float x_rand;
    private float z_rand;
    Random random = new Random();

    private Transform target;
    private GameObject player;
    private bool hivar = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(0, 1, 0);
        player = GameObject.FindWithTag("Captain");
        target = player.transform;
        int index_attente = 0;
    }

    void ChangePosition()
    {
        var newposition = Random.insideUnitCircle * 5;
        transform.position = new Vector3(newposition.x, 1, newposition.y);
        hivar = false;
    }
    
    IEnumerator ChangeIAposition()
    {
        if (hivar == true)
        {
            ChangePosition();
            yield return new WaitForSeconds(120f);
            hivar = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, target.position)<2)
        {
            StartCoroutine("ChangeIAposition");
        }
        //FIXME
        //Gerer les stats des deux personnages
        //Gerer les attaques des deux personnages
    }
}

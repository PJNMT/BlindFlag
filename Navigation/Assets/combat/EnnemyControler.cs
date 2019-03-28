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
    private int index_attente;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position,transform.position)<1.5)
        {
            if(index_attente==5000)
            {
                ChangePosition();
                index_attente = 0;
            }
            index_attente += 1;
            //attaque
            //ou attendre attaque ?
        }

        //FIXME
        //Trouver un moyen de retarder le déplacement de l'IA
        //Gerer les stats des deux personnages
        //Gerer les attaques des deux personnages
    }
}

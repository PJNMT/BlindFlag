using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouette_3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(Vector3.up, 15 * Time.deltaTime);
        transform.Translate(Vector3.left * 15 * Time.deltaTime); //on avance en f° du temps
    }
}

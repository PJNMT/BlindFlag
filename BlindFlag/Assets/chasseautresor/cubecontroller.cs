using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	public float m_speed = 0.5f;
	
	
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(2f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
	    /*if (Input.GetKey(KeyCode.Space))
	    {
		    m_speed = 1f;
	    }*/ // ne fonctionne pas passe à travers les murs
	    Vector3 move = new Vector3();

	    // Récupération des touches haut et bas
	    if(Input.GetKey(KeyCode.UpArrow))
		    move.x += m_speed;
	    if(Input.GetKey(KeyCode.DownArrow))
		    move.x -= m_speed;

	    // Récupération des touches gauche et droite
	    if(Input.GetKey(KeyCode.LeftArrow))
		    move.z -= m_speed;
	    if(Input.GetKey(KeyCode.RightArrow))
		    move.z += m_speed;

	    // On applique le mouvement à l'objet
	    transform.position += move;
        
        		
    }
}

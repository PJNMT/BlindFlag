using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipcontroller : MonoBehaviour
{
	public float moveSpeed = 20f;
	public float turnSpeed = 50f;
	
	
    // Start is called before the first frame update
    /*void Start()
    {
        transform.position = new Vector3(2f, 2f, 0f);
    }*/

    // Update is called once per frame
    void Update()
    {

	    if (Input.GetKey(KeyCode.UpArrow)) moveSpeed += 1f;
	    if (Input.GetKey(KeyCode.DownArrow)) moveSpeed -= 1f;
	    if(Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
	    if (Input.GetKey(KeyCode.Space)) moveSpeed += 1f;
	    
	    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
	    
	    /*
	    Vector3 move = new Vector3();

	    // Récupération des touches haut et bas
	    if (Input.GetKey(KeyCode.UpArrow))
	    {
		    move.z += m_speed;
		    move.x -= m_speed;
	    }

	    if (Input.GetKey(KeyCode.DownArrow))
	    {
		    move.z -= m_speed;
		    move.x += m_speed;
	    }
	    if (move.x > 0)
	    {
		    move.x = 0;
	    }
        // Récupération des touches gauche et droite
	    if (Input.GetKey(KeyCode.LeftArrow)) rot += 3;
		if(Input.GetKey(KeyCode.RightArrow)) rot -= 3;

	    if (rot > 180) rot -= 180;
	    if (rot < -180) rot += 180;
	    
	    
	    // On applique le mouvement à l'objet
	    transform.position += move;*/
        
        		
    }
}

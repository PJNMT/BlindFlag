using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shipcontroller : MonoBehaviour
{
	public int TailleMap = 1000;
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	public float vmax = 200f;
	private GameObject SEA;
	private int AutoTurn = 1;
	private void Start()
	{
		
		
	}

	// Update is called once per frame
    void Update()
    {
	    bool control = true;
	    if (Math.Abs(transform.position.x) > TailleMap / 2)
	    {
		    transform.Rotate(Vector3.up, turnSpeed * AutoTurn /**Turn()*/ * Time.deltaTime);
		    control = false;
	    }
	    if (Math.Abs(transform.position.z) > TailleMap / 2)
	    {
		    transform.Rotate(Vector3.up, turnSpeed * AutoTurn/* * Turn()*/ * Time.deltaTime);
		    control = false;
	    }
	    
	    if (control)
	    {
		    
		    if (Input.GetKey(KeyCode.UpArrow)) moveSpeed += 5f; //on augmente la vitesse ou on la baisse
		    if (Input.GetKey(KeyCode.DownArrow)) moveSpeed -= 5f;
		    if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		    if (Input.GetKey(KeyCode.RightArrow))
			    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime); //on tourne en f° du temps et non de l'update
		    if (Input.GetKey(KeyCode.Space)) moveSpeed += 5f;


		    if (moveSpeed < 0f) moveSpeed = 0f;
		    if (moveSpeed > vmax) moveSpeed = vmax;
		    
		    

	    }

	    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); //on avance en f° du temps

	    
	    
	    
	    
    }

	/*
	private int Turn()
	{
		
		double ang = transform.eulerAngles.y;
		double babord = DistanceCote(SEA, ang + Math.PI);
		double tribord = DistanceCote(SEA, ang);
		double devant = DistanceCote(SEA, ang - Math.PI/2);


		if (devant < tribord && devant < babord) return 0;
		if (babord > tribord) return 1;
			
		return -1;


	}
	
	private double DistanceCote(GameObject oO, double ang,int x = 100)
	{
		return Math.Sqrt(Math.Pow((oO.transform.position.x - GetComponentInParent<Transform>().position.x - x*Math.Sin(ang)), 2)+
		                 Math.Pow((oO.transform.position.z - GetComponentInParent<Transform>().position.z - x*Math.Cos(ang)), 2));
	}*/

	private double Distance(GameObject O_O)
	{
		return Math.Sqrt(Math.Pow(O_O.transform.position.x - transform.position.x, 2)+
		       Math.Pow(O_O.transform.position.z - transform.position.z, 2));
	}

	
	private void OnTriggerEnter(Collider other)
	{
		/*
		if (other.gameObject.name == "sea")
		{
			SEA = other.gameObject;
		}*/
		if (other.gameObject.name == "island" && 30>Distance(other.gameObject))
		{
            
			if (other.tag == "Ile au trésor")
			{
				
				SceneManager.LoadScene("chasseautrésor");
				BlindShip_Stat.SceneLoad = 7;
			}

			if (other.tag == "Port")
			{
				SceneManager.LoadScene("Port");
				BlindShip_Stat.SceneLoad = 2;
			}

			SceneManager.UnloadSceneAsync("navi");
		}
	}
}

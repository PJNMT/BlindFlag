using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shipcontroller : MonoBehaviour
{
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	
	// Update is called once per frame
    void Update()
    {
	    if (Input.GetKey(KeyCode.UpArrow)) moveSpeed += 1f; //on augmente la vitesse ou on la baisse
	    if (Input.GetKey(KeyCode.DownArrow)) moveSpeed -= 1f;
	    if(Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime); //on tourne en f° du temps et non de l'update
	    if (Input.GetKey(KeyCode.Space)) moveSpeed += 1f;

	    if (moveSpeed < 0f) moveSpeed = 0f;
	    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); //on avance en f° du temps
	}

	private double Distance(GameObject O_O)
	{
		return Math.Sqrt(Math.Pow(O_O.transform.position.x - transform.position.x, 2)+
		       Math.Pow(O_O.transform.position.z - transform.position.z, 2));
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "island" && 30>Distance(other.gameObject))
		{
            
			if (other.tag == "Ile au trésor")
			{
				
				SceneManager.LoadScene("chasseautrésor");
			}

			if (other.tag == "Port")
			{
				SceneManager.LoadScene("Port");
			}

			SceneManager.UnloadSceneAsync("navi");
		}
		
		if (other.gameObject.name == "Cannonball_E(Clone)")
		{
			Debug.Log("on est touche");
			Destroy(other.gameObject, 0f);
			BlindShip_Stat.HP -= AI_enemy.Damage;
		}
	}
}

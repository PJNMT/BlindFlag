﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	public float m_speed;
	public int t_speed;
	public float x;
	public float z;

	private KeyCode inputavant;
	private KeyCode inputarrière;

	private KeyCode inputRotatedroite;
	private KeyCode inputRotategauche;


	public FootStep ground;

	public bool sedeplacer = true;
	public bool isWalkin;
	public AudioSource _audioSource;
	public AudioClip TutoChasse;

	void Touches()
	{
	    inputRotatedroite = KeyCode.RightArrow;
	    inputRotategauche = KeyCode.LeftArrow;
		inputavant = KeyCode.UpArrow;
		inputarrière = KeyCode.DownArrow;
	}
	
	// Start is called before the first frame update
	void Start()
	{
	    transform.position = new Vector3(50f, 0f, 2f);
		x = 2;
		z = 0;

		_audioSource = this.GetComponent<AudioSource>();
		Touches();
		
		if (!BlindCaptain_Stat.Tuto["ChasseAuTresor"])
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() => _audioSource.PlayOneShot(TutoChasse));
			UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoChasse.length * 1000 + 500));

			BlindCaptain_Stat.Tuto["ChasseAuTresor"] = true;
		}
		
	}

	// Update is called once per frame
	void Update()
	{

		if (sedeplacer)
		{
			if (transform.position.x>98)
			{
			    z = transform.position.z;
				transform.position = new Vector3(98f,0f,z);
				
			}

			if (transform.position.z > 98)
			{
				x = transform.position.x;
				transform.position = new Vector3(x,0f,98f);
				
			}
			Vector3 move = new Vector3();

			// Récupération des touches haut et bas
			if (Input.GetKey(KeyCode.UpArrow))
			{
				ground.Sound();
				transform.Translate(Vector3.forward*m_speed*Time.deltaTime);
				
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				ground.Sound();
				transform.Translate(-Vector3.forward*m_speed*Time.deltaTime);
				
			}

			// Récupération des touches gauche et droite
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				ground.Sound();
				transform.Rotate(-Vector3.up * t_speed * Time.deltaTime);
				
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				ground.Sound();
				transform.Rotate(Vector3.up * t_speed * Time.deltaTime);
				
			}
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.name == "Plane")
		{
			ground = other.GetComponent<FootStep>();
		}
		if (other.gameObject.name == "beach" || other.gameObject.name == "beach 2")
		{
			ground = other.GetComponent<FootStep>();
		}
		if (other.gameObject.name == "sea" )
		{
			ground = other.GetComponent<FootStep>();
		}
		if (other.gameObject.name == "grass")
		{
			ground = other.GetComponent<FootStep>();
		}
	}
}

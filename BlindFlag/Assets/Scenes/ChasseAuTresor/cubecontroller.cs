using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	public float m_speed;
	public int t_speed;
	private float x;
	private float z;

	private KeyCode inputdroit;
	private KeyCode inputgauche;
	private KeyCode inputavant;
	private KeyCode inputarrière;

	private KeyCode inputRotatedroite;
	private KeyCode inputRotategauche;

	
	

	public bool sedeplacer = true;
	public AudioSource _audioSource;

	void Touches()
	{
		inputdroit = KeyCode.RightArrow;
	  inputgauche = KeyCode.LeftArrow;
		inputavant = KeyCode.UpArrow;
		inputarrière = KeyCode.DownArrow;
	}
	
	// Start is called before the first frame update
	void Start()
	{
		transform.position = new Vector3(50f, 1f, 5f);
		x = 2;
		z = 0;

		_audioSource = this.GetComponent<AudioSource>();
		Touches();
		
	}

	// Update is called once per frame
	void Update()
	{

		if (sedeplacer)
		{
			Vector3 move = new Vector3();

			// Récupération des touches haut et bas
			if (Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate(Vector3.forward*m_speed*Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				transform.Translate(-Vector3.forward*m_speed*Time.deltaTime);
				
			}

			// Récupération des touches gauche et droite
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				
				transform.Rotate(-Vector3.up * t_speed * Time.deltaTime);
				
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.Rotate(Vector3.up * t_speed * Time.deltaTime);
				
			}

		}
	    
	    
        
        		
	}

	
}

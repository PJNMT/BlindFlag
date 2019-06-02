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

	private KeyCode inputavant;
	private KeyCode inputarrière;

	private KeyCode inputRotatedroite;
	private KeyCode inputRotategauche;

	
	

	public bool sedeplacer = true;
	public bool isGrounded;
	public AudioSource _audioSource;

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

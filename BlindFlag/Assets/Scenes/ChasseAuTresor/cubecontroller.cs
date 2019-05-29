using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	public float m_speed = 0.005f;
	private float x;
	private float z;

	public bool sedeplacer = true;
	public AudioSource _audioSource;
	
	
	// Start is called before the first frame update
	void Start()
	{
		transform.position = new Vector3(46f, 1f, 0f);
		x = 2;
		z = 0;

		_audioSource = this.GetComponent<AudioSource>();
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
				move.x -= m_speed;
				x -= m_speed;
				_audioSource.Play();
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				move.x += m_speed;
				x += m_speed;
				_audioSource.Play();
			}

			// Récupération des touches gauche et droite
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				move.z -= m_speed;
				z -= m_speed;
				_audioSource.Play();
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				move.z += m_speed;
				z += m_speed;
				_audioSource.Play();
			}

			// On applique le mouvement à l'objet
			transform.position += move;
			

		
		}
	    
	    
        
        		
	}

	
}

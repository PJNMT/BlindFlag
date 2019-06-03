using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
	public float m_speed;
	public int t_speed;
	public float x;
	public float z;

	private KeyCode inputavant;
	private KeyCode inputarrière;

	private KeyCode inputRotatedroite;
	private KeyCode inputRotategauche;


	public AudioClip Grass;
	public AudioClip Sea;
	public AudioClip Sand;

	private AudioClip typeofground;

	public bool sedeplacer = true;
	public bool isWalkin;
	public AudioSource _audioSource;
	
	// Start is called before the first frame update
	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		inputRotatedroite = KeyCode.RightArrow;
		inputRotategauche = KeyCode.LeftArrow;
		inputavant = KeyCode.UpArrow;
		inputarrière = KeyCode.DownArrow;
		
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
				_audioSource.PlayOneShot(typeofground);
				transform.Translate(Vector3.forward*m_speed*Time.deltaTime);
				
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				_audioSource.PlayOneShot(typeofground);
				transform.Translate(-Vector3.forward*m_speed*Time.deltaTime);
				
			}

			// Récupération des touches gauche et droite
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				_audioSource.PlayOneShot(typeofground);
				transform.Rotate(-Vector3.up * t_speed * Time.deltaTime);
				
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				_audioSource.PlayOneShot(typeofground);
				transform.Rotate(Vector3.up * t_speed * Time.deltaTime);
				
			}
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
		
		if (other.gameObject.name == "Grass")
		{
			typeofground = Grass;
		}
		if (other.gameObject.name == "Water" )
		{
			typeofground = Sea;
		}
		if (other.gameObject.name == "Sand")
		{
			typeofground = Sand;
		}
	}
}

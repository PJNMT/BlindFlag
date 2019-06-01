using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beach : MonoBehaviour
{
    public AudioClip walk_on_sand;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        cubecontroller a = FindObjectOfType<cubecontroller>();
        _audioSource.clip = walk_on_sand;
    }
}

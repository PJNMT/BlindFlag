using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_controller : MonoBehaviour
{
    public float moveSpeed = 200f;
    public Vector3 vect = Vector3.forward;
    public AudioClip plouf;
    
    // Update is called once per frame
    void Update()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Translate(vect * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "sea")
        {
            Debug.Log("Plouf");
            this.GetComponent<AudioSource>().clip = plouf;
            this.GetComponent<AudioSource>().PlayDelayed(plouf.length);
            Destroy(gameObject, 0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_controller : MonoBehaviour
{
    public float moveSpeed = 200f;
    public Vector3 vect = Vector3.forward;
    
    // Update is called once per frame
    void Update()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Translate(vect * moveSpeed * Time.deltaTime));
        if (gameObject.transform.position.y < -10) Destroy(this.gameObject, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "sea") Debug.Log("Plouf");
    }
}

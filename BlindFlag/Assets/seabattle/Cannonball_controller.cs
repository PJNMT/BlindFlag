using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_controller : MonoBehaviour
{
    public float moveSpeed = 100f;
    public Vector3 vect = Vector3.left;

    // Update is called once per frame
    void Update()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Translate(vect * moveSpeed * Time.deltaTime));
    }
}

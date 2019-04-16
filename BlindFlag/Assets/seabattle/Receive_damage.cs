using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Receive_damage : MonoBehaviour
{
    public float life = 100f;
    public float damage_CB = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cannonball(Clone)")
        {
            Debug.Log("touche");
            Destroy(other.gameObject, 0f);
            life -= damage_CB;
        }
    }

    private void Destroy(object gameObject, float f)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (life <= 0) Destroy(this.gameObject, 0f);
    }

    void Start()
    {
        life = 100f;
    }
}

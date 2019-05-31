using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ship-Shop")
        {
            LoadScene.Load(LoadScene.Scene.ShipShop, LoadScene.Scene.Port);
        }

        if (other.gameObject.name == "Bar")
        {
            LoadScene.Load(LoadScene.Scene.Taverne, LoadScene.Scene.Port);
        }
		
    }
}

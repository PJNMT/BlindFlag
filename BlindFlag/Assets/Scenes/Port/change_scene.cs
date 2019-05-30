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
            SceneManager.LoadScene("ShipShop");
            BlindShip_Stat.SceneLoad = 6;
            SceneManager.UnloadSceneAsync("Port");
        }

        if (other.gameObject.name == "Bar")
        {
            SceneManager.LoadScene("taverne");
            BlindShip_Stat.SceneLoad = 3;
            SceneManager.UnloadSceneAsync("Port");
        }
		
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_shop : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start(){}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "You")
        {
            SceneManager.LoadScene("ShipShop");
            BlindShip_Stat.SceneLoad = 6;
            SceneManager.UnloadSceneAsync("Port");
        }
        
    }

    // Update is called once per frame
    //void Update(){}
}

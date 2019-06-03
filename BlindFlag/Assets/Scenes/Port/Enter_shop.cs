using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_shop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "You")
        {
            Thread.Sleep(2000);
            LoadScene.Load(LoadScene.Scene.ShipShop, LoadScene.Scene.Port);
        }
        
    }

}

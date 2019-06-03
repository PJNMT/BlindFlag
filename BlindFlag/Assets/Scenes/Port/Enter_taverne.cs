using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_taverne : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You")
        {
            Thread.Sleep(2000);
            LoadScene.Load(LoadScene.Scene.Taverne, LoadScene.Scene.Port);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "You")
        {
            LoadScene.Load(LoadScene.Scene.Taverne, LoadScene.Scene.Port);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnclick : MonoBehaviour
{
    public void LoadStartScene()
    {
        LoadScene.Load(LoadScene.Scene.Navigation,LoadScene.Scene.START);
    }
}

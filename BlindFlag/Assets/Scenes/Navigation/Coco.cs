using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using static System.Threading.Thread;
using static UnityEngine.Time;

public class Coco : MonoBehaviour
{
    public static void Paused()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Time.timeScale = 0.0f);
    }

    public static void Play()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Time.timeScale = 1f);
    }
   
}

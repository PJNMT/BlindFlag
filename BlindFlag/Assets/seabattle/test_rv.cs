using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_rv : MonoBehaviour
{

    public static float moveSpeed = 20f;
    public static float turnSpeed = 50f;

    void Move(string speech)
    {
        switch (speech)
        {
            case "tribord":
                UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Rotate(Vector3.up, test_rv.turnSpeed * Time.deltaTime));
                break;

            case "babord":
                UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Rotate(Vector3.up, -test_rv.turnSpeed * Time.deltaTime));
                break;

            case "plus":
                test_rv.moveSpeed += 1f;
                break;

            case "moins":
                test_rv.moveSpeed -= 1f;
                break;
        }

        if (test_rv.moveSpeed < 0f) test_rv.moveSpeed = 0f;
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Translate(Vector3.left * test_rv.moveSpeed * Time.deltaTime));
    }

    void Start()
    {
        Recognition.Function M = Move;
        Recognition.start_recognition(0, "babord tribord plus moins", M);
    }
}
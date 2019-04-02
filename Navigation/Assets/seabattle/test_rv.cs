using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading;
using System.IO;

public class test_rv : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float turnSpeed = 50f;
    string action = "none";
    int i = 0;

    void Start()
    {
        try
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.StartInfo.FileName = "C:\\WINDOWS\\system32\\cmd.exe";
            string path = "D:\\USER\\Desktop\\Projet\\BlindFlag\\Navigation\\Assets\\Recognition.exe";
            myProcess.StartInfo.Arguments = "/c" + path + "recognition 0 tribord babord plus moins none";
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
        }

        catch
        {
            throw new System.Exception("Error");
        }
    }

    // Update is called once per frame
    void Update()
    {    
       if (i == 25)
        {
            i = 0;

            using (StreamReader MyReader = new StreamReader("D:\\USER\\Desktop\\Projet\\BlindFlag\\Navigation\\Assets\\result.txt"))
            {
                string[] results = MyReader.ReadToEnd().Split(' ');
                action = results[results.Length - 1];
            }
        }
        else
        {
            i += 1;
        }

        switch (action)
        {
            case "tribord":
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                break;

            case "babord":
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                break;

            case "plus":
                moveSpeed += 1f;
                break;

            case "moins":
                moveSpeed -= 1f;
                break;
        }

        if (moveSpeed < 0f) moveSpeed = 0f;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}

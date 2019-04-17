using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Synthesis : MonoBehaviour
{
    static Process myProcess;
    
    public static void synthesis(string text)
    {
        // lancement de Recognition.exe
        myProcess = new Process();
            
        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        myProcess.StartInfo.CreateNoWindow = true;
            
        myProcess.StartInfo.FileName = "Recognition.exe";
        myProcess.StartInfo.Arguments = "synthesis " + text;
        myProcess.EnableRaisingEvents = true;

        myProcess.Start();
    }
}

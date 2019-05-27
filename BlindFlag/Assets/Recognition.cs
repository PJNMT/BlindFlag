using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Recognition : MonoBehaviour
{
    public delegate void Function(string speech);

    static TcpListener tcpListener;
    static Thread tcpListenerThread;
    static TcpClient connectedTcpClient;

    static Process myProcess;

    static string speech;
    static Function treatment;

    static bool loop;

    public static void start_recognition(Function f, string KeyWords = "", int time_s = 0)
    {
        try
        {
            treatment = f;
            
            loop = true;

            // lancement de Recognition.exe
            myProcess = new Process();
            
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            
            myProcess.StartInfo.FileName = "Recognition.exe";
            myProcess.StartInfo.Arguments = "recognition " + time_s + ((KeyWords != "") ? " " + KeyWords : "");
            myProcess.EnableRaisingEvents = true;

            myProcess.Start();

            // lancement du serveur TCP sur un autre Thread
            tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
        }

        catch (Exception e)
        {
            throw e;
        }
    }

    public static void stop_recognition()
    {
        myProcess.Kill();
        tcpListener.Stop();
        tcpListenerThread.Abort();
    }

    private static void ListenForIncommingRequests()
    {
        try
        {
            // création du serveur TCP
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
            tcpListener.Start();

            Byte[] bytes = new Byte[1024];
            while (loop)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length; 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);

                            speech = Encoding.ASCII.GetString(incommingData);

                            treatment(speech); // Fonction de traitement
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}

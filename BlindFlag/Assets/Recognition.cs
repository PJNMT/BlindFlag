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
using Debug = UnityEngine.Debug;

public class Recognition : MonoBehaviour
{
    public delegate void Function(string speech);

    static TcpListener tcpListener;
    static Thread tcpListenerThread;
    static TcpClient connectedTcpClient;
    
    private static TcpClient tcpclnt;
    private static ASCIIEncoding asen;
    private static Stream stm;
    private static byte[] ba;

    static Process myProcess;

    static string speech;
    static Function treatment;

    static bool loop;

    public static void start_recognition(Function f, string KeyWords = "", int time_s = 0)
    {
        try
        {
            stop_recognition();
            
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

            
            
            tcpclnt = new TcpClient();
            asen = new ASCIIEncoding();
            tcpclnt.Connect("localhost", 8051);
            stm = tcpclnt.GetStream();
        }

        catch (Exception e)
        {
            throw e;
        }
    }
    
    private static void Transmition(string str)
    {
        try
        {
            ba = asen.GetBytes(str);
            stm.Write(ba, 0, ba.Length);
            Debug.Log("envoie mute/unmute");
        }
        catch
        {
            Environment.Exit(0);
        }
    }

    public static void stop_recognition()
    {
        if (myProcess != null) myProcess.Kill();
        if (tcpListener != null) tcpListener.Stop();
        if (tcpListenerThread != null) tcpListenerThread.Abort();
        if (tcpclnt != null) tcpclnt.Close();
        if (stm != null) stm.Close(); 
    }

    private static void ListenForIncommingRequests()
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

                        if (speech != "ENDOFTRANSMITION")
                        {
                            Transmition("Mute");
                            treatment(speech); // Fonction de traitement
                            Transmition("UnMute");
                        }
                    }
                }
            }
        }
    }
}

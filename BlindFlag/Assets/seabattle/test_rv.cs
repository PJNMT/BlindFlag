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
using System.Collections.Concurrent;

public class test_rv : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float turnSpeed = 50f;
    string action = "plus";
    TcpListener tcpListener;
    Thread tcpListenerThread;
    TcpClient connectedTcpClient;
    

    void Start()
    {
        try
        {
            
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            myProcess.StartInfo.CreateNoWindow = false;
            string path = "D:\\USER\\Desktop\\BlindFlag\\Recognition\\Recognition\\bin\\Debug\\Recognition.exe";
            myProcess.StartInfo.FileName = path;
            myProcess.StartInfo.Arguments = "recognition 0 tribord babord plus moins";
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();

            tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
        }

        catch
        {
            throw new System.Exception("Error");
        }
    }

    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
            tcpListener.Start();
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);

                            action = clientMessage;

                            switch (action)
                            {
                                case "tribord":
                                    UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime));
                                    break;

                                case "babord":
                                    UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime));
                                    break;

                                case "plus":
                                    moveSpeed += 1f;
                                    break;

                                case "moins":
                                    moveSpeed -= 1f;
                                    break;
                            }

                            if (moveSpeed < 0f) moveSpeed = 0f;
                            UnityMainThreadDispatcher.Instance().Enqueue(() => transform.Translate(Vector3.left * moveSpeed * Time.deltaTime));
                        }
                    }
                }                
            }
        }
        catch (SocketException socketException)
        {
            throw socketException;
        }
    }

    /*async Task NetworkLoop()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 9999);
        // we set our IP address as server's address, and we also set the port: 9999

        server.Start();  // this will start the server

        while (true)   //we wait for a connection
        {
            TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it

            NetworkStream ns = client.GetStream();

            while (client.Connected)  //while the client is connected, we look for incoming messages
            {
                byte[] msg = new byte[1024];     //the messages arrive as byte array
                ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                action = (Encoding.UTF8.GetString(msg).Trim()); //now , we write the message as string

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
    }*/
}


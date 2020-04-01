using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody rb;
    Thread receiveThread;
    IPHostEntry ipHost;
    System.Net.IPAddress ipAddr;   
    IPEndPoint localEndPoint;
    string data;
    Socket sender;
   
    
    void Start()
    {
        Debug.Log("This is start");

        try
        {
            //Connection acquiring
            Debug.Log("hey inside");
            //  ipHost = Dns.GetHostEntry(Dns.GetHostName()); 
            ipAddr = System.Net.IPAddress.Parse("127.0.0.1");
            localEndPoint = new IPEndPoint(ipAddr, 5000); 
    
            var path = @"D:\track-object-movement\C# and Python\data.txt";
           //File.WriteAllText(path, String.Empty);
            string text = "Play";
            File.WriteAllText(path, text);
            // Creation TCP/IP Socket using 
            // Socket Class Costructor 
            sender = new Socket(ipAddr.AddressFamily,SocketType.Stream, ProtocolType.Tcp); 
            sender.Connect(localEndPoint); 
          
        }

        catch(Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e.ToString()); 
        }

        InitTCP();
        
    }

    
    // Reset will Abort the thread
    public void reset()
    {
        /*byte[] messageSent = Encoding.ASCII.GetBytes("Stop"); 
        int byteSent = sender.Send(messageSent);*/
        var path = @"D:\track-object-movement\C# and Python\data.txt";
        //File.WriteAllText(path, String.Empty);
        string text = "Stop";
        File.WriteAllText(path, text);
        receiveThread.Abort();
        InitTCP();
        Debug.Log("From Reset");
    }
    public void Done()
    {
        sender.Close();
    }


    private void InitTCP()
    {
        print ("TCP Initialized");
        receiveThread = new Thread (new ThreadStart(ReceiveData)); //1 
        receiveThread.IsBackground = true; //2
        receiveThread.Start (); //3
    }

    private void ReceiveData()
    {
       Debug.Log("From thread");
        try 
        { 
            // Will continuously read data
            
            while(true)
            {

             Debug.Log("From Tcp"); 
             // byte[] messageSent = Encoding.ASCII.GetBytes("Hey there"); 
             // int byteSent = sender.Send(messageSent); 

             // Data buffer 
             byte[] messageReceived = new byte[1024]; 
             int byteRecv = sender.Receive(messageReceived); 
             data = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
             // Send dummy data
             /*byte[] messageSent = Encoding.ASCII.GetBytes("Dummy"); 
             int byteSent = sender.Send(messageSent);*/
             //Debug.Log(data);

            } 
        }
        catch (Exception e)
        { 
            Console.WriteLine("Unexpected exception : {0}", e.ToString()); 
        } 
    } 
    
    
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0, 0, 2500 * Time.deltaTime);
       
        Debug.Log("hey");

        if((string.Compare("left",data) == 0) || Input.GetKey("left"))
        {
        	rb.AddForce(-50 * Time.deltaTime, 0 ,0 ,ForceMode.VelocityChange);
        }

        if(string.Compare("right",data) == 0 || Input.GetKey("right"))
        {
         	rb.AddForce(50 * Time.deltaTime, 0, 0 ,ForceMode.VelocityChange);
        }

        Debug.Log(data);    
        data = "";
    }

}


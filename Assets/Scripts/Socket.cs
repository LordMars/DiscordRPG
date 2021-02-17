using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class DiscordSocket : MonoBehaviour
{
    public string clientip = "127.0.0.1";
    public int clientport = 6000;
    Thread receiveDataThread;
    public Socket clientSocket;
    byte[] buffer = new byte[1024]; //stores data from socket

    public delegate void ReceiveEventHandler(string data);
    public event ReceiveEventHandler receiveEvent; //child class subscribes to this to know when socket has data available

    //Connect to server side of socket
    public Socket connectSocketAsClient(){
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Connecting to Socket");
        clientSocket.Connect(clientip, clientport);

        return clientSocket;
    }

    //Loop that receives data from socket
    public void receiveData(Socket socket){
        while(true){
            parseData();
        }
    }

    //Start a seperate thread to listen for data received from socket. Prevents blocking calls from freezing the main thread
    public void setReceiveDataThread(){
        receiveDataThread = new Thread( new ThreadStart( ()=>{
                receiveData(clientSocket);
            })
        );
        receiveDataThread.Start();
    }

    //clears off excess empty bytes in the buffer
    protected virtual void parseData(){
        int len = clientSocket.Receive(buffer);
        string data = Encoding.UTF8.GetString(buffer).Remove(len);
        buffer.Initialize();//clear the buffer after data receipt
        receiveEvent?.Invoke(data);
    }

}

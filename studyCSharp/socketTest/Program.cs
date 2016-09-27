using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Socket测试
/// </summary>
namespace socketTest
{
    class Program
    {
        
	    private static byte[] result = new byte[1024];  
	    private static int myProt = 8885;   //端口  
	    static Socket serverSocket;

        static void Main(string[] args)
        {

            //服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口  
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            serverSocket.Listen(10);    //设定最多10个排队连接请求 
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }

        /// <summary>  
        /// 监听客户端连接  
        /// </summary>  
        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Console.WriteLine("用户已连接");
                //Thread receiveThread = new Thread(ReceiveMessage);
                //receiveThread.Start(clientSocket);
            }
        }
    }
}

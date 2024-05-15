using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        Socket socket;

        public bool StartServer()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5556);
                socket.Bind(endPoint);

                ThreadStart threadStart = Listen;
                new Thread(threadStart).Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool StopServer()
        {
             try
            {
                socket.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Listen()
        {
            try
            {
                while (true)
                {
                    socket.Listen(5);
                    Socket clientSocket = socket.Accept();
                    NetworkStream stream = new NetworkStream(clientSocket);
                    new ClientHandler(stream); 
                }
            }
            catch (Exception)
            {

            }
        }

    }
}

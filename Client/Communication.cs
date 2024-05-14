using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Communication
    {
        TcpClient client;
        NetworkStream stream;
        BinaryFormatter formatter;

        public bool ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5555);
                stream = client.GetStream();
                formatter = new BinaryFormatter();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        void Stop()
        {
            DTO transfer = new DTO();
            transfer.Operation = Operations.End;
            formatter.Serialize(stream, transfer);
        }
    }
}

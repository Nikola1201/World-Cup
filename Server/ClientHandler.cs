using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ClientHandler
    {
        private NetworkStream stream;
        private BinaryFormatter formatter;

        public ClientHandler(NetworkStream stream)
        {
            this.stream = stream;
            formatter = new BinaryFormatter();
        }

        public void Handle()
        {
            try
            {
                int operation = 0;
                while (operation != (int)Operations.End)
                {
                    DTO transfer = formatter.Deserialize(stream) as DTO;
                    switch (transfer.Operation)
                    {
                        case Operations.End:
                            operation = 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

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
        private TcpClient client;
        private NetworkStream stream;
        private BinaryFormatter formatter;

        public bool ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5556);
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
        public List<Country> GetAllCountries()
        {
            // Request
            DTO transfer = new DTO();
            transfer.Operation = Operations.GetAllCountries;
            formatter.Serialize(stream, transfer);

            // Response
            transfer = formatter.Deserialize(stream) as DTO;
            return transfer.Result as List<Country>;
        }
        public bool SavePairs(List<Pair> pairs)
        {
            // Request
            DTO transfer = new DTO();
            transfer.Operation = Operations.SavePairs;
            transfer.TransferObject = pairs;
            formatter.Serialize(stream, transfer);

            //Response

            transfer = formatter.Deserialize(stream) as DTO;
            return (bool)transfer.Result;
        }
        public bool ExistingSchedule(List<Pair>pairs)
        {
            // Request
            DTO transfer = new DTO();
            transfer.Operation = Operations.ExistingSchedule;
            transfer.TransferObject = pairs;
            formatter.Serialize(stream, transfer);

            //Response

            transfer = formatter.Deserialize(stream) as DTO;
            return (bool)transfer.Result;
        }
    }
}

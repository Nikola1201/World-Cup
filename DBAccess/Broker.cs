using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class Broker
    {
        SqlConnection connection;
        SqlCommand command;
        SqlTransaction transaction;

        public Broker()
        {
            connection = new SqlConnection(@"");
            command = connection.CreateCommand();
        }
        // Singleton pattern

        public static Broker instance;

        public static Broker Instance()
        {
            if (instance == null) instance = new Broker();
            return instance;
        }
    }
}

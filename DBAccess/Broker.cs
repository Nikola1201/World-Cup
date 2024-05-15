using Domain;
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
            connection = new SqlConnection(@"Data Source=desktop-u6itn59\sqlexpress;Initial Catalog=WorldCup;Integrated Security=True;TrustServerCertificate=True");
            command = connection.CreateCommand();
        }
        // Singleton pattern

        public static Broker instance;

        public static Broker Instance()
        {
            if (instance == null) instance = new Broker();
            return instance;
        }

        public List<Country> GetAllCountries()
        {
            List<Country> countries = new List<Country>();
            try
            {
                connection.Open();
                command.CommandText = "Select * FROM Country";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    Country c = new Country();
                    c.Id = Convert.ToInt32(reader["ID"]);
                    c.Name = Convert.ToString(reader["Name"]);
                    countries.Add(c);
                }
                reader.Close();
                return countries;
            }
            catch (Exception)
            {

                throw;
            }
            finally { if (connection != null) connection.Close(); }
        }
    }
}

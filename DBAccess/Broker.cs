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

        public int GetPairId()
        {
            try
            {
                command.CommandText = "SELECT MAX(ID) FROM Pair";
                try
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    return id+1;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ExistingSchedule(List<Pair>pairs)
        {
          
            Pair p = pairs[0];
            try
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(Date) FROM Pair WHERE Date = @date";
                command.Parameters.AddWithValue("@date", p.Date.ToString("yyyy-MM-dd"));
                try
                {
                    int exists = Convert.ToInt32(command.ExecuteScalar());
                    command.Parameters.Clear();
                    if (exists > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                    }
            catch (Exception)
            {

                throw;
            }
            finally { if (connection != null) connection.Close(); }
        }
        public bool SavePairs(List<Pair> pairs)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command = new SqlCommand("", connection, transaction);
                
                foreach(Pair p in pairs)
                {
                    p.Id=GetPairId();
                    command.CommandText ="INSERT INTO Pair VALUES "+"(@id,@homeTeam,@awayTeam,@date)";
                    command.Parameters.AddWithValue("@id", p.Id);
                    command.Parameters.AddWithValue("@homeTeam", p.HomeTeam.Id);
                    command.Parameters.AddWithValue("@awayTeam", p.AwayTeam.Id);
                    command.Parameters.AddWithValue("@date", p.Date.ToString("yyyy-MM-dd"));
                    
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
           
                }
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

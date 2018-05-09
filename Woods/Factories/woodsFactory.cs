using Woods.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

namespace Woods.Factory{
   public class TrailFactory{

       string Server = "localhost";
       string Port = "3306";
       string  Database = "woods";
       string User = "root";
       string Password ="root";
       private string connectionString;
        public TrailFactory()
        {
            connectionString = $"server={Server};userid={User};password={Password};port={Port};database={Database};SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Trail trail)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO trails (name, description, length, elevation, longitude, latitude) VALUES (@Name, @Description, @Length, @Elevation, @Longitude, @Latitude )";
                dbConnection.Open();
                dbConnection.Execute(query, trail);
            }
        }
        public List<Trail> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails").ToList();
            }
        }
        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }


   }
}

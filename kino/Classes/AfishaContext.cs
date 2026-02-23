using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kino.Classes.Common;
using kino.Modell;
using MySql.Data.MySqlClient;

namespace kino.Classes
{
    public class AfishaContext : Afisha
    {
        public AfishaContext(int Id, int IdKinoteatr, string Name, DateTime Time, int Price) : base(Id, IdKinoteatr, Name, Time, Price) { }

        public static List<AfishaContext> Select()
        {
            List<AfishaContext> AllAfishas = new List<AfishaContext>();
            string SQL = "SELECT * FROM `afisha`";

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);

            while (Data.Read())
            {
                AllAfishas.Add(new AfishaContext(
                    Data.GetInt32(0),     // id
                    Data.GetInt32(4),     // id_kinoteatr (это 4-й индекс!)
                    Data.GetString(1),    // name
                    Data.GetDateTime(2),  // time  
                    Data.GetInt32(3)      // price
                ));
            }

            Connection.CloseConnection(connection);
            return AllAfishas;
        }
        public void Add()
        {
            string SQL = "INSERT INTO " +
            "afisha(" +
            "id_kinoteatr, " +
            "name, " +
            "time, " +
            "price) " +
            "VALUES (" +
            $"'{this.IdKinoteatr}', " +
            $"'{this.Name}', " +
            $"'{this.Time.ToString("yyyy-MM-dd HH:mm:ss")}', " +
            $"'{this.Price}')";

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Update()
        {
            string SQL = "UPDATE " +
            "afisha " +
            "SET " +
            $"id_kinoteatr='{this.IdKinoteatr}', " +
            $"name='{this.Name}', " +
            $"time='{this.Time.ToString("yyyy-MM-dd HH:mm:ss")}', " +
            $"price='{this.Price}' " +
            "WHERE " +
            $"id='{this.Id}'";

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM `afisha` WHERE `id` = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}

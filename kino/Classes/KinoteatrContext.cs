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
    public class KinoteatrContext : Kinoteatr
    {
        public KinoteatrContext(int Id, string Name, int CountZal, int Count) : base(Id, Name, CountZal, Count) { }

        public static List<KinoteatrContext> Select()
        {
            List<KinoteatrContext> AllKinoteatrs = new List<KinoteatrContext>();
            string SQL = "SELECT * FROM `Kinoteatr`";

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);

            while (Data.Read())
            {
                AllKinoteatrs.Add(new KinoteatrContext(
                    Data.GetInt32(0),
                    Data.GetString(1),
                    Data.GetInt32(2),
                    Data.GetInt32(3)
                ));
            }

            Connection.CloseConnection(connection);
            return AllKinoteatrs;
        }

        public void Add()
        {
            string SQL = "INSERT INTO " +
            "kinoteatr(" +
            "name, " +
            "count_zal, " +
            "count) " +
            "VALUES " +
            $"('{this.Name}', " +
            $"'{this.CountZal}', " +
            $"'{this.Count}')";

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Update()
        {
            string SQL = "UPDATE " +
            "kinoteatr " +
            "SET " +
            $"name='{this.Name}', " +
            $"count_zal='{this.CountZal}', " +
            $"count='{this.Count}' " +
            "WHERE " +
            $"id='{this.Id}'";

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `kinoteatr` WHERE `id` = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}

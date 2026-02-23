using System;
using System.Collections.Generic;
using kino.Classes.Common;
using MySql.Data.MySqlClient;

namespace kino.Classes
{
    public class TicketContext : Ticket
    {
        public TicketContext(int id, int idAfisha, string clientName, int seatNumber, DateTime purchaseDate, decimal price, bool isSold)
            : base(id, idAfisha, clientName, seatNumber, purchaseDate, price, isSold) { }

        public void Add()
        {
            string SQL = "INSERT INTO tickets (id_afisha, client_name, seat_number, purchase_date, price, is_sold) VALUES " +
                        $"('{this.IdAfisha}', '{this.ClientName}', '{this.SeatNumber}', '{this.PurchaseDate:yyyy-MM-dd HH:mm:ss}', '{this.Price}', 1)";

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public static List<int> GetSoldSeats(int idAfisha)
        {
            List<int> soldSeats = new List<int>();
            string SQL = $"SELECT seat_number FROM tickets WHERE id_afisha = {idAfisha} AND is_sold = 1";

            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader reader = Connection.Query(SQL, connection);

            while (reader.Read())
                soldSeats.Add(reader.GetInt32(0));

            Connection.CloseConnection(connection);
            return soldSeats;
        }
    }
}
using System;

namespace kino.Classes
{
    public class Ticket
    {
        public int Id { get; set; }
        public int IdAfisha { get; set; }
        public string ClientName { get; set; }
        public int SeatNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }

        public Ticket() { }

        public Ticket(int id, int idAfisha, string clientName, int seatNumber,
                     DateTime purchaseDate, decimal price, bool isSold)
        {
            Id = id;
            IdAfisha = idAfisha;
            ClientName = clientName;
            SeatNumber = seatNumber;
            PurchaseDate = purchaseDate;
            Price = price;
            IsSold = isSold;
        }
    }
}
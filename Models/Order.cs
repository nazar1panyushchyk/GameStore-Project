namespace GameStore.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int Id;
        public int ClientId;
        public List<Game> Games;
        public string GameIds;
        public double Discount;
        public double TotalPrice;

        public Order(int id, int clientId, string gameIds, double discount, double totalPrice)
        {
            Id = id;
            ClientId = clientId;
            GameIds = gameIds;
            Discount = discount;
            TotalPrice = totalPrice;
        }

        public Order(List<Game> games, double discount)
        {
            Games = games;
            Discount = discount;
        }

        public Order() { }
    }
}
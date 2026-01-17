using System.Collections.Generic;

namespace GameStore.Models
{
    /// <summary>
    /// Клас, що описує замовлення в системі.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="id">ID замовлення.</param>
        /// <param name="clientId">ID клієнта.</param>
        /// <param name="gameIds">Рядок з ID ігор.</param>
        /// <param name="discount">Знижка.</param>
        /// <param name="totalPrice">Загальна сума.</param>
        public Order(int id, int clientId, string gameIds, double discount, double totalPrice)
        {
            Id = id;
            ClientId = clientId;
            GameIds = gameIds;
            Discount = discount;
            TotalPrice = totalPrice;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="games">Список об'єктів ігор, обраних користувачем.</param>
        /// <param name="discount">Випадкова знижка, згенерована системою.</param>
        public Order(List<Game> games, double discount)
        {
            Games = games;
            Discount = discount;
        }

        public int Id { get; set; }

        public int ClientId { get; set; }

        public List<Game> Games { get; set; }

        public string GameIds { get; set; }

        public double Discount { get; set; }

        public double TotalPrice { get; set; }
    }
}

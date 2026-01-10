namespace GameStore.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Клас, що описує замовлення в системі.
    /// </summary>
    public class Order
    {
        public int Id;
        public int ClientId;
        public List<Game> Games;
        public string GameIds;
        public double Discount;
        public double TotalPrice;

        /// <summary>
        /// Конструктор для створення об'єкта замовлення при зчитуванні з файлу.
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
        /// Конструктор для створення нового замовлення в процесі покупки.
        /// Ініціалізує список ігор та знижку перед збереженням у базу.
        /// </summary>
        /// <param name="games">Список об'єктів ігор, обраних користувачем.</param>
        /// <param name="discount">Випадкова знижка, згенерована системою.</param>
        public Order(List<Game> games, double discount)
        {
            Games = games;
            Discount = discount;
        }

        /// <summary>
        /// Порожній конструктор для ініціалізації без параметрів.
        /// </summary>
        public Order() { }
    }
}
namespace GameStore.Models
{
    /// <summary>
    /// Клас, що моделює сутність "Гра" в магазині.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="id">Унікальний ідентифікатор.</param>
        /// <param name="name">Назва гри.</param>
        /// <param name="genre">Жанр.</param>
        /// <param name="rating">Рейтинг (0-10).</param>
        /// <param name="price">Ціна товару.</param>
        public Game(int id, string name, string genre, double rating, decimal price)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Rating = rating;
            Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public double Rating { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Перевіряє, чи є об'єкт гри порожнім (некоректним).
        /// </summary>
        /// <returns>True, якщо назва гри відсутня.</returns>
        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Name);
        }
    }
}

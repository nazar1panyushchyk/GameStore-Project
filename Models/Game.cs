namespace GameStore.Models
{
    /// <summary>
    /// Клас, що моделює сутність "Гра" в магазині.
    /// </summary>
    public class Game
    {
        public int Id;
        public string Name;
        public string Genre;
        public double Rating;
        public decimal Price;

        /// <summary>
        /// Ініціалізує новий екземпляр гри.
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

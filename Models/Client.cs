namespace GameStore.Models
{
    /// <summary>
    /// Клас, що моделює сутність "Клієнт".
    /// Зберігає персональні дані покупця для історії замовлень.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="id">Унікальний ідентифікатор у системі.</param>
        /// <param name="name">Повне ім'я або псевдонім клієнта.</param>
        /// <param name="email">Електронна пошта для зв'язку.</param>
        public Client(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Перевіряє, чи є об'єкт клієнта коректним (чи заповнене ім'я).
        /// Використовується для запобігання створенню пустих записів.
        /// </summary>
        /// <returns>True, якщо ім'я клієнта відсутнє або порожнє.</returns>
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email);
        }
    }
}

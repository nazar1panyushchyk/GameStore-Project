namespace GameStore.Models
{
    /// <summary>
    /// Клас, що моделює сутність "Клієнт".
    /// Зберігає персональні дані покупця для історії замовлень.
    /// </summary>
    public class Client
    {
        public int Id;
        public string Name;
        public string Email;

        /// <summary>
        /// Ініціалізує новий екземпляр клієнта.
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

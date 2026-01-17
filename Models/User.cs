namespace GameStore.Models
{
    /// <summary>
    /// Клас, що описує обліковий запис користувача (адміністратора) системи.
    /// Використовується для автентифікації та надання доступу до функцій магазину.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">Унікальний системний номер.</param>
        /// <param name="email">Логін (електронна пошта) для входу.</param>
        /// <param name="passwordHash">Пароль (у навчальних цілях зберігається як текст).</param>
        public User(int id, string email, string passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}

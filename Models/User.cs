using System;

namespace GameStore.Models
{
    /// <summary>
    /// Клас, що описує обліковий запис користувача (адміністратора) системи.
    /// Використовується для автентифікації та надання доступу до функцій магазину.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        /// <summary>
        /// Ініціалізує нового користувача з обліковими даними.
        /// </summary>
        /// <param name="id">Унікальний системний номер.</param>
        /// <param name="email">Логін (електронна пошта) для входу.</param>
        /// <param name="password">Пароль (у навчальних цілях зберігається як текст).</param>
        public User(int id, string email, string passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}

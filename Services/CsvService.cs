using System;
using System.Collections.Generic;
using System.IO;
using GameStore.Models;

/// <summary>
/// Сервіс для роботи з CSV-файлами.
/// Забезпечує читання та запис даних про ігри, клієнтів, користувачів та замовлення.
/// </summary>
public class CsvService
{
    private string folderPath;

    /// <summary>
    /// Ініціалізує новий екземпляр сервісу та перевіряє наявність робочої директорії.
    /// </summary>
    /// <param name="folderPath">Шлях до папки з файлами даних.</param>
    public CsvService(string folderPath)
    {
        this.folderPath = folderPath;
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }

    /// <summary>
    /// Додає новий запис про гру в кінець файлу games.csv.
    /// Якщо файл не існує, створює його та додає заголовок.
    /// </summary>
    /// <param name="g">Об'єкт гри, який необхідно зберегти.</param>
    public void AddGame(Game g)
    {
        string path = Path.Combine(folderPath, "games.csv");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.WriteLine("Id,Name,Genre,Rating,Price");
        }

        using (StreamWriter sw = new StreamWriter(path, true))
        {
            string ratingStr = Math.Round(g.Rating, 2).ToString().Replace(",", ".");
            string priceStr = Math.Round(g.Price, 2).ToString().Replace(",", ".");
            sw.WriteLine($"{g.Id},{g.Name},{g.Genre},{ratingStr},{priceStr}");
        }
    }

    /// <summary>
    /// Зчитує повний список ігор з файлу games.csv.
    /// Виконує парсинг текстових рядків у об'єкти класу Game.
    /// </summary>
    /// <returns>Список об'єктів Game або порожній список, якщо файл відсутній.</returns>
    public List<Game> GetAllGames()
    {
        string path = Path.Combine(folderPath, "games.csv");
        var games = new List<Game>();

        if (!File.Exists(path))
            return games;

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            var g = new Game(int.Parse(parts[0]), parts[1], parts[2], double.Parse(parts[3].Replace(".", ",")), decimal.Parse(parts[4].Replace(".", ",")));
            games.Add(g);
        }

        return games;
    }

    /// <summary>
    /// Оновлює інформацію про існуючого клієнта у файлі clients.csv.
    /// </summary>
    /// <param name="updatedClient">Об'єкт клієнта з оновленими даними.</param>
    public void UpdateGame(Game g)
    {
        string path = Path.Combine(folderPath, "games.csv");
        if (!File.Exists(path)) return;

        var lines = File.ReadAllLines(path);
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (int.Parse(parts[0]) == g.Id)
                {
                    string ratingStr = Math.Round(g.Rating, 2).ToString().Replace(",", ".");
                    string priceStr = Math.Round(g.Price, 2).ToString().Replace(",", ".");
                    sw.WriteLine($"{g.Id},{g.Name},{g.Genre},{ratingStr},{priceStr}");
                }
                else
                    sw.WriteLine(lines[i]);
            }
        }
    }

    /// <summary>
    /// Видаляє гру з файлу games.csv за її ідентифікатором.
    /// Зчитує всі ігри, видаляє потрібну та повністю перезаписує файл.
    /// </summary>
    /// <param name="id">Ідентифікатор гри для видалення.</param>
    public void DeleteGame(int id)
    {
        string path = Path.Combine(folderPath, "games.csv");
        if (!File.Exists(path)) return;

        var lines = File.ReadAllLines(path);
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (int.Parse(parts[0]) != id)
                    sw.WriteLine(lines[i]);
            }
        }
    }

    /// <summary>
    /// Додає нового клієнта до файлу clients.csv.
    /// Створює файл із заголовками, якщо він ще не існує.
    /// </summary>
    /// <param name="c">Об'єкт клієнта, який потрібно зберегти.</param>
    public void AddClient(Client c)
    {
        string path = Path.Combine(folderPath, "clients.csv");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.WriteLine("Id,Name,Email");
        }

        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine($"{c.Id},{c.Name},{c.Email}");
        }
    }

    /// <summary>
    /// Зчитує всіх клієнтів з файлу clients.csv.
    /// </summary>
    public List<Client> GetAllClients()
    {
        string path = Path.Combine(folderPath, "clients.csv");
        var clients = new List<Client>();

        if (!File.Exists(path))
            return clients;

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            var c = new Client(int.Parse(parts[0]), parts[1], parts[2]);
            clients.Add(c);
        }

        return clients;
    }

    /// <summary>
    /// Оновлює дані клієнта у файлі.
    /// </summary>
    public void UpdateClient(Client c)
    {
        string path = Path.Combine(folderPath, "clients.csv");
        if (!File.Exists(path)) return;

        var lines = File.ReadAllLines(path);
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (int.Parse(parts[0]) == c.Id)
                    sw.WriteLine($"{c.Id},{c.Name},{c.Email}");
                else
                    sw.WriteLine(lines[i]);
            }
        }
    }

    /// <summary>
    /// Видаляє клієнта з файлу clients.csv за його ідентифікатором.
    /// Після видалення перезаписує файл для збереження актуального стану.
    /// </summary>
    /// <param name="id">Ідентифікатор клієнта для видалення.</param>
    public void DeleteClient(int id)
    {
        string path = Path.Combine(folderPath, "clients.csv");
        if (!File.Exists(path)) return;

        var lines = File.ReadAllLines(path);
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (int.Parse(parts[0]) != id)
                    sw.WriteLine(lines[i]);
            }
        }
    }

    /// <summary>
    /// Зберігає інформацію про нове замовлення у файл orders.csv.
    /// </summary>
    /// <param name="order">Об'єкт замовлення для запису.</param>
    public void AddOrder(Order o)
    {
        string path = Path.Combine(folderPath, "orders.csv");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.WriteLine("Id,ClientId,GameIds,Discount,TotalPrice");
        }

        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine($"{o.Id},{o.ClientId},{o.GameIds},{o.Discount},{o.TotalPrice}");
        }
    }

    /// <summary>
    /// Зчитує історію замовлень з файлу orders.csv.
    /// </summary>
    public List<Order> GetAllOrders()
    {
        string path = Path.Combine(folderPath, "orders.csv");
        var orders = new List<Order>();

        if (!File.Exists(path))
            return orders;

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            if (parts.Length < 5)
                continue;

            if (!int.TryParse(parts[0], out int id))
                continue;
            if (!int.TryParse(parts[1], out int clientId))
                clientId = 0;

            string gameIds = parts[2];

            double discount = 0;
            double totalPrice = 0;
            double.TryParse(parts[3].Replace(".", ","), out discount);
            double.TryParse(parts[4].Replace(".", ","), out totalPrice);

            if (id == 0 && (string.IsNullOrWhiteSpace(gameIds) || totalPrice == 0))
                continue;

            var o = new Order
            {
                Id = id,
                ClientId = clientId,
                GameIds = gameIds,
                Discount = discount,
                TotalPrice = totalPrice
            };
            orders.Add(o);
        }

        return orders;
    }

    /// <summary>
    /// Зберігає повний список замовлень у файл, перезаписуючи його.
    /// </summary>
    public void SaveAllOrders(List<Order> orders)
    {
        string path = Path.Combine(folderPath, "orders.csv");
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine("Id,ClientId,GameIds,Discount,TotalPrice");
            foreach (var order in orders)
            {
                sw.WriteLine($"{order.Id},{order.ClientId},{order.GameIds},{order.Discount},{order.TotalPrice}");
            }
        }
    }

    /// <summary>
    /// Зчитує список усіх зареєстрованих користувачів (адміністраторів) з файлу users.csv.
    /// Виконує перевірку на коректність даних у рядках файлу.
    /// </summary>
    /// <returns>Список об'єктів User.</returns>
    public List<User> GetAllUsers()
    {
        string path = Path.Combine(folderPath, "users.csv");
        var users = new List<User>();

        if (!File.Exists(path))
            return users;

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            if (parts.Length < 3)
                continue;

            if (!int.TryParse(parts[0], out int id))
                continue;

            var u = new User(id, parts[1], parts[2]);
            users.Add(u);
        }

        return users;
    }

    /// <summary>
    /// Реєструє нового користувача в системі, додаючи запис у файл users.csv.
    /// </summary>
    /// <param name="u">Об'єкт користувача з логіном та паролем.</param>
    public void AddUser(User u)
    {
        string path = Path.Combine(folderPath, "users.csv");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.WriteLine("Id,Email,Password");
        }

        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine($"{u.Id},{u.Email},{u.PasswordHash}");
        }
    }

    /// <summary>
    /// Перевіряє, чи існує користувач із вказаною електронною поштою.
    /// Використовується при реєстрації для уникнення дублікатів.
    /// </summary>
    /// <param name="email">Email для перевірки.</param>
    /// <returns>True, якщо користувач вже існує, інакше False.</returns>
    public bool UserExists(string email)
    {
        var users = GetAllUsers();
        foreach (var u in users)
        {
            if (u.Email == email)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Знаходить користувача (адміністратора) за email для авторизації.
    /// </summary>
    public User GetUserByEmail(string email)
    {
        var users = GetAllUsers();
        foreach (var u in users)
        {
            if (u.Email == email)
                return u;
        }
        return null;
    }

}

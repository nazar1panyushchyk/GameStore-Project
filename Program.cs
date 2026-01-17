using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameStore.Models;

namespace GameStore
{
    internal class Program
    {
        private static readonly string BaseDir = Directory.GetCurrentDirectory();

        private static readonly string CsvFolder =
            Path.Combine(BaseDir, "CSV");

        private static readonly string GamesFile =
            Path.Combine(CsvFolder, "games.csv");

        private static readonly string ClientsFile =
            Path.Combine(CsvFolder, "clients.csv");

        private static readonly string OrdersFile =
            Path.Combine(CsvFolder, "orders.csv");

        private static readonly string UsersFile =
            Path.Combine(CsvFolder, "users.csv");

        private static CsvService csvService = new CsvService("CSV");

        private static List<Game> games = new List<Game>();
        private static List<Client> clients = new List<Client>();
        private static List<Order> buyHistoryMenu = new List<Order>();

        /// <summary>
        /// Головна точка входу в програму.
        /// Відповідає за створення необхідних директорій, завантаження даних
        /// та запуск циклу авторизації користувача.
        /// </summary>
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Directory.CreateDirectory(CsvFolder);

            LoadData();

            int attempts = 3;
            bool loggedIn = false;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Вітаємо в GameStore! ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Вхід");
            Console.WriteLine("2. Реєстрація\n");
            Console.ResetColor();

            int choice = ReadInt("Виберіть опцію (1 або 2): ", 1, 2);

            if (choice == 1)
            {
                loggedIn = LoginUser(attempts);
            }
            else
            {
                loggedIn = RegisterUser(attempts);
            }

            if (!loggedIn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Спроби вичерпано. Програма завершує роботу.");
                Console.ResetColor();
                Environment.Exit(0);
            }

            Console.Clear();

            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("=== Вітаємо в нашому GameStore! ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Каталог ігор");
                Console.WriteLine("2. Клієнти");
                Console.WriteLine("3. Замовлення");
                Console.WriteLine("4. Платежі");
                Console.WriteLine("5. Рейтинги");
                Console.WriteLine("6. Фільтри за жанром");
                Console.WriteLine("7. Історія покупок");
                Console.WriteLine("8. Статистика");
                Console.WriteLine("9. Звіт");
                Console.WriteLine("0. Вийти з магазину\n");
                Console.ResetColor();

                int menuChoice = ReadInt("Введіть номер бажаної категорії: ", 0, 9);

                switch (menuChoice)
                {
                    case 1:
                        GameList();
                        break;
                    case 2:
                        ClientsMenu();
                        break;
                    case 3:
                        Orders();
                        break;
                    case 4:
                        Payments();
                        break;
                    case 5:
                        Ratings();
                        break;
                    case 6:
                        Filters();
                        break;
                    case 7:
                        ShowBuyHistory();
                        break;
                    case 8:
                        Statistics();
                        break;
                    case 9:
                        Report();
                        break;
                    case 0:
                        SaveData();
                        Console.WriteLine("Вихід з магазину...");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
        }

        /// <summary>
        /// Здійснює завантаження даних про ігри та клієнтів із CSV-файлів у оперативну пам'ять.
        /// Використовує методи CsvService для читання файлів.
        /// </summary>
        private static void LoadData()
        {
            games = csvService.GetAllGames();
            clients = csvService.GetAllClients();
            buyHistoryMenu = csvService.GetAllOrders();
        }

        /// <summary>
        /// Зберігає поточні зміни в списках ігор та клієнтів назад у CSV-файли.
        /// Викликається перед завершенням роботи програми.
        /// </summary>
        private static void SaveData()
        {
            foreach (var g in games)
            {
                csvService.UpdateGame(g);
            }

            foreach (var c in clients)
            {
                csvService.UpdateClient(c);
            }
        }

        /// <summary>
        /// Відображає меню управління каталогом ігор.
        /// Надає доступ до перегляду, додавання, редагування, видалення та пошуку.
        /// </summary>
        private static void GameList()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Каталог ігор ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Переглянути всі ігри");
            Console.WriteLine("2. Додати нову гру");
            Console.WriteLine("3. Редагувати гру");
            Console.WriteLine("4. Видалити гру");
            Console.WriteLine("5. Пошук гри");
            Console.WriteLine("6. Сортування ігор");
            Console.WriteLine("7. Назад\n");
            Console.ResetColor();

            int choice = ReadInt("Введіть номер дії: ", 1, 7);

            switch (choice)
            {
                case 1:
                    ShowGameList();
                    break;
                case 2:
                    AddGames();
                    break;
                case 3:
                    EditGame();
                    break;
                case 4:
                    DeleteGame();
                    break;
                case 5:
                    SearchGame();
                    break;
                case 6:
                    SortGames();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }

        /// <summary>
        /// Виводить на екран повний список ігор у вигляді форматованої таблиці.
        /// </summary>
        private static void ShowGameList()
        {
            Console.Clear();
            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор наразі не додано!");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7} {4,-8}", "ID", "Назва", "Жанр", "Рейтинг", "Ціна");
            Console.WriteLine(new string('-', 65));

            foreach (var g in games)
            {
                Console.WriteLine(
                    "{0,-5} {1,-25} {2,-15} {3,-7:F1} {4,-8:F2}",
                    g.Id,
                    g.Name,
                    g.Genre,
                    g.Rating,
                    g.Price);
            }

            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Виводить на екран повний список ігор у вигляді форматованої таблиці.
        /// </summary>
        private static void ShowGameList(List<Game> list)
        {
            foreach (var game in list)
            {
                Console.WriteLine($"{game.Id}. {game.Name} — {game.Price} грн");
            }
        }

        /// <summary>
        /// Виводить форматовану таблицю всіх ігор із заголовками.
        /// Використовується як допоміжний метод перед операціями редагування або видалення.
        /// </summary>
        private static void ShowGamesTable()
        {
            Console.Clear();

            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор наразі немає!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7} {4,-8}", "ID", "Назва", "Жанр", "Рейтинг", "Ціна");
            Console.WriteLine(new string('-', 70));
            Console.ResetColor();

            foreach (var g in games)
            {
                Console.WriteLine(
                    "{0,-5} {1,-25} {2,-15} {3,-7:F1} {4,-8:F2}",
                    g.Id,
                    g.Name,
                    g.Genre,
                    g.Rating,
                    g.Price);
            }
        }

        /// <summary>
        /// Додає нові ігри до каталогу.
        /// Перевіряє ліміт на кількість ігор (максимум 5) та запитує дані у користувача.
        /// </summary>
        private static void AddGames()
        {
            Console.Clear();

            var gamesList = csvService.GetAllGames();

            if (gamesList.Count >= 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Каталог вже містить 5 ігор. Додати нову поки не можна.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            int gamesToAdd = 5 - gamesList.Count;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Ви можете додати ще стільки ігор: {gamesToAdd}");
            Console.ResetColor();

            for (int i = 0; i < gamesToAdd; i++)
            {
                int newId = gamesList.Count > 0 ? gamesList.Max(g => g.Id) + 1 : 1;

                Game game = CreateGame(newId);

                gamesList.Add(game);
                csvService.AddGame(game);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nГру '{game.Name}' успішно додано!");
                Console.ResetColor();
            }

            games = csvService.GetAllGames();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Допоміжний метод для створення об'єкта гри на основі введених користувачем даних.
        /// Виконує валідацію числових полів (рейтинг, ціна).
        /// </summary>
        /// <param name="number">Унікальний ідентифікатор нової гри.</param>
        /// <returns>Створений об'єкт класу Game.</returns>
        private static Game CreateGame(int number)
        {
            Console.Clear();
            Console.WriteLine($"\n=== Гра {number} ===");
            string name = ReadString("Назва: ");
            string genre = ReadString("Жанр: ");

            double rating;
            while (true)
            {
                string ratingInput = ReadString("Рейтинг (0–5): ").Replace(",", ".");
                if (!double.TryParse(ratingInput, out rating) || rating < 0 || rating > 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Введіть число від 0 до 5.");
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            double price;
            while (true)
            {
                string priceInput = ReadString("Ціна: ").Replace(",", ".");
                if (!double.TryParse(priceInput, out price) || price < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Введіть дійсне число для ціни.");
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            return new Game(number, name, genre, rating, (decimal)price);
        }

        /// <summary>
        /// Дозволяє редагувати параметри існуючої гри (назва, жанр, рейтинг, ціна).
        /// Змінює лише ті поля, які користувач вирішив оновити.
        /// </summary>
        private static void EditGame()
        {
            Console.Clear();
            var csvService = new CsvService("CSV");
            var gamesList = csvService.GetAllGames();

            if (gamesList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає для редагування.");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            ShowGamesTable();

            int id = ReadInt("Введіть Id гри для редагування: ", 1, gamesList.Count);
            Game g = gamesList[id - 1];

            string newName = ReadString($"Нова назва ({g.Name}, якщо пусто — без змін): ");
            if (!string.IsNullOrWhiteSpace(newName))
            {
                g.Name = newName;
            }

            string newGenre = ReadString($"Новий жанр ({g.Genre}, якщо пусто — без змін): ");
            if (!string.IsNullOrWhiteSpace(newGenre))
            {
                g.Genre = newGenre;
            }

            while (true)
            {
                string ratingStr = ReadString($"Новий рейтинг ({g.Rating}, 0–5, якщо пусто — без змін): ");
                if (string.IsNullOrWhiteSpace(ratingStr))
                {
                    break;
                }

                ratingStr = ratingStr.Replace(",", ".");
                if (!double.TryParse(ratingStr, out double newRating) || newRating < 0 || newRating > 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Рейтинг має бути числом від 0 до 5.");
                    Console.ResetColor();
                    continue;
                }

                g.Rating = newRating;
                break;
            }

            while (true)
            {
                string priceStr = ReadString($"Нова ціна ({g.Price}, якщо пусто — без змін): ");

                if (string.IsNullOrWhiteSpace(priceStr))
                {
                    break;
                }

                priceStr = priceStr.Replace(",", ".");

                if (!double.TryParse(priceStr, out double newPrice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Ви ввели текст або некоректний формат. Введіть число.");
                    Console.ResetColor();
                    continue;
                }

                if (newPrice < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Ціна не може бути меншою за нуль.");
                    Console.ResetColor();
                    continue;
                }

                g.Price = (decimal)newPrice;
                break;
            }

            gamesList[id - 1] = g;
            csvService.UpdateGame(g);

            games = csvService.GetAllGames();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Гру успішно змінено!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        /// <summary>
        /// Видаляє гру з каталогу за її ідентифікатором.
        /// Виконує переіндексацію (зсув ID) залишених ігор для збереження порядку.
        /// </summary>
        private static void DeleteGame()
        {
            Console.Clear();
            var csvService = new CsvService("CSV");
            var gamesList = csvService.GetAllGames();

            if (gamesList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, видаляти нічого!");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            ShowGamesTable();

            int idToDelete = ReadInt("Введіть ID гри для видалення: ", 1, gamesList.Count);
            gamesList.RemoveAt(idToDelete - 1);

            for (int i = 0; i < gamesList.Count; i++)
            {
                Game g = gamesList[i];
                gamesList[i] = new Game(i + 1, g.Name, g.Genre, g.Rating, g.Price);
            }

            string path = Path.Combine(CsvFolder, "games.csv");
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Id,Name,Genre,Rating,Price");
                foreach (var g in gamesList)
                {
                    string ratingStr = Math.Round(g.Rating, 2).ToString().Replace(",", ".");
                    string priceStr = Math.Round(g.Price, 2).ToString().Replace(",", ".");
                    sw.WriteLine($"{g.Id},{g.Name},{g.Genre},{ratingStr},{priceStr}");
                }
            }

            games = csvService.GetAllGames();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Гру успішно видалено!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        /// <summary>
        /// Виконує пошук гри за частковим співпадінням назви.
        /// </summary>
        private static void SearchGame()
        {
            Console.Clear();
            string search = ReadString("Введіть назву гри для пошуку: ").ToLower();
            bool found = false;

            foreach (var g in games)
            {
                if (g.Name.ToLower().Contains(search))
                {
                    PrintGame(g);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Гру не знайдено.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Сортує список ігор за ціною.
        /// Дозволяє вибрати між вбудованим методом Sort та алгоритмом "бульбашки".
        /// </summary>
        private static void SortGames()
        {
            Console.Clear();
            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, сортувати нічого!");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Оберіть метод сортування:");
            Console.WriteLine("1. Вбудоване сортування за ціною");
            Console.WriteLine("2. Бульбашкове сортування за ціною");
            Console.ResetColor();

            int choice = ReadInt("Введіть номер: ", 1, 2);

            List<Game> sortedGames = new List<Game>(games);

            if (choice == 1)
            {
                sortedGames.Sort((a, b) => a.Price.CompareTo(b.Price));
            }
            else
            {
                for (int i = 0; i < sortedGames.Count - 1; i++)
                {
                    for (int j = 0; j < sortedGames.Count - i - 1; j++)
                    {
                        if (sortedGames[j].Price > sortedGames[j + 1].Price)
                        {
                            var temp = sortedGames[j];
                            sortedGames[j] = sortedGames[j + 1];
                            sortedGames[j + 1] = temp;
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nІгри відсортовано за ціною (за зростанням):\n");
            Console.ResetColor();

            ShowGameList(sortedGames);

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Відображає меню управління базою клієнтів.
        /// </summary>
        private static void ClientsMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("=== Наші клієнти ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Показати всіх клієнтів");
                Console.WriteLine("2. Додати клієнта");
                Console.WriteLine("3. Редагувати клієнта");
                Console.WriteLine("4. Видалити клієнта");
                Console.WriteLine("5. Сортування клієнтів А-Я");
                Console.WriteLine("6. Назад\n");
                Console.ResetColor();

                int choice = ReadInt("Введіть номер дії: ", 1, 6);

                switch (choice)
                {
                    case 1:
                        ShowClients();
                        break;
                    case 2:
                        AddClient();
                        break;
                    case 3:
                        EditClient();
                        break;
                    case 4:
                        DeleteClient();
                        break;
                    case 5:
                        SortClients();
                        break;
                    case 6:
                        back = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Виводить список всіх клієнтів у табличному вигляді.
        /// </summary>
        private static void ShowClients()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Всі клієнти ===\n");
            Console.ResetColor();

            Console.Clear();
            if (clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів ще немає.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-20} {2,-30}", "ID", "Ім'я", "Email");
            Console.WriteLine(new string('-', 60));

            foreach (var c in clients)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-30}", c.Id, c.Name, c.Email);
            }

            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Виводить переданий список клієнтів у спрощеному вигляді.
        /// Використовується для відображення відсортованих списків.
        /// </summary>
        /// <param name="list">Список клієнтів для відображення.</param>
        private static void ShowClients(List<Client> list)
        {
            foreach (var client in list)
            {
                Console.WriteLine($"{client.Id}. {client.Name}, {client.Email}");
            }
        }

        /// <summary>
        /// Очищає екран та виводить повну таблицю всіх клієнтів із заголовками.
        /// Використовується в меню управління клієнтами.
        /// </summary>
        private static void ShowClientsList()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Всі клієнти ===\n");
            Console.ResetColor();
            Console.Clear();
            if (clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів ще немає.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-20} {2,-30}", "ID", "Ім'я", "Email");
            Console.WriteLine(new string('-', 60));

            foreach (var c in clients)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-30}", c.Id, c.Name, c.Email);
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Додає нового клієнта до системи.
        /// Генерує новий ID та зберігає введені ім'я та email.
        /// </summary>
        private static void AddClient()
        {
            Console.Clear();
            var clientsList = csvService.GetAllClients();

            string name = ReadString("Введіть ім'я клієнта: ");
            string email = ReadString("Введіть email клієнта: ");

            int newId = clientsList.Count > 0 ? clientsList.Max(c => c.Id) + 1 : 1;

            Client client = new Client(newId, name, email);
            csvService.AddClient(client);

            clients = csvService.GetAllClients();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nКлієнта додано успішно!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        /// <summary>
        /// Редагує дані обраного клієнта.
        /// </summary>
        private static void EditClient()
        {
            Console.Clear();
            var csvService = new CsvService("CSV");
            var clientsList = csvService.GetAllClients();

            if (clientsList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів немає для редагування.");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            ShowClientsList();

            int id = ReadInt("Введіть Id клієнта для редагування: ", 1, clientsList.Count);
            GameStore.Models.Client c = clientsList[id - 1];

            string name = ReadString($"Нове ім'я ({c.Name}, якщо пусто — без змін): ");
            string email = ReadString($"Новий email ({c.Email}, якщо пусто — без змін): ");

            if (!string.IsNullOrWhiteSpace(name))
            {
                c.Name = name;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                c.Email = email;
            }

            clientsList[id - 1] = c;
            clientsList = csvService.GetAllClients();

            csvService.UpdateClient(c);

            clients = csvService.GetAllClients();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Клієнта успішно змінено!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        /// <summary>
        /// Видаляє клієнта з бази та оновлює файл, зберігаючи послідовність ID.
        /// </summary>
        private static void DeleteClient()
        {
            Console.Clear();
            var csvService = new CsvService("CSV");
            var clientsList = csvService.GetAllClients();

            if (clientsList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів немає, видаляти нічого!");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            ShowClientsList();

            int idToDelete = ReadInt("Введіть ID клієнта для видалення: ", 1, clientsList.Count);
            clientsList.RemoveAt(idToDelete - 1);

            for (int i = 0; i < clientsList.Count; i++)
            {
                GameStore.Models.Client c = clientsList[i];
                clientsList[i] = new Client(i + 1, c.Name, c.Email);
            }

            string path = Path.Combine(CsvFolder, "clients.csv");
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Id,Name,Email");
                foreach (var c in clientsList)
                {
                    sw.WriteLine($"{c.Id},{c.Name},{c.Email}");
                }
            }

            clients = csvService.GetAllClients();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Клієнта успішно видалено!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        /// <summary>
        /// Сортує клієнтів за іменем в алфавітному порядку.
        /// Реалізує вибір алгоритму сортування.
        /// </summary>
        private static void SortClients()
        {
            Console.Clear();

            if (clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів ще немає, сортування неможливе.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Оберіть метод сортування:");
            Console.WriteLine("1. Вбудоване сортування за ім’ям");
            Console.WriteLine("2. Власне сортування бульбашкою за ім’ям");
            Console.ResetColor();
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            List<Client> sortedClients;

            switch (choice)
            {
                case "1":
                    sortedClients = new List<Client>(clients);
                    sortedClients.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
                    Console.WriteLine("\nКлієнти відсортовані за ім’ям (вбудоване сортування):");
                    break;
                case "2":
                    sortedClients = BubbleSortClientsByName(clients);
                    Console.WriteLine("\nКлієнти відсортовані за ім’ям (бульбашка):");
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Повертаємось у меню...");
                    Console.ReadKey(true);
                    return;
            }

            ShowClients(sortedClients);
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Реалізує процес оформлення замовлення.
        /// Дозволяє обирати ігри зі списку, розраховує загальну суму та застосовує випадкову знижку.
        /// </summary>
        private static void Orders()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Замовлення ===\n");
            Console.ResetColor();

            var csvService = new CsvService("CSV");
            List<Game> allGames = csvService.GetAllGames();
            List<Client> allClients = csvService.GetAllClients();
            List<Order> allOrders = csvService.GetAllOrders();

            if (allGames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Зараз ігор в наявності немає!");
                Console.ResetColor();
                Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            ShowGamesTable();

            double total = 0;
            string answer = "так";
            List<Game> currentOrder = new List<Game>();

            while (answer.ToLower() == "так")
            {
                int pick = ReadInt("Введіть номер гри, яку бажаєте придбати: ", 1, allGames.Count);
                Game selected = allGames[pick - 1];

                total += (double)selected.Price;
                currentOrder.Add(selected);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Додано → {selected.Name} ({selected.Price:F2} грн)");
                Console.ResetColor();

                while (true)
                {
                    Console.Write("Бажаєте додати ще щось? (так/ні): ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string input = Console.ReadLine();
                    Console.ResetColor();

                    if (input != null)
                    {
                        input = input.Trim().ToLower();
                    }

                    if (input == "так" || input == "ні")
                    {
                        answer = input;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Помилка! Введіть \"так\" або \"ні\".");
                        Console.ResetColor();
                    }
                }
            }

            if (currentOrder.Count > 0)
            {
                Random randomDiscount = new Random();
                double discount = randomDiscount.Next(5, 16);

                string gameIds = string.Empty;
                for (int i = 0; i < currentOrder.Count; i++)
                {
                    if (i > 0)
                    {
                        gameIds += "|";
                    }

                    gameIds += currentOrder[i].Id.ToString();
                }

                Order newOrder = new Order(currentOrder, discount);
                newOrder.GameIds = gameIds;
                newOrder.TotalPrice = total;
                newOrder.ClientId = 0;

                int maxId = 0;
                foreach (var o in buyHistoryMenu)
                {
                    if (o.Id > maxId)
                    {
                        maxId = o.Id;
                    }
                }

                newOrder.Id = maxId + 1;

                buyHistoryMenu.Add(newOrder);

                csvService.SaveAllOrders(buyHistoryMenu);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nЗагальна сума замовлення: {total:F2} грн");
                Console.WriteLine($"Знижка для вас: {discount}%");
                double finalPrice = total * (1 - (discount / 100.0));
                finalPrice = Math.Round(finalPrice, 2);
                Console.WriteLine($"Загальна сума зі знижкою: {finalPrice:F2} грн");
                Console.ResetColor();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Відображає список всіх здійснених платежів з деталізацією сум.
        /// </summary>
        private static void Payments()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Платежі ===\n");
            Console.ResetColor();

            var csvService = new CsvService("CSV");
            List<Order> allOrders = csvService.GetAllOrders();

            if (allOrders.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Платежів ще немає!\n");
                Console.ResetColor();
                Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double totalSum = 0;
            int orderNum = 1;

            foreach (var order in allOrders)
            {
                double orderSum = order.TotalPrice;

                double finalPrice = orderSum * (1 - (order.Discount / 100.0));
                finalPrice = Math.Round(finalPrice, 2);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Замовлення {orderNum++}: ID товарів → {order.GameIds}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Сума сплачена зі знижкою: {finalPrice:F2} грн\n");
                Console.ResetColor();

                totalSum += finalPrice;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Загальна сума всіх платежів: {totalSum:F2} грн");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Показує детальну історію покупок, включаючи перелік ID товарів та знижки.
        /// </summary>
        private static void ShowBuyHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Історія покупок ===\n");
            Console.ResetColor();

            var csvService = new CsvService("CSV");
            List<Order> allOrders = csvService.GetAllOrders();

            if (allOrders.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Покупок ще не було!\n");
                Console.ResetColor();
            }
            else
            {
                int num = 1;
                foreach (var order in allOrders)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Замовлення {num++}:");
                    Console.ResetColor();

                    double orderSum = order.TotalPrice;
                    Console.WriteLine($"  ID товарів: {order.GameIds}");
                    Console.WriteLine($"  Сума: {orderSum:F2} грн");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  Знижка: {order.Discount}%");
                    double finalPrice = orderSum * (1 - (order.Discount / 100.0));
                    finalPrice = Math.Round(finalPrice, 2);
                    Console.WriteLine($"  Загальна сума зі знижкою: {finalPrice:F2} грн\n");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Відображає таблицю з рейтингами ігор.
        /// Дозволяє швидко переглянути оцінки товарів у магазині.
        /// </summary>
        private static void Ratings()
        {
            Console.Clear();
            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор ще немає, рейтинги відображати неможливо!\n");
                Console.ResetColor();
                Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7}", "ID", "Назва", "Жанр", "Рейтинг");
            Console.WriteLine(new string('-', 60));

            foreach (var g in games)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7:F1}", g.Id, g.Name, g.Genre, g.Rating);
            }

            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Фільтрує список ігор за введеним жанром.
        /// </summary>
        private static void Filters()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Фільтр ігор за жанром ===\n");
            Console.ResetColor();

            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор ще немає, фільтрувати нічого!\n");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            string genre = ReadString("Введіть жанр для пошуку: ").ToLower();
            bool found = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nРезультати пошуку:\n");
            Console.ResetColor();

            foreach (var g in games)
            {
                if (g.Genre.ToLower() == genre)
                {
                    PrintGame(g);
                    found = true;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор з таким жанром не знайдено.");
                Console.ResetColor();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Обчислює статистичні показники по асортименту ігор.
        /// </summary>
        private static void Statistics()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Статистика ігор ===\n");
            Console.ResetColor();

            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор наразі немає, статистику обчислити неможливо.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double totalPrice = 0;
            decimal minPrice = decimal.MaxValue;
            decimal maxPrice = decimal.MinValue;
            int countExpensive = 0;
            decimal threshold = 500;

            foreach (var g in games)
            {
                totalPrice += (double)g.Price;
                if (g.Price < minPrice)
                {
                    minPrice = g.Price;
                }

                if (g.Price > maxPrice)
                {
                    maxPrice = g.Price;
                }

                if (g.Price > threshold)
                {
                    countExpensive++;
                }
            }

            double averagePrice = totalPrice / games.Count;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Кількість ігор: {games.Count}");
            Console.WriteLine($"Загальна сума всіх ігор: {totalPrice:F2} грн");
            Console.WriteLine($"Середня ціна гри: {averagePrice:F2} грн");
            Console.WriteLine($"Кількість ігор з ціною > {threshold:F2} грн: {countExpensive}");
            Console.WriteLine($"Мінімальна ціна гри: {minPrice:F2} грн");
            Console.WriteLine($"Максимальна ціна гри: {maxPrice:F2} грн");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Формує та виводить на екран загальний звіт про діяльність магазину.
        /// Включає статистику цін (мінімальна, максимальна, середня).
        /// </summary>
        private static void Report()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Звіт по іграх ===\n");
            Console.ResetColor();

            if (games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, звіт сформувати неможливо.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double total = 0;
            decimal minPrice = decimal.MaxValue;
            decimal maxPrice = decimal.MinValue;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7} {4,-8}", "Id", "Назва", "Жанр", "Рейтинг", "Ціна");
            Console.WriteLine(new string('-', 65));

            foreach (var g in games)
            {
                PrintGameReport(g);
                total += (double)g.Price;
                if (g.Price < minPrice)
                {
                    minPrice = g.Price;
                }

                if (g.Price > maxPrice)
                {
                    maxPrice = g.Price;
                }
            }

            double average = total / games.Count;

            Console.WriteLine(new string('-', 65));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Підсумки:");
            Console.WriteLine($"Кількість ігор: {games.Count}");
            Console.WriteLine($"Загальна сума: {total:F2} грн");
            Console.WriteLine($"Середня ціна: {average:F2} грн");
            Console.WriteLine($"Мінімальна ціна: {minPrice:F2} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice:F2} грн");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Безпечно зчитує ціле число з консолі в заданому діапазоні.
        /// </summary>
        /// <param name="message">Повідомлення для користувача.</param>
        /// <param name="min">Мінімальне допустиме значення.</param>
        /// <param name="max">Максимальне допустиме значення.</param>
        /// <returns>Введене коректне число.</returns>
        private static int ReadInt(string message, int min, int max)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine().Replace(",", ".");
                if (!int.TryParse(input, out result))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Введіть число.");
                    Console.ResetColor();
                    continue;
                }

                if (result < min || result > max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Помилка! Введіть число від {min} до {max}.");
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            return result;
        }

        /// <summary>
        /// Зчитує рядкове значення з консолі, попередньо вивівши повідомлення.
        /// </summary>
        /// <param name="message">Текст запрошення для користувача.</param>
        /// <returns>Введений користувачем рядок.</returns>
        private static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// Виводить інформацію про одну гру в рядок з роздільниками.
        /// Використовується при пошуку та фільтрації.
        /// </summary>
        /// <param name="g">Об'єкт гри для виведення.</param>
        private static void PrintGame(Game g)
        {
            Console.WriteLine($"Id: {g.Id} | Назва: {g.Name} | Жанр: {g.Genre} | Рейтинг: {g.Rating} | Ціна: {g.Price:F2} грн");
        }

        /// <summary>
        /// Виводить інформацію про одного клієнта в рядок.
        /// </summary>
        /// <param name="c">Об'єкт клієнта.</param>
        private static void PrintClient(Client c)
        {
            Console.WriteLine($"Id: {c.Id} | Ім'я: {c.Name} | Email: {c.Email}");
        }

        /// <summary>
        /// Виводить форматований рядок таблиці для звіту про гру.
        /// Використовується в методі Report для генерації фінальної таблиці.
        /// </summary>
        /// <param name="g">Об'єкт гри.</param>
        private static void PrintGameReport(Game g)
        {
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7:F1} {4,-8:F2}", g.Id, g.Name, g.Genre, g.Rating, g.Price);
        }

        /// <summary>
        /// Реалізує алгоритм сортування бульбашкою для списку ігор.
        /// Сортує ігри за зростанням ціни.
        /// </summary>
        /// <param name="list">Вхідний список ігор.</param>
        /// <returns>Новий відсортований список ігор.</returns>
        private static List<Game> BubbleSortGamesByPrice(List<Game> list)
        {
            List<Game> sortedList = new List<Game>(list);

            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = 0; j < sortedList.Count - i - 1; j++)
                {
                    if (sortedList[j].Price > sortedList[j + 1].Price)
                    {
                        Game temp = sortedList[j];
                        sortedList[j] = sortedList[j + 1];
                        sortedList[j + 1] = temp;
                    }
                }
            }

            return sortedList;
        }

        /// <summary>
        /// Реалізує алгоритм сортування бульбашкою для списку клієнтів.
        /// Сортує клієнтів за іменем в алфавітному порядку.
        /// </summary>
        /// <param name="list">Вхідний список клієнтів.</param>
        /// <returns>Новий відсортований список клієнтів.</returns>
        private static List<Client> BubbleSortClientsByName(List<Client> list)
        {
            List<Client> sortedList = new List<Client>(list);

            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = 0; j < sortedList.Count - i - 1; j++)
                {
                    if (string.Compare(sortedList[j].Name, sortedList[j + 1].Name) > 0)
                    {
                        Client temp = sortedList[j];
                        sortedList[j] = sortedList[j + 1];
                        sortedList[j + 1] = temp;
                    }
                }
            }

            return sortedList;
        }

        /// <summary>
        /// Здійснює процедуру входу користувача в систему.
        /// Перевіряє відповідність введеного email та пароля збереженим даним.
        /// </summary>
        /// <param name="attempts">Кількість доступних спроб для введення пароля.</param>
        /// <returns>True, якщо вхід успішний; інакше False.</returns>
        private static bool LoginUser(int attempts)
        {
            do
            {
                Console.Write("\nВведіть email: ");
                string email = Console.ReadLine();

                Console.Write("Введіть пароль: ");
                string password = Console.ReadLine();

                User user = csvService.GetUserByEmail(email);

                if (user != null && password == user.PasswordHash)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nВхід успішний!\n");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nНевірний email або пароль. Залишилось спроб: {attempts}\n");
                    Console.ResetColor();
                }
            }
            while (attempts > 0);

            return false;
        }

        /// <summary>
        /// Реєструє нового користувача в системі.
        /// Перевіряє унікальність email та співпадіння паролів при введенні.
        /// </summary>
        /// <param name="attempts">Кількість спроб для підтвердження пароля.</param>
        /// <returns>Результат автоматичного входу після успішної реєстрації.</returns>
        private static bool RegisterUser(int attempts)
        {
            Console.Clear();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Реєстрація нового користувача ===\n");
            Console.ResetColor();

            string email = string.Empty;
            bool emailValid = false;

            while (!emailValid)
            {
                email = ReadString("Введіть email: ");

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Email не може бути порожнім!");
                    Console.ResetColor();
                    continue;
                }

                if (csvService.UserExists(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Користувач з таким email вже існує!");
                    Console.ResetColor();
                    continue;
                }

                emailValid = true;
            }

            string password = ReadString("Введіть пароль: ");

            int confirmAttempts = 3;
            string passwordConfirm = string.Empty;
            bool passwordMatches = false;

            while (confirmAttempts > 0 && !passwordMatches)
            {
                passwordConfirm = ReadString("Підтвердіть пароль: ");

                if (password == passwordConfirm)
                {
                    passwordMatches = true;
                }
                else
                {
                    confirmAttempts--;
                    if (confirmAttempts > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Паролі не збігаються! Спроб залишилось: {confirmAttempts}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вичерпано всі спроби підтвердження пароля. Програма завершує роботу.");
                        Console.ResetColor();
                        Environment.Exit(0);
                    }
                }
            }

            var users = csvService.GetAllUsers();
            int newId = users.Count > 0 ? users[users.Count - 1].Id + 1 : 1;

            User newUser = new User(newId, email, password);

            csvService.AddUser(newUser);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nРеєстрація успішна! Тепер увійдіть у свій акаунт.\n");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити вхід...");
            Console.ReadKey(true);
            Console.Clear();

            return LoginUser(attempts);
        }
    }
}

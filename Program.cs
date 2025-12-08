using System;
using System.Collections.Generic;
using GameStore.Models;

namespace GameStore
{
    class Program
    {
        private static List<Game> Games = new List<Game>();
        private static List<Client> Clients = new List<Client>();
        private static List<Order> BuyHistoryMenu = new List<Order>();

        // private static int nextClientId = 1;

        static void Main()
        {
            string correctLogin = "admin";
            string correctPassword = "123123";
            int attempts = 3;
            bool loggedIn = false;

            do
            {
                Console.Write("Введіть логін: ");
                string login = Console.ReadLine();

                Console.Write("Введіть пароль: ");
                string password = Console.ReadLine();

                if (login == correctLogin && password == correctPassword)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nВхід успішний!\n");
                    Console.ResetColor();
                    loggedIn = true;
                    break;
                }
                else
                {
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nНевірний логін або пароль. Залишилось спроб: {attempts}\n");
                    Console.ResetColor();
                }
            } while (attempts > 0);

            if (!loggedIn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Спроби вичерпано. Програма завершує роботу.");
                Console.ResetColor();
                Environment.Exit(0);
            }

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

                int choice = ReadInt("Введіть номер бажаної категорії: ", 0, 9);

                switch (choice)
                {
                    case 1: GameList(); break;
                    case 2: ClientsMenu(); break;
                    case 3: Orders(); break;
                    case 4: Payments(); break;
                    case 5: Ratings(); break;
                    case 6: Filters(); break;
                    case 7: ShowBuyHistory(); break;
                    case 8: Statistics(); break;
                    case 9: Report(); break;
                    case 0:
                        Console.WriteLine("Вихід з магазину...");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
        }

        static void GameList()
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


        static void ShowGameList()
        {
            Console.Clear();
            if (Games.Count == 0)
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

            foreach (var g in Games)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7:F1} {4,-8:F2}", g.Id, g.Name, g.Genre, g.Rating, g.Price);
            }
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        static void ShowGameList(List<Game> list)
        {
            foreach (var game in list)
            {
                Console.WriteLine($"{game.Id}. {game.Name} — {game.Price} грн");
            }
        }


        static void ShowGamesTable()
        {
            Console.Clear();

            if (Games.Count == 0)
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

            foreach (var g in Games)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7:F1} {4,-8:F2}",
                    g.Id, g.Name, g.Genre, g.Rating, g.Price);
            }
        }



        // static void ShowGameListExit()
        // {
        //     ShowGameList();
        //     Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
        //     Console.ReadKey(true);
        // }

        static void AddGames()
        {
            Console.Clear();
            if (Games.Count >= 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Каталог вже містить 5 ігор. Додати нову поки не можна.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            int gamesToAdd = 5 - Games.Count;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Ви можете додати ще стільки ігор: {gamesToAdd}");
            Console.ResetColor();

            for (int i = 0; i < gamesToAdd; i++)
            {
                Games.Add(CreateGame(Games.Count + 1));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ігри успішно додано!");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }



        static Game CreateGame(int number)
        {
            Console.Clear();
            Console.WriteLine($"\n=== Гра {number} ===");
            string name = ReadString("Назва: ");
            string genre = ReadString("Жанр: ");

            double rating;
            while (true)
            {
                if (!double.TryParse(ReadString("Рейтинг (0–5): "), out rating) || rating < 0 || rating > 5)
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
                if (!double.TryParse(ReadString("Ціна: "), out price) || price < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Введіть дійсне число для ціни.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            return new Game(number, name, genre, rating, price);
        }

        static void EditGame()
        {
            Console.Clear();
            if (Games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає для редагування.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            ShowGamesTable();

            int id = ReadInt("Введіть Id гри для редагування: ", 1, Games.Count);
            Game g = Games[id - 1];

            // Назва та жанр — можна пропустити (натискання Enter)
            string name = ReadString($"Нова назва ({g.Name}, якщо пусто - без змін): ");
            if (!string.IsNullOrWhiteSpace(name))
                g.Name = name;

            string genre = ReadString($"Новий жанр ({g.Genre}, якщо пусто - без змін): ");
            if (!string.IsNullOrWhiteSpace(genre))
                g.Genre = genre;

            // Рейтинг: якщо пусто — без змін; інакше перевіряємо валідність вводу
            while (true)
            {
                string ratingStr = ReadString($"Новий рейтинг ({g.Rating}, 0–5, якщо пусто - без змін): ");
                if (string.IsNullOrWhiteSpace(ratingStr))
                {
                    // користувач пропустив зміну рейтингу
                    break;
                }

                if (!double.TryParse(ratingStr, out double newRating))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Рейтинг має бути числом. Спробуйте ще раз або натисніть Enter, щоб пропустити.");
                    Console.ResetColor();
                    continue;
                }

                if (newRating < 0 || newRating > 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Рейтинг повинен бути в діапазоні 0–5.");
                    Console.ResetColor();
                    continue;
                }

                g.Rating = newRating;
                break;
            }

            // Ціна: якщо пусто — без змін; інакше перевіряємо валідність вводу
            while (true)
            {
                string priceStr = ReadString($"Нова ціна ({g.Price}, якщо пусто - без змін): ");
                if (string.IsNullOrWhiteSpace(priceStr))
                {
                    // користувач пропустив зміну ціни
                    break;
                }

                if (!double.TryParse(priceStr, out double newPrice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Ціна має бути числом. Спробуйте ще раз або натисніть Enter, щоб пропустити.");
                    Console.ResetColor();
                    continue;
                }

                if (newPrice < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Ціна не може бути від'ємною.");
                    Console.ResetColor();
                    continue;
                }

                g.Price = newPrice;
                break;
            }

            // Оскільки Game — struct, потрібно записати назад в список
            Games[id - 1] = g;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Гру успішно змінено!");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }



        static void DeleteGame()
        {
            Console.Clear();
            if (Games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, видаляти нічого!");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            ShowGamesTable();

            int idToDelete = ReadInt("Введіть ID гри для видалення: ", 1, Games.Count);

            Games.RemoveAt(idToDelete - 1);

            for (int i = 0; i < Games.Count; i++)
            {
                Game g = Games[i];
                Games[i] = new Game(i + 1, g.Name, g.Genre, g.Rating, g.Price);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Гру успішно видалено!");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }


        static void SearchGame()
        {
            Console.Clear();
            string search = ReadString("Введіть назву гри для пошуку: ").ToLower();
            bool found = false;

            foreach (var g in Games)
            {
                if (g.Name.ToLower().Contains(search))
                {
                    PrintGame(g);
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Гру не знайдено.");

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        static void SortGames()
        {
            Console.Clear(); // очищаємо консоль для чистого вигляду
            if (Games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, сортувати нічого!");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            // Вибір сортування
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Оберіть метод сортування:");
            Console.WriteLine("1. Вбудоване сортування за ціною");
            Console.WriteLine("2. Бульбашкове сортування за ціною");
            Console.ResetColor();

            int choice = ReadInt("Введіть номер: ", 1, 2);

            List<Game> sortedGames = new List<Game>(Games);

            if (choice == 1)
            {
                sortedGames.Sort((a, b) => a.Price.CompareTo(b.Price)); // вбудоване сортування
            }
            else
            {
                // Бульбашкове сортування за ціною
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

        static void ClientsMenu()
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
                        ShowClients(); // показує список клієнтів і лишає користувача в меню
                        break;
                    case 2:
                        AddClient(); // додає клієнта і лишає користувача в меню
                        break;
                    case 3:
                        EditClient(); // редагування
                        break;
                    case 4:
                        DeleteClient(); // видалення
                        break;
                    case 5:
                        SortClients(); // сортування
                        break;
                    case 6:
                        back = true; // вихід із підменю
                        break;
                }
            }
        }




        static void AddClient()
        {
            Console.Clear();
            string name = ReadString("Введіть ім'я клієнта: ");
            string email = ReadString("Введіть email клієнта: ");

            Clients.Add(new Client(Clients.Count + 1, name, email)); // новий клієнт останній

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nКлієнта додано успішно!");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        static void ShowClients()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Всі клієнти ===\n");
            Console.ResetColor();

            Console.Clear();
            if (Clients.Count == 0)
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

            foreach (var c in Clients)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-30}", c.Id, c.Name, c.Email);
            }
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        static void ShowClients(List<Client> list)
        {
            foreach (var client in list)
            {
                Console.WriteLine($"{client.Id}. {client.Name}, {client.Email}");
            }
        }


        static void ShowClientsList()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Всі клієнти ===\n");
            Console.ResetColor();
            Console.Clear();
            if (Clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів ще немає.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-20} {2,-30}", "ID", "Ім'я", "Email");
            Console.WriteLine(new string('-', 60));

            foreach (var c in Clients)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-30}", c.Id, c.Name, c.Email);
            }
            Console.ResetColor();
        }

        static void EditClient()
        {
            Console.Clear();
            if (Clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів немає для редагування.");
                Console.ResetColor();
                // Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            ShowClientsList(); // список без паузи

            int id = ReadInt("Введіть Id клієнта для редагування: ", 1, Clients.Count);
            Client c = Clients[id - 1];

            string name = ReadString($"Нове ім'я ({c.Name}, якщо пусто — без змін): ");
            string email = ReadString($"Новий email ({c.Email}, якщо пусто — без змін): ");

            if (!string.IsNullOrWhiteSpace(name))
                c.Name = name;

            if (!string.IsNullOrWhiteSpace(email))
                c.Email = email;

            // ⬇⬇⬇ ДОДАТИ СЮДИ!
            Clients[id - 1] = c;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Клієнта успішно змінено!");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);

        }


        static void DeleteClient()
        {
            Console.Clear();
            if (Clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів немає, видаляти нічого!");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            ShowClientsList(); // список без паузи

            int idToDelete = ReadInt("Введіть ID клієнта для видалення: ", 1, Clients.Count);

            Clients.RemoveAt(idToDelete - 1);

            // Перегенеруємо ID
            for (int i = 0; i < Clients.Count; i++)
            {
                Client c = Clients[i];
                Clients[i] = new Client(i + 1, c.Name, c.Email);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Клієнта успішно видалено!");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }


        static void SortClients()
        {
            Console.Clear();

            if (Clients.Count == 0)
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
                    sortedClients = Clients.OrderBy(c => c.Name).ToList();
                    Console.WriteLine("\nКлієнти відсортовані за ім’ям (вбудоване сортування):");
                    break;
                case "2":
                    sortedClients = BubbleSortClientsByName(Clients);
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







        static void Orders()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Замовлення ===\n");
            Console.ResetColor();

            if (Games.Count == 0)
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

            while (answer == "так")
            {
                int pick = ReadInt("Введіть номер гри, яку бажаєте придбати: ", 1, Games.Count);
                Game selected = Games[pick - 1];

                total += selected.Price;
                currentOrder.Add(selected);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Додано → {selected.Name} ({selected.Price:F2} грн)");
                Console.ResetColor();

                Console.Write("Бажаєте додати ще щось? (так/ні): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                answer = Console.ReadLine();
                if (answer == null) answer = "ні";
                Console.ResetColor();
            }

            if (currentOrder.Count > 0)
            {
                Random randomDiscount = new Random();
                double discount = randomDiscount.Next(5, 16); // від 5% до 15%
                BuyHistoryMenu.Add(new Order(currentOrder, discount));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nЗагальна сума замовлення: {total:F2} грн");
                Console.WriteLine($"Знижка для вас: {discount}%");
                double finalPrice = total * (1 - discount / 100.0);
                finalPrice = Math.Round(finalPrice, 2);
                Console.WriteLine($"Загальна сума зі знижкою: {finalPrice:F2} грн");
                Console.ResetColor();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);
        }



        static void Payments()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Платежі ===\n");
            Console.ResetColor();

            if (BuyHistoryMenu.Count == 0)
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

            foreach (var order in BuyHistoryMenu)
            {
                double orderSum = 0;
                foreach (var game in order.Games)
                {
                    orderSum += game.Price;
                }

                double finalPrice = orderSum * (1 - order.Discount / 100.0);
                finalPrice = Math.Round(finalPrice, 2);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Замовлення {orderNum++}: ID товарів → {string.Join(", ", order.Games.ConvertAll(g => g.Id.ToString()))}");
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



        static void ShowBuyHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Історія покупок ===\n");
            Console.ResetColor();

            if (BuyHistoryMenu.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Покупок ще не було!\n");
                Console.ResetColor();
            }
            else
            {
                int num = 1;
                foreach (var order in BuyHistoryMenu)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Замовлення {num++}:");
                    Console.ResetColor();

                    double orderSum = 0;
                    foreach (var g in order.Games)
                    {
                        Console.WriteLine($"  Id: {g.Id}, Назва: {g.Name}, Жанр: {g.Genre}, Ціна: {g.Price:F2} грн");
                        orderSum += g.Price;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Знижка: {order.Discount}%");
                    double finalPrice = orderSum * (1 - order.Discount / 100.0);
                    finalPrice = Math.Round(finalPrice, 2);
                    Console.WriteLine($"Загальна сума зі знижкою: {finalPrice:F2} грн\n");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }


        static void Ratings()
        {
            Console.Clear();
            if (Games.Count == 0)
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

            foreach (var g in Games)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7:F1}", g.Id, g.Name, g.Genre, g.Rating);
            }
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }


        static void Filters()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Фільтр ігор за жанром ===\n");
            Console.ResetColor();

            if (Games.Count == 0)
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

            foreach (var g in Games)
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

        static void Statistics()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Статистика ігор ===\n");
            Console.ResetColor();

            if (Games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор наразі немає, статистику обчислити неможливо.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double totalPrice = 0;
            double minPrice = double.MaxValue;
            double maxPrice = double.MinValue;
            int countExpensive = 0;
            double threshold = 500;

            foreach (var g in Games)
            {
                totalPrice += g.Price;
                if (g.Price < minPrice) minPrice = g.Price;
                if (g.Price > maxPrice) maxPrice = g.Price;
                if (g.Price > threshold) countExpensive++;
            }

            double averagePrice = totalPrice / Games.Count;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Кількість ігор: {Games.Count}");
            Console.WriteLine($"Загальна сума всіх ігор: {totalPrice:F2} грн");
            Console.WriteLine($"Середня ціна гри: {averagePrice:F2} грн");
            Console.WriteLine($"Кількість ігор з ціною > {threshold:F2} грн: {countExpensive}");
            Console.WriteLine($"Мінімальна ціна гри: {minPrice:F2} грн");
            Console.WriteLine($"Максимальна ціна гри: {maxPrice:F2} грн");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }


        static void Report()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Звіт по іграх ===\n");
            Console.ResetColor();

            if (Games.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, звіт сформувати неможливо.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double total = 0;
            double minPrice = double.MaxValue;
            double maxPrice = double.MinValue;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7} {4,-8}", "Id", "Назва", "Жанр", "Рейтинг", "Ціна");
            Console.WriteLine(new string('-', 65));

            foreach (var g in Games)
            {
                PrintGameReport(g);
                total += g.Price;
                if (g.Price < minPrice) minPrice = g.Price;
                if (g.Price > maxPrice) maxPrice = g.Price;
            }

            double average = total / Games.Count;

            Console.WriteLine(new string('-', 65));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Підсумки:");
            Console.WriteLine($"Кількість ігор: {Games.Count}");
            Console.WriteLine($"Загальна сума: {total:F2} грн");
            Console.WriteLine($"Середня ціна: {average:F2} грн");
            Console.WriteLine($"Мінімальна ціна: {minPrice:F2} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice:F2} грн");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }



        static int ReadInt(string message, int min, int max)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
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

        static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static void PrintGame(Game g)
        {
            Console.WriteLine($"Id: {g.Id} | Назва: {g.Name} | Жанр: {g.Genre} | Рейтинг: {g.Rating} | Ціна: {g.Price:F2} грн");
        }

        static void PrintClient(Client c)
        {
            Console.WriteLine($"Id: {c.Id} | Ім'я: {c.Name} | Email: {c.Email}");
        }

        static void PrintGameReport(Game g)
        {
            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-7:F1} {4,-8:F2}", g.Id, g.Name, g.Genre, g.Rating, g.Price);
        }

        static List<Game> BubbleSortGamesByPrice(List<Game> list)
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

        static List<Client> BubbleSortClientsByName(List<Client> list)
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

    }
}
class Order
{
    public List<Game> Games; // список ігор у замовленні
    public double Discount;  // знижка у відсотках

    public Order(List<Game> games, double discount)
    {
        Games = games;
        Discount = discount;
    }
}

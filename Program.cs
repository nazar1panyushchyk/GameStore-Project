using System;
using System.Text.Encodings.Web;
using GameStore.Models;

namespace GameStore
{
    class Program
    {
        private static Game g1, g2, g3, g4, g5;
        private static Client c1 = new Client();
        private static Client c2 = new Client();
        private static Client c3 = new Client();
        private static Client c4 = new Client();
        private static Client c5 = new Client();
        private static int nextClientId = 1;
        // private static int nextId = 1;
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

                // int choice;
                // while (true)
                // {
                //     Console.Write("Введіть номер бажаної категорії: ");
                //     Console.ForegroundColor = ConsoleColor.Cyan;
                //     string input = Console.ReadLine();
                //     Console.ResetColor();

                //     try
                //     {
                //         choice = Convert.ToInt32(input);
                //         break;
                //     }
                //     catch
                //     {
                //         Console.WriteLine("Помилка! Введіть число від 1 до 8.");
                //     }
                // }

                switch (choice)
                {
                    case 1:
                        GameList();
                        break;
                    case 2:
                        Clients();
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
                        BuyHistory();
                        break;
                    case 8:
                        Statistics();
                        break;
                    case 9:
                        Report();
                        break;
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Каталог ігор ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Переглянути всі ігри");
            Console.WriteLine("2. Додати нову гру");
            Console.WriteLine("3. Редагувати гру");
            Console.WriteLine("4. Видалити гру");
            Console.WriteLine("5. Пошук гри");
            Console.WriteLine("6. Назад\n");
            Console.ResetColor();

            int choice = ReadInt("Введіть номер дії: ", 1, 6);


            // int choice;
            // while (true)
            // {
            //     Console.Write("Введіть номер дії: ");
            //     Console.ForegroundColor = ConsoleColor.Cyan;
            //     string input = Console.ReadLine();
            //     Console.ResetColor();

            //     try
            //     {
            //         choice = Convert.ToInt32(input);
            //         break;
            //     }
            //     catch
            //     {
            //         Console.WriteLine("Помилка! Введіть число від 1 до 6.");
            //     }
            // }

            switch (choice)
            {
                case 1:
                    ShowGameListExit();
                    break;
                case 2:
                    AddGames();
                    break;
                case 3:
                case 4:
                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nФункція в розробці...\n");
                    Console.ResetColor();
                    Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
                    Console.ReadKey(true);
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }

        static void ShowGameList()
        {

            if (g1.IsEmpty() && g2.IsEmpty() && g3.IsEmpty() && g4.IsEmpty() && g5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор наразі не додано!");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (!g1.IsEmpty()) PrintGame(g1);
                if (!g2.IsEmpty()) PrintGame(g2);
                if (!g3.IsEmpty()) PrintGame(g3);
                if (!g4.IsEmpty()) PrintGame(g4);
                if (!g5.IsEmpty()) PrintGame(g5);
                Console.ResetColor();
            }

            // Console.Write("Повернутися до початкового меню? (так): ");
            // Console.ForegroundColor = ConsoleColor.Cyan;
            // string choice = Console.ReadLine();
            // Console.ResetColor();


            // if (choice == "так")
            // {
            //     return;
            // }
            // else
            // {
            //     Console.WriteLine("Ви ввели щось незрозуміле!");
            //     return;
            // }
        }

        static void ShowGameListExit()
        {
            ShowGameList();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);
        }

        static void PrintGame(Game g)
        {
            Console.WriteLine($"Id: {g.Id}, Назва: {g.Name}, Жанр: {g.Genre}, Рейтинг: {g.Rating}, Ціна: {g.Price} грн");
        }

        static void AddGames()
        {
            if (!g1.IsEmpty() || !g2.IsEmpty() || !g3.IsEmpty() || !g4.IsEmpty() || !g5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігри вже були додані — змінити або додати нові не можна (мінімум = максимум = 5).");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Потрібно додати рівно 5 ігор.");
            Console.ResetColor();

            g1 = CreateGame(1);
            g2 = CreateGame(2);
            g3 = CreateGame(3);
            g4 = CreateGame(4);
            g5 = CreateGame(5);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nУспіх! Додано 5 ігор у каталог.");
            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);
        }

        static Game CreateGame(int number)
        {
            Console.WriteLine($"\n=== Гра {number} ===");

            Console.Write("Назва: ");
            string name = Console.ReadLine();

            Console.Write("Жанр: ");
            string genre = Console.ReadLine();

            double rating;
            while (true)
            {
                Console.Write("Рейтинг (0–5): ");
                try
                {
                    rating = Convert.ToDouble(Console.ReadLine());
                    if (rating >= 0 && rating <= 5) break;
                    else Console.WriteLine("Помилка! Введіть число від 0 до 5.");
                }
                catch
                {
                    Console.WriteLine("Помилка! Введіть число від 0 до 5.");
                }
            }

            double price;
            while (true)
            {
                Console.Write("Ціна: ");
                try
                {
                    price = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Помилка! Введіть число.");
                }
            }

            return new Game(number, name, genre, rating, price);
        }


        static void Clients()
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
            Console.WriteLine("5. Назад\n");
            Console.ResetColor();

            int choice = ReadInt("Введіть номер дії: ", 1, 5);

            // int choice;
            // while (true)
            // {
            //     Console.Write("Введіть номер дії: ");
            //     if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 5)
            //         break;
            //     Console.WriteLine("Помилка! Введіть число від 1 до 5.");
            // }

            switch (choice)
            {
                case 1:
                    ShowClients();
                    break;
                case 2:
                    AddClient();
                    break;
                case 3:
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nФункція в розробці...\n");
                    Console.ResetColor();
                    Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
                    Console.ReadKey(true);
                    break;
                case 5:
                    return;
            }
        }

        static void ShowClients()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Список клієнтів ===\n");
            Console.ResetColor();

            if (c1.IsEmpty() && c2.IsEmpty() && c3.IsEmpty() && c4.IsEmpty() && c5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Клієнтів ще немає!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (!c1.IsEmpty()) PrintClient(c1);
                if (!c2.IsEmpty()) PrintClient(c2);
                if (!c3.IsEmpty()) PrintClient(c3);
                if (!c4.IsEmpty()) PrintClient(c4);
                if (!c5.IsEmpty()) PrintClient(c5);
                Console.ResetColor();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }


        static void PrintClient(Client client)
        {
            Console.WriteLine($"Id: {client.Id}, Ім'я: {client.Name}, Email: {client.Email}");
        }

        static void AddClient()
        {
            if (!c1.IsEmpty() && !c2.IsEmpty() && !c3.IsEmpty() && !c4.IsEmpty() && !c5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Досягнуто максимуму клієнтів (5). Додати нового не можна.");
                Console.ResetColor();
                Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.Write("Введіть ім'я клієнта: ");
            string name = Console.ReadLine();

            Console.Write("Введіть email клієнта: ");
            string email = Console.ReadLine();

            Client newClient = new Client(nextClientId++, name, email);

            if (c1.IsEmpty()) c1 = newClient;
            else if (c2.IsEmpty()) c2 = newClient;
            else if (c3.IsEmpty()) c3 = newClient;
            else if (c4.IsEmpty()) c4 = newClient;
            else if (c5.IsEmpty()) c5 = newClient;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nКлієнта додано успішно!");
            Console.ResetColor();
            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ReadKey(true);
        }

        static void Orders()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Замовлення ===\n");
            Console.ResetColor();

            if (g1.IsEmpty() && g2.IsEmpty() && g3.IsEmpty() && g4.IsEmpty() && g5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Зараз ігор в наявності немає!");
                Console.ResetColor();
                Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!g1.IsEmpty()) PrintGame(g1);
            if (!g2.IsEmpty()) PrintGame(g2);
            if (!g3.IsEmpty()) PrintGame(g3);
            if (!g4.IsEmpty()) PrintGame(g4);
            if (!g5.IsEmpty()) PrintGame(g5);
            Console.ResetColor();

            double total = 0;
            string answer = "так";

            while (answer == "так")
            {
                int pick = ReadInt("Введіть номер гри, яку бажаєте придбати: ", 1, 5);

                // int pick;

                // while (true)
                // {
                //     Console.Write("Введіть номер гри, яку бажаєте придбати: ");
                //     Console.ForegroundColor = ConsoleColor.Cyan;
                //     string input = Console.ReadLine();
                //     Console.ResetColor();

                //     try
                //     {
                //         pick = Convert.ToInt32(input);
                //         break;
                //     }
                //     catch
                //     {
                //         Console.WriteLine("Помилка! Введіть число від 1 до 5.");
                //     }
                // }

                Game selected;

                if (pick == 1)
                    selected = g1;
                else if (pick == 2)
                    selected = g2;
                else if (pick == 3)
                    selected = g3;
                else if (pick == 4)
                    selected = g4;
                else if (pick == 5)
                    selected = g5;
                else
                {
                    Console.WriteLine("Такої гри немає!");
                    selected = new Game();
                }

                if (!selected.IsEmpty())
                {
                    total += selected.Price;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Додано → {selected.Name} ({selected.Price} грн)");
                    Console.ResetColor();
                }

                Console.Write("Бажаєте додати ще щось? (так/ні): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                answer = Console.ReadLine();
                if (answer == null)
                    answer = "ні";
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Загальна сума: {total:F2} грн");

            if (total > 0)
            {
                Random randomDiscount = new Random();
                double discount = randomDiscount.Next(5, 16) / 100.0;
                Console.WriteLine($"Знижка для Вас: {Math.Round(discount * 100)}%");

                double finalPrice = total * (1 - discount);
                finalPrice = Math.Round(finalPrice, 2);
                Console.WriteLine($"Сума до оплати: {finalPrice:F2} грн");
            }
            Console.ResetColor();

            // Console.Write("Повернутися до початкового меню? (так): ");
            // Console.ForegroundColor = ConsoleColor.Cyan;
            // string choice = Console.ReadLine();
            // Console.ResetColor();
            // Console.ForegroundColor = ConsoleColor.DarkGray;
            // Console.WriteLine("============================================\n");
            // Console.ResetColor();

            // if (choice == "так")
            // {
            //     return;
            // }
            // else
            // {
            //     Console.WriteLine("Ви ввели щось незрозуміле!");
            // }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);
        }


        static void Payments()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Платежі ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Функція в розробці...\n");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);

            // Console.Write("Повернутися до початкового меню? (так): ");
            // Console.ForegroundColor = ConsoleColor.Cyan;
            // string choice = Console.ReadLine();
            // Console.ResetColor();


            // if (choice == "так")
            // {
            //     return;
            // }
            // else
            // {
            //     Console.WriteLine("Ви ввели щось незрозуміле!");
            //     return;
            // }

        }
        static void Ratings()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Рейтинги ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("1. Minecraft - ⭐ 5,0/5");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. Cyberpunk 2077 - ⭐ 4,5/5");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("3. Far Cry 3 - ⭐ 4,7/5");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("4. Stalker 2 - ⭐ 3,9/5");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("5. GTA V - ⭐ 4,9/5");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("6. The Forest - ⭐ 5,0/5\n");
            Console.ResetColor();

            // Console.Write("Повернутися до початкового меню? (так): ");
            // Console.ForegroundColor = ConsoleColor.Cyan;
            // string choice = Console.ReadLine();
            // Console.ResetColor();
            // Console.ForegroundColor = ConsoleColor.DarkGray;
            // Console.WriteLine("============================================\n");
            // Console.ResetColor();

            // if (choice == "так")
            // {
            //     return;
            // }
            // else
            // {
            //     Console.WriteLine("Ви ввели щось незрозуміле!");
            // }
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...\n");
            Console.ReadKey(true);

        }
        static void Filters()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Фільтри за жанром ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Функція в розробці...\n");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);

            // Console.Write("Повернутися до початкового меню? (так): ");
            // Console.ForegroundColor = ConsoleColor.Cyan;
            // string choice = Console.ReadLine();
            // Console.ResetColor();


            // if (choice == "так")
            // {
            //     return;
            // }
            // else
            // {
            //     Console.WriteLine("Ви ввели щось незрозуміле!");
            //     return;
            // }

        }
        static void BuyHistory()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Історія покупок ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Функція в розробці...\n");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey(true);

            // Console.Write("Повернутися до початкового меню? (так): ");
            // Console.ForegroundColor = ConsoleColor.Cyan;
            // string choice = Console.ReadLine();
            // Console.ResetColor();


            // if (choice == "так")
            // {
            //     return;
            // }
            // else
            // {
            //     Console.WriteLine("Ви ввели щось незрозуміле!");
            //     return;
            // }

        }
        static void Statistics()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Статистика ігор ===\n");
            Console.ResetColor();

            if (g1.IsEmpty() && g2.IsEmpty() && g3.IsEmpty() && g4.IsEmpty() && g5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор наразі немає, статистику обчислити неможливо.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double total = 0;
            int count = 0;
            int countOver500 = 0;
            double minPrice = double.MaxValue;
            double maxPrice = double.MinValue;

            if (!g1.IsEmpty())
            {
                total += g1.Price;
                count++;
                if (g1.Price > 500) countOver500++;
                if (g1.Price < minPrice) minPrice = g1.Price;
                if (g1.Price > maxPrice) maxPrice = g1.Price;
            }

            if (!g2.IsEmpty())
            {
                total += g2.Price;
                count++;
                if (g2.Price > 500) countOver500++;
                if (g2.Price < minPrice) minPrice = g2.Price;
                if (g2.Price > maxPrice) maxPrice = g2.Price;
            }

            if (!g3.IsEmpty())
            {
                total += g3.Price;
                count++;
                if (g3.Price > 500) countOver500++;
                if (g3.Price < minPrice) minPrice = g3.Price;
                if (g3.Price > maxPrice) maxPrice = g3.Price;
            }

            if (!g4.IsEmpty())
            {
                total += g4.Price;
                count++;
                if (g4.Price > 500) countOver500++;
                if (g4.Price < minPrice) minPrice = g4.Price;
                if (g4.Price > maxPrice) maxPrice = g4.Price;
            }

            if (!g5.IsEmpty())
            {
                total += g5.Price;
                count++;
                if (g5.Price > 500) countOver500++;
                if (g5.Price < minPrice) minPrice = g5.Price;
                if (g5.Price > maxPrice) maxPrice = g5.Price;
            }

            double average = 0;
            if (count > 0)
                average = total / count;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Кількість ігор: {count}");
            Console.WriteLine($"Загальна сума: {total:F2} грн");
            Console.WriteLine($"Середня ціна: {average:F2} грн");
            Console.WriteLine($"Кількість ігор з ціною > 500 грн: {countOver500}");
            Console.WriteLine($"Мінімальна ціна: {minPrice:F2} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice:F2} грн");
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

            if (g1.IsEmpty() && g2.IsEmpty() && g3.IsEmpty() && g4.IsEmpty() && g5.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ігор немає, звіт сформувати неможливо.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
                Console.ReadKey(true);
                return;
            }

            double total = 0;
            int count = 0;
            double minPrice = double.MaxValue;
            double maxPrice = double.MinValue;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-7} {4,-8}", "Id", "Назва", "Жанр", "Рейтинг", "Ціна");
            Console.WriteLine(new string('-', 60));

            if (!g1.IsEmpty())
            {
                PrintGameReport(g1);
                total += g1.Price;
                count++;
                if (g1.Price < minPrice) minPrice = g1.Price;
                if (g1.Price > maxPrice) maxPrice = g1.Price;
            }
            if (!g2.IsEmpty())
            {
                PrintGameReport(g2);
                total += g2.Price;
                count++;
                if (g2.Price < minPrice) minPrice = g2.Price;
                if (g2.Price > maxPrice) maxPrice = g2.Price;
            }
            if (!g3.IsEmpty())
            {
                PrintGameReport(g3);
                total += g3.Price;
                count++;
                if (g3.Price < minPrice) minPrice = g3.Price;
                if (g3.Price > maxPrice) maxPrice = g3.Price;
            }
            if (!g4.IsEmpty())
            {
                PrintGameReport(g4);
                total += g4.Price;
                count++;
                if (g4.Price < minPrice) minPrice = g4.Price;
                if (g4.Price > maxPrice) maxPrice = g4.Price;
            }
            if (!g5.IsEmpty())
            {
                PrintGameReport(g5);
                total += g5.Price;
                count++;
                if (g5.Price < minPrice) minPrice = g5.Price;
                if (g5.Price > maxPrice) maxPrice = g5.Price;
            }

            double average = 0;
            if (count > 0)
                average = total / count;

            Console.WriteLine(new string('-', 60));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Підсумки:");
            Console.WriteLine($"Кількість ігор: {count}");
            Console.WriteLine($"Загальна сума: {total:F2} грн");
            Console.WriteLine($"Середня ціна: {average:F2} грн");
            Console.WriteLine($"Мінімальна ціна: {minPrice:F2} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice:F2} грн");
            Console.ResetColor();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey(true);
        }

        static void PrintGameReport(Game g)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-7:F1} {4,-8:F2}", g.Id, g.Name, g.Genre, g.Rating, g.Price);
        }

        static int ReadInt(string message, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(message);
                    int value = Convert.ToInt32(Console.ReadLine());
                    if (value < min || value > max)
                        Console.WriteLine($"Помилка! Введіть число від {min} до {max}.");
                    else
                        return value;
                }
                catch
                {
                    Console.WriteLine("Помилка! Введіть правильне число.");
                }
            }
        }

        static double ReadDouble(string message, double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(message);
                    double value = Convert.ToDouble(Console.ReadLine());
                    if (value < min || value > max)
                        Console.WriteLine($"Помилка! Введіть число від {min} до {max}.");
                    else
                        return value;
                }
                catch
                {
                    Console.WriteLine("Помилка! Введіть правильне число.");
                }
            }
        }

        static string ReadString(string message)
        {
            while (true)
            {
                try
                {
                    Console.Write(message);
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Помилка! Введіть текст.");
                    }
                    else
                        return input;
                }
                catch
                {
                    Console.WriteLine("Сталася невідома помилка вводу.");
                }
            }
        }



    }
}

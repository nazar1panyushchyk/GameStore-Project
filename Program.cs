using System;

namespace GameStore
{
    class Program
    {
        static void Main()
        {
            bool open = true;

            while (open)
            {

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
                Console.WriteLine("8. Вийти з магазину\n");
                Console.ResetColor();

                int choice;
                while (true)
                {
                    Console.Write("Введіть номер бажаної категорії: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string input = Console.ReadLine() ?? "";
                    Console.ResetColor();

                    try
                    {
                        choice = Convert.ToInt32(input);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Помилка! Введіть число від 1 до 8.");
                    }
                }

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

            int choice;
            while (true)
            {
                Console.Write("Введіть номер дії: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string input = Console.ReadLine() ?? "";
                Console.ResetColor();

                try
                {
                    choice = Convert.ToInt32(input);
                    break;
                }
                catch
                {
                    Console.WriteLine("Помилка! Введіть число від 1 до 6.");
                }
            }

            switch (choice)
            {
                case 1:
                    ShowGameList();
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nФункція в розробці...\n");
                    Console.ResetColor();
                    break;
                case 6:
                    Main();
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }

        static void ShowGameList()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n1. Minecraft");
            Console.WriteLine("2. Cyberpunk 2077");
            Console.WriteLine("3. Far Cry 3");
            Console.WriteLine("4. Stalker 2");
            Console.WriteLine("5. GTA V");
            Console.WriteLine("6. The Forest\n");
            Console.ResetColor();

            Console.Write("Повернутися до початкового меню? (так): ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string choice = Console.ReadLine() ?? "";
            Console.ResetColor();

            if (choice == "так")
            {
                return;
            }
            else
            {
                Console.WriteLine("Ви ввели щось незрозуміле!");
                return;
            }


        }


        static void Clients()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Наші клієнти ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Функція в розробці...\n");
            Console.ResetColor();

        }

        static void Orders()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Замовлення ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Minecraft - 700 грн");
            Console.WriteLine("2. Cyberpunk 2077 - 1400 грн");
            Console.WriteLine("3. Far Cry 3 - 300 грн");
            Console.WriteLine("4. Stalker 2 - 1300 грн");
            Console.WriteLine("5. GTA V - 950 грн");
            Console.WriteLine("6. The Forest - 280 грн\n");
            Console.ResetColor();

            double total = 0;

            string answer = "так";

            while (answer == "так")
            {
                int pick;

                while (true)
                {
                    Console.Write("Введіть номер гри, яку бажаєте придбати: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string input = Console.ReadLine() ?? "";
                    Console.ResetColor();

                    try
                    {
                        pick = Convert.ToInt32(input);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Помилка! Введіть номер гри від 1 до 6.");
                    }
                }

                if (pick == 1)
                    total += 700;
                else if (pick == 2)
                    total += 1400;
                else if (pick == 3)
                    total += 300;
                else if (pick == 4)
                    total += 1300;
                else if (pick == 5)
                    total += 950;
                else if (pick == 6)
                    total += 280;
                else
                    Console.WriteLine("Такої гри немає!");

                Console.Write("Бажаєте додати ще щось? (так/ні): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                answer = Console.ReadLine() ?? "ні";
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

            Console.Write("Повернутися до початкового меню? (так): ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string choice = Console.ReadLine() ?? "";
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("============================================\n");
            Console.ResetColor();

            if (choice == "так")
            {
                return;
            }
            else
            {
                Console.WriteLine("Ви ввели щось незрозуміле!");
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...\n");
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

            Console.Write("Повернутися до початкового меню? (так): ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string choice = Console.ReadLine() ?? "";
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("============================================\n");
            Console.ResetColor();

            if (choice == "так")
            {
                return;
            }
            else
            {
                Console.WriteLine("Ви ввели щось незрозуміле!");
            }
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

        }
        static void BuyHistory()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Історія покупок ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Функція в розробці...\n");
            Console.ResetColor();

        }
    }
}

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

            Console.Write("Введіть номер бажаної категорії: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

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
            Console.WriteLine("1. Minecraft");
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
            } else
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Функція в розробці...");
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
                Console.Write("Введіть номер гри, яку бажаєте придбати: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int pick = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();

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

            Random randomDiscount = new Random();
            double discount = randomDiscount.Next(5, 16) / 100.0;
            Console.WriteLine($"Знижка для Вас: {Math.Round(discount * 100)}%");

            double finalPrice = total * (1 - discount);
            finalPrice = Math.Round(finalPrice, 2);
            Console.WriteLine($"Сума до оплати: {finalPrice:F2} грн");
            Console.ResetColor();

        }
        static void Payments()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Платежі ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Функція в розробці...");
            Console.ResetColor();

        }
        static void Ratings()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Рейтинги ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Minecraft");
            Console.WriteLine("2. Cyberpunk 2077");
            Console.WriteLine("3. Far Cry 3");
            Console.WriteLine("4. Stalker 2");
            Console.WriteLine("5. GTA V");
            Console.WriteLine("6. The Forest\n");
            Console.WriteLine("0. Назад");
            Console.ResetColor();

        }
        static void Filters()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Фільтри за жанром ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Функція в розробці...");
            Console.ResetColor();

        }
        static void BuyHistory()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Історія покупок ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Функція в розробці...");
            Console.ResetColor();

        }
    }
}

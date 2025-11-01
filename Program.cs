using System;

namespace GameStore
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== Вітаємо в нашому GameStore! ===\n");
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
                Console.Write("Введіть номер бажаної гри: ");
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
    }
}

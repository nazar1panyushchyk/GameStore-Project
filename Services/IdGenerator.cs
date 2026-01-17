using System;
using System.IO;

namespace GameStore.Services
{
    /// <summary>
    /// Допоміжний клас для генерації унікальних ідентифікаторів.
    /// </summary>
    public class IdGenerator
    {
        private string folderPath;

        public IdGenerator(string folder)
        {
            folderPath = folder;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// Визначає наступний доступний ID для запису у вказаному файлі.
        /// Аналізує існуючі ID та повертає значення на 1 більше від максимального.
        /// </summary>
        /// <param name="fileName">Назва файлу (наприклад, "games.csv").</param>
        /// <returns>Унікальний цілочисельний ідентифікатор.</returns>
        public int GetNextId(string fileName)
        {
            string path = Path.Combine(folderPath, fileName);
            int maxId = 0;

            if (!File.Exists(path))
            {
                return 1;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');
                    int id;
                    if (parts.Length > 0 && int.TryParse(parts[0], out id))
                    {
                        if (id > maxId)
                        {
                            maxId = id;
                        }
                    }
                }
            }

            return maxId + 1;
        }
    }
}

using System;
using System.IO;

namespace GameStore.Services
{
    public class IdGenerator
    {
        private string folderPath;

        public IdGenerator(string folder)
        {
            folderPath = folder;
            
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        public int GetNextId(string fileName)
        {
            string path = Path.Combine(folderPath, fileName);
            int maxId = 0;

            if (!File.Exists(path))
                return 1;

            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');
                    int id;
                    if (parts.Length > 0 && int.TryParse(parts[0], out id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return maxId + 1;
        }
    }
}

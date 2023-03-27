using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;

namespace RandomTextRevel
{
    class Base
    {
        static void Main(string[] args)
        {
            //Console.Clear();

            Random rnd = new Random();
            int speed = 20;

            IEnumerable<string> words = ReadLogLines(GetDataFilePath("words.txt"));
            IEnumerable<string> letters = ReadLogLines(GetDataFilePath("letters.txt"));

            foreach(string word in words)
            {
                for(int i = 0; i < word.Length; i++)
                {
                    Render(i, word);
                }
                Console.WriteLine();
            }
            Console.Write("Press any key to escape.");
            Console.ReadKey();

            void Render(int index, string word)
            {
                string letter;
                string searchedLetter = word[index].ToString();
                int numberOfSearch = 0;
                Update();

                for(; numberOfSearch <= 5; )
                {
                    if(letter == searchedLetter)
                        return;
                    Update();
                }

                if(letter != searchedLetter)
                    Console.Write(searchedLetter);

                void Update()
                {
                    letter = letters.ToArray()[rnd.Next(0, letters.Count())];
                    Console.Write(letter);
                    Thread.Sleep(speed);
                    if(letter != searchedLetter)
                    {
                        Console.SetCursorPosition(Console.CursorLeft -1, Console.CursorTop);
                        numberOfSearch++;
                    }
                }
            }

            string GetDataFilePath(string fileName)
            {
                // Először megkeressük az aktuális munkakönyvtárat
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Végigmegyünk a szülőkönyvtárakon, amíg megtaláljuk a "RandomTextRevel" mappát
                DirectoryInfo parent = Directory.GetParent(currentDirectory);
                while (parent != null && parent.Name != "RandomTextRevel")
                {
                    parent = parent.Parent;
                }

                if (parent == null)
                {
                    // Nem találtuk meg a "RandomTextRevel" mappát
                    throw new DirectoryNotFoundException("Cannot find RandomTextRevel project directory.");
                }

                // Megkeressük a "data" mappát a "RandomTextRevel" mappában és visszaadjuk a fájl elérési útját
                string dataDirectory = Path.Combine(parent.FullName, "data");
                string filePath = Directory.GetFiles(dataDirectory, fileName, SearchOption.AllDirectories).FirstOrDefault();

                if (string.IsNullOrEmpty(filePath))
                {
                    // Nem találtuk meg a fájlt
                    throw new FileNotFoundException($"Cannot find {fileName} file.");
                }

                return filePath;
            }

            IEnumerable<string> ReadLogLines(string logPath)
            {
                using(StreamReader reader = File.OpenText(logPath))
                {
                    string? line = "";
                    while((line = reader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}

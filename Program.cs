using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace RandomTextRevel
{
    class Base
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int speed = 10;
            Console.WriteLine(Directory.GetCurrentDirectory());

            IEnumerable<string> words = ReadLogLines(Path.Combine(Directory.GetCurrentDirectory(), "data\\words.txt"));
            IEnumerable<string> letters = ReadLogLines(Path.Combine(Directory.GetCurrentDirectory(), "data\\letters.txt"));

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
                int snumberOfSearch = 0;
                Update();

                while(letter != searchedLetter)
                {
                    Update();
                    if(snumberOfSearch == 5)
                    {
                        Console.Write(searchedLetter);
                        break;
                    }
                }

                void Update()
                {
                    letter = letters.ToArray()[rnd.Next(0, letters.Count())];
                    Console.Write(letter);
                    Thread.Sleep(speed);
                    if(letter != searchedLetter)
                    {
                        Console.SetCursorPosition(Console.CursorLeft -1, Console.CursorTop);
                        snumberOfSearch++;
                    }
                }
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

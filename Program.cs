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
            Random rnd = new Random();
            int speed = 10;
            
            IEnumerable<string> words = ReadLogLines(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\data\words.txt");
            IEnumerable<string> letters = ReadLogLines(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\data\letters.txt");

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    internal class Program
    {
        static public bool ShouldFinish(List<Word>list)
        {
            int correctAnswers = 0;
            foreach(Word word in list)
            {
                if (word.Correct)
                {
                    correctAnswers++;
                }
            }
            return list.Count == correctAnswers;
        }
        static void Main(string[] args)
        {
            List<Word> wordList = new List<Word>();
            List<Word> wordsToRemove = new List<Word>();
            using (var words = new StreamReader("Data.txt"))
            {
                while (!words.EndOfStream)
                {
                   string word = words.ReadLine();
                    wordList.Add(new Word()
                    {
                        Correct = false,
                        English = word.Split('-')[0],
                        Polish = word.Split('-')[1],

                    });
                }
            }

            do
            {
                foreach (Word word in wordList)
                {
                    Console.Write("Słowo po angielsku: ");
                    Console.WriteLine(word.English);
                    Console.Write("Słowo po polsku: ");
                    string polishWord = Console.ReadLine();
                    if (polishWord == word.Polish)
                    {
                        Console.WriteLine("Brawo");
                        word.Correct = true;
                        wordsToRemove.Add(word);
                    }
                    else
                    {
                        Console.WriteLine($"Zła odpowiedź. Poprawne tłumaczenie to: {word.Polish}");
                        word.Correct = false;
                    }
                }
                //Console.WriteLine($"Słowo do usunięcia {wordToRemove}");
                foreach(Word word in wordsToRemove)
                {
                    wordList.Remove(word);
                }
                wordsToRemove.Clear();
            } while (!ShouldFinish(wordList));
            Console.ReadKey();
        }
    }
}

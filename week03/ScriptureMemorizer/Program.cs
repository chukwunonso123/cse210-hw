using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Class to represent a single word
    public class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
        }

        public bool IsHidden()
        {
            return _isHidden;
        }

        public string GetDisplayText()
        {
            return _isHidden ? new string('_', _text.Length) : _text;
        }
    }

    // Class to represent a scripture reference
    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int? _endVerse;

        public Reference(string book, int chapter, int startVerse, int? endVerse = null)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public string GetDisplayText()
        {
            return _endVerse == null
                ? $"{_book} {_chapter}:{_startVerse}"
                : $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }

    // Class to represent the scripture
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public void HideRandomWords(int numberToHide)
        {
            Random random = new Random();
            var visibleWords = _words.Where(word => !word.IsHidden()).ToList();

            for (int i = 0; i < numberToHide && visibleWords.Count > 0; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }
        }

        public string GetDisplayText()
        {
            string wordsText = string.Join(' ', _words.Select(word => word.GetDisplayText()));
            return $"{_reference.GetDisplayText()}\n{wordsText}";
        }

        public bool IsCompletelyHidden()
        {
            return _words.All(word => word.IsHidden());
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Scripture Memorizer!");

            // Example scripture
            Reference reference = new Reference("Proverbs", 3, 5, 6);
            string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding.";
            Scripture scripture = new Scripture(reference, text);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());

                if (scripture.IsCompletelyHidden())
                {
                    Console.WriteLine("\nYou have successfully hidden the entire scripture. Good job!");
                    break;
                }

                Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (input?.ToLower() == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                scripture.HideRandomWords(3); // Hide 3 random words per iteration
            }
        }
    }
}
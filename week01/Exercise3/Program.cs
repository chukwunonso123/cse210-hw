using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);
        int numberOfGuesses = 0;
        bool playAgain = true;

        while (playAgain)
        {
            Console.WriteLine("Welcome to Guess My Number!");

            while (true)
            {
                Console.Write("What is your guess? ");
                string userInput = Console.ReadLine();
                int guess = int.Parse(userInput);
                numberOfGuesses++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it! It took you {numberOfGuesses} guesses.");
                    break;
                }
            }

            Console.Write("Would you like to play again? (yes/no) ");
            string response = Console.ReadLine();
            playAgain = response.ToLower() == "yes";
            magicNumber = randomGenerator.Next(1, 101);
            numberOfGuesses = 0;
        }
    }
}
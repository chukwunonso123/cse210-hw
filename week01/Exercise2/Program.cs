using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise2 Project.");
        Console.Write("What is your grade percentage? ");
        string userInput = Console.ReadLine();
        int grade = int.Parse(userInput);

        string letter = "";
        string sign = "";

        if (grade >= 90)
        {
            letter = "A";
            if (grade % 10 < 3)
            {
                sign = "-";
            }
        }
        else if (grade >= 80)
        {
            letter = "B";
            if (grade % 10 >= 7)
            {
                sign = "+";
            }
            else if (grade % 10 < 3)
            {
                sign = "-";
            }
        }
        else if (grade >= 70)
        {
            letter = "C";
            if (grade % 10 >= 7)
            {
                sign = "+";
            }
            else if (grade % 10 < 3)
            {
                sign = "-";
            }
        }
        else if (grade >= 60)
        {
            letter = "D";
            if (grade % 10 >= 7)
            {
                sign = "+";
            }
            else if (grade % 10 < 3)
            {
                sign = "-";
            }
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is {letter}{sign}.");

        if (grade >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Sorry, you did not pass the course. Better luck next time!");
        }
    }
}

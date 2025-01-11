using System;

class Program
{
    static void Main(string[] args)
    {
        List<double> numbers = new List<double>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            string userInput = Console.ReadLine();
            double number = double.Parse(userInput);

            if (number == 0)
            {
                break;
            }

            numbers.Add(number);
        }

        double sum = numbers.Sum();
        double average = numbers.Average();
        double max = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch challenges
        double smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty(double.MaxValue).Min();
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        var sortedNumbers = numbers.OrderBy(n => n);
        Console.WriteLine("The sorted list is:");
        foreach (var number in sortedNumbers)
        {
            Console.WriteLine(number);
        }
    }
}
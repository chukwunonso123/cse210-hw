using System;

public class Fraction
{
    private int numerator;
    private int denominator;

    // Constructor with no parameters
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    // Constructor with one parameter
    public Fraction(int numerator)
    {
        this.numerator = numerator;
        denominator = 1;
    }

    // Constructor with two parameters
    public Fraction(int numerator, int denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator;
    }

    // Getters and setters
    public int GetNumerator()
    {
        return numerator;
    }

    public void SetNumerator(int numerator)
    {
        this.numerator = numerator;
    }

    public int GetDenominator()
    {
        return denominator;
    }

    public void SetDenominator(int denominator)
    {
        this.denominator = denominator;
    }

    // Method to return the fraction as a string
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    // Method to return the decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create fractions using the different constructors
        Fraction fraction1 = new Fraction();
        Fraction fraction2 = new Fraction(5);
        Fraction fraction3 = new Fraction(3, 4);

        // Verify getters and setters
        fraction1.SetNumerator(10);
        fraction1.SetDenominator(2);
        Console.WriteLine($"Numerator: {fraction1.GetNumerator()}, Denominator: {fraction1.GetDenominator()}");

        // Verify GetFractionString and GetDecimalValue methods
        Console.WriteLine($"Fraction 1: {fraction1.GetFractionString()} = {fraction1.GetDecimalValue()}");
        Console.WriteLine($"Fraction 2: {fraction2.GetFractionString()} = {fraction2.GetDecimalValue()}");
        Console.WriteLine($"Fraction 3: {fraction3.GetFractionString()} = {fraction3.GetDecimalValue()}");
    }
}
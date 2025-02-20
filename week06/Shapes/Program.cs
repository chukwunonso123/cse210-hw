using System;
using System.Collections.Generic;

// Base Shape class
public class Shape
{
    public string Color { get; set; }

    public Shape(string color)
    {
        Color = color;
    }

    public virtual double GetArea()
    {
        return 0; // Default area (will be overridden)
    }
}

// Square class
public class Square : Shape
{
    public double Side { get; set; }

    public Square(string color, double side) : base(color)
    {
        Side = side;
    }

    public override double GetArea()
    {
        return Side * Side;
    }
}

// Rectangle class
public class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }

    public Rectangle(string color, double length, double width) : base(color)
    {
        Length = length;
        Width = width;
    }

    public override double GetArea()
    {
        return Length * Width;
    }
}

// Circle class
public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(string color, double radius) : base(color)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("Red", 5));
        shapes.Add(new Rectangle("Blue", 4, 6));
        shapes.Add(new Circle("Green", 3));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.Color}, Area: {shape.GetArea()}");
        }
    }
}
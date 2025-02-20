using System;
using System.Collections.Generic;
using System.IO;

// Base class for all goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsCompleted = false;
    }

    public abstract void RecordEvent();
    public abstract string GetStringRepresentation();
    public abstract string GetDisplayString();
}

// Simple goal: complete once
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            Console.WriteLine($"You completed '{Name}' and earned {Points} points!");
        }
        else
        {
            Console.WriteLine($"You've already completed '{Name}'.");
        }
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{Name},{Points},{IsCompleted}";
    }

    public override string GetDisplayString()
    {
        return $"[{(IsCompleted ? "X" : " ")}] {Name}";
    }
}

// Eternal goal: complete many times
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"You recorded progress on '{Name}' and earned {Points} points!");
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{Name},{Points}";
    }

    public override string GetDisplayString()
    {
        return $"[ ] {Name}";
    }
}

// Checklist goal: complete a certain number of times
public class ChecklistGoal : Goal
{
    public int RequiredCount { get; set; }
    public int CurrentCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, int points, int requiredCount, int bonusPoints) : base(name, points)
    {
        RequiredCount = requiredCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        if (CurrentCount < RequiredCount)
        {
            CurrentCount++;
            Console.WriteLine($"You recorded progress on '{Name}' ({CurrentCount}/{RequiredCount}) and earned {Points} points!");

            if (CurrentCount == RequiredCount)
            {
                IsCompleted = true;
                Console.WriteLine($"You completed '{Name}' and earned a bonus of {BonusPoints} points!");
            }
        }
        else
        {
            Console.WriteLine($"You've already completed '{Name}'.");
        }
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{Name},{Points},{RequiredCount},{CurrentCount},{BonusPoints}";
    }

    public override string GetDisplayString()
    {
        return $"[{(IsCompleted ? "X" : " ")}] {Name} (Completed {CurrentCount}/{RequiredCount} times)";
    }
}

public class EternalQuest
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;
    private string filename = "goals.txt";

    public void Run()
    {
        LoadGoals();

        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListGoals();
                    break;
                case "2":
                    CreateGoal();
                    break;
                case "3":
                    RecordEvent();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    SaveGoals();
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\nEternal Quest Menu:");
        Console.WriteLine("1. List Goals");
        Console.WriteLine("2. Create New Goal");
        Console.WriteLine("3. Record Event");
        Console.WriteLine("4. Display Score");
        Console.WriteLine("5. Save and Exit");
        Console.Write("Enter your choice: ");
    }

    private void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDisplayString()}");
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("\nChoose Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        Goal newGoal = null;

        switch (choice)
        {
            case "1":
                newGoal = new SimpleGoal(name, points);
                break;
            case "2":
                newGoal = new EternalGoal(name, points);
                break;
            case "3":
                Console.Write("Enter required count: ");
                int requiredCount = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                newGoal = new ChecklistGoal(name, points, requiredCount, bonusPoints);
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        goals.Add(newGoal);
        Console.WriteLine("Goal created!");
    }

    private void RecordEvent()
    {
        ListGoals();
        Console.Write("Enter the number of the goal you completed: ");
        if (int.TryParse(Console.ReadLine(), out int goalNumber) && goalNumber > 0 && goalNumber <= goals.Count)
        {
            Goal goal = goals[goalNumber - 1];
            goal.RecordEvent();
            if (goal.IsCompleted && goal is ChecklistGoal checklistGoal)
            {
                score += goal.Points + checklistGoal.BonusPoints;
            }
            else
            {
                score += goal.Points;
            }
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    private void DisplayScore()
    {
        Console.WriteLine($"\nYour Score: {score}");
    }

    private void SaveGoals()
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(score);
            foreach (Goal goal in goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved.");
    }

    private void LoadGoals()
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            if (lines.Length > 0)
            {
                score = int.Parse(lines[0]);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(':');
                    string[] goalParts = parts[1].Split(',');
                    if (parts[0] == "SimpleGoal")
                    {
                        goals.Add(new SimpleGoal(goalParts[0], int.Parse(goalParts[1])) { IsCompleted = bool.Parse(goalParts[2]) });
                    }
                    else if (parts[0] == "EternalGoal")
                    {
                        goals.Add(new EternalGoal(goalParts[0], int.Parse(goalParts[1])));
                    }
                    else if (parts[0] == "ChecklistGoal")
                    {
                        goals.Add(new ChecklistGoal(goalParts[0], int.Parse(goalParts[1]), int.Parse(goalParts[2]), int.Parse(goalParts[4])) { CurrentCount = int.Parse(goalParts[3]) });
                    }
                }
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        EternalQuest quest = new EternalQuest();
        quest.Run();
    }
}
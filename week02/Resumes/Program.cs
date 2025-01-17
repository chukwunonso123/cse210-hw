using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Resume> resumes = new List<Resume>();
        string userChoice = "";

        while (userChoice != "5")
        {
            Console.WriteLine("\nResume Builder Menu:");
            Console.WriteLine("1. Create a new resume");
            Console.WriteLine("2. View all resumes");
            Console.WriteLine("3. Save resumes to a file");
            Console.WriteLine("4. Load resumes from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    resumes.Add(CreateResume());
                    break;
                case "2":
                    DisplayResumes(resumes);
                    break;
                case "3":
                    SaveResumes(resumes);
                    break;
                case "4":
                    resumes = LoadResumes();
                    break;
                case "5":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static Resume CreateResume()
    {
        Console.Write("Enter your full name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your email address: ");
        string email = Console.ReadLine();

        List<string> skills = new List<string>();
        Console.WriteLine("Enter your skills (type 'done' when finished):");
        string skill;
        while ((skill = Console.ReadLine()) != "done")
        {
            skills.Add(skill);
        }

        List<string> experience = new List<string>();
        Console.WriteLine("Enter your work experience (type 'done' when finished):");
        string exp;
        while ((exp = Console.ReadLine()) != "done")
        {
            experience.Add(exp);
        }

        return new Resume(name, email, skills, experience);
    }

    static void DisplayResumes(List<Resume> resumes)
    {
        if (resumes.Count == 0)
        {
            Console.WriteLine("No resumes available.");
            return;
        }

        foreach (var resume in resumes)
        {
            Console.WriteLine("\n============================");
            Console.WriteLine(resume);
            Console.WriteLine("============================");
        }
    }

    static void SaveResumes(List<Resume> resumes)
    {
        Console.Write("Enter filename to save resumes: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var resume in resumes)
            {
                writer.WriteLine(resume.ToFileFormat());
            }
        }

        Console.WriteLine("Resumes saved successfully.");
    }

    static List<Resume> LoadResumes()
    {
        Console.Write("Enter filename to load resumes from: ");
        string filename = Console.ReadLine();

        List<Resume> resumes = new List<Resume>();
        try
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                resumes.Add(Resume.FromFileFormat(line));
            }

            Console.WriteLine("Resumes loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading resumes: {ex.Message}");
        }

        return resumes;
    }
}

class Resume
{
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Skills { get; set; }
    public List<string> Experience { get; set; }

    public Resume(string name, string email, List<string> skills, List<string> experience)
    {
        Name = name;
        Email = email;
        Skills = skills;
        Experience = experience;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nEmail: {Email}\nSkills: {string.Join(", ", Skills)}\nExperience: {string.Join(", ", Experience)}";
    }

    public string ToFileFormat()
    {
        return $"{Name}|{Email}|{string.Join(",", Skills)}|{string.Join(",", Experience)}";
    }

    public static Resume FromFileFormat(string fileLine)
    {
        string[] parts = fileLine.Split('|');
        string name = parts[0];
        string email = parts[1];
        List<string> skills = new List<string>(parts[2].Split(','));
        List<string> experience = new List<string>(parts[3].Split(','));
        return new Resume(name, email, skills, experience);
    }
}
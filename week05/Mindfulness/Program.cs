using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("---------------------");

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");

                Console.Write("Choose an activity: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        BreathingActivity activity1 = new BreathingActivity();
                        activity1.Start();
                        break;
                    case 2:
                        ReflectionActivity activity2 = new ReflectionActivity();
                        activity2.Start();
                        break;
                    case 3:
                        ListingActivity activity3 = new ListingActivity();
                        activity3.Start();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");
                        break;
                }
            }
        }
    }

    abstract class MindfulnessActivity
    {
        protected int duration;

        public void Start()
        {
            Console.Write("Enter duration (in seconds): ");
            duration = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Starting {GetType().Name}...");
            Console.WriteLine("Prepare to begin...");
            Thread.Sleep(3000);

            Activity();
        }

        protected abstract void Activity();
    }

    class BreathingActivity : MindfulnessActivity
    {
        protected override void Activity()
        {
            for (int i = 0; i < duration; i++)
            {
                Console.WriteLine("Breathe in...");
                Thread.Sleep(2000);
                Console.WriteLine("Breathe out...");
                Thread.Sleep(2000);
            }
        }
    }

    class ReflectionActivity : MindfulnessActivity
    {
        private List<string> prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        protected override void Activity()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];

            Console.WriteLine(prompt);

            foreach (string question in questions)
            {
                Console.WriteLine(question);
                Thread.Sleep(3000);
            }
        }
    }

    class ListingActivity : MindfulnessActivity
    {
        private List<string> prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        protected override void Activity()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];

            Console.WriteLine(prompt);

            int count = 0;
            while (true)
            {
                Console.Write("Enter item (or 'done' to finish): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                {
                    break;
                }

                count++;
                Console.WriteLine($"Item {count}: {input}");
            }

            Console.WriteLine($"You listed {count} items.");
        }
    }
}
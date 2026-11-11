using System;
using System.Collections.Generic;
using System.Numerics;
namespace QuizBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ställ in UTF-8 för att snygga ramar och tecken ska fungera i konsolenConsole.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Välkomstskärm och spelarnamnConsole.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║            VÄLKOMMEN TILL              ║");
            Console.WriteLine("║             QUIZBATTLE!                ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nSkriv in ditt spelarnamn: ");
            string playerName = Console.ReadLine();
            Player player = new Player(string.IsNullOrWhiteSpace(playerName) ? "Spelare 1" : playerName);

            // 2. Skapa QuestionManager och ladda frågor från filen            QuestionManager manager = new QuestionManager();
            try
            {
                manager.LoadQuestions("questions.json");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nKunde inte läsa in 'questions.json': {ex.Message}");
                Console.WriteLine("Kontrollera att filen finns i mappen. Tryck på valfri knapp för att avsluta.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            bool running = true;
            while (running)
            {
                Console.Clear();
                // Huvudmeny-rutaConsole.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("│               HUVUDMENY                │");
                Console.WriteLine("└────────────────────────────────────────┘");
                Console.ResetColor();

                Console.WriteLine($" Spelare: {player.Name} | Poäng: {player.Score} | Streak: 🔥 {player.CurrentStreak}");
                Console.WriteLine("\n Välj ett alternativ i menyn:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [1] 🚀 Starta QuizBattle");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [2] 📊 Visa spelarinfo");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [3] ❌ Avsluta spelet");
                Console.ResetColor();
                Console.Write("\n Ditt val (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayGame(manager, player);
                        break;
                    case "2":
                        ShowPlayerStats(player);
                        break;
                    case "3":
                        running = false;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Tack för att du spelade QuizBattle! Välkommen åter.");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Ogiltigt val! Välj mellan 1 och 3. Tryck på valfri knapp.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void PlayGame(QuestionManager manager, Player player)
        {
            List<Question> questions = manager.GetAllQuestions();

            if (questions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInga frågor hittades i listan!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            foreach (var question in questions)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("──────────────────────────────────────────");
                Console.ResetColor();

                question.DisplayQuestion();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("──────────────────────────────────────────");
                Console.ResetColor();

                Console.Write("\nDitt svar: ");
                string input = Console.ReadLine();

                bool isCorrect = question.CheckAnswer(input);

                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n RÄTT! Du fick {question.Points} poäng! 🎉");
                    player.AddCorrectAnswer(question.Points);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n FEL SVAR! ❌ Bättre lycka nästa gång.");
                    player.ResetStreak();
                    Console.ResetColor();
                }

                Console.Write("\nTryck på valfri knapp för nästa fråga...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║           SPELET ÄR SLUT!              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"\nBra kämpat, {player.Name}!");
            Console.WriteLine($"Din slutpoäng: {player.Score} poäng.");
            Console.WriteLine($"Antal rätta svar: {player.CorrectAnswersCount}");

            Console.Write("\nTryck på valfri knapp för att återgå till menyn...");
            Console.ReadKey();
        }

        private static void ShowPlayerStats(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("│            SPELARSTATISTIK             │");
            Console.WriteLine("└────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine($" Namn:              {player.Name}");
            Console.WriteLine($" Totalpoäng:        {player.Score} p");
            Console.WriteLine($" Rätta svar:        {player.CorrectAnswersCount} st");
            Console.WriteLine($" Nuvarande streak:    🔥 {player.CurrentStreak} i rad");

            Console.Write("\nTryck på valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }
    }
}

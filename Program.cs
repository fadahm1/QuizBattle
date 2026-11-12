
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.Clear();
            ShowWelcomeScreen();

            Console.Write("\nSkriv in ditt spelarnamn: ");
            string playerName = Console.ReadLine() ?? "";

            Player player = new Player(
                string.IsNullOrWhiteSpace(playerName) ? "Spelare 1" : playerName
            );

            QuestionManager manager = new QuestionManager();

            try
            {
                manager.LoadQuestions("questions.json");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nKunde inte läsa in questions.json.");
                Console.WriteLine($"Fel: {ex.Message}");
                Console.ResetColor();

                Console.WriteLine("\nKontrollera att filen finns.");
                Console.WriteLine("Tryck på valfri knapp för att avsluta.");
                Console.ReadKey();
                return;
            }

            bool running = true;

            while (running)
            {
                Console.Clear();
                ShowMainMenu(player);

                string choice = Console.ReadLine() ?? "";

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
                        ShowGoodbyeScreen();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOgiltigt val! Välj mellan 1 och 3.");
                        Console.ResetColor();

                        Console.WriteLine("Tryck på valfri knapp för att försöka igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowWelcomeScreen()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("========================================");
            Console.WriteLine("                                        ");
            Console.WriteLine("           VÄLKOMMEN TILL               ");
            Console.WriteLine("             QUIZBATTLE!                ");
            Console.WriteLine("                                        ");
            Console.WriteLine("========================================");

            Console.ResetColor();
        }

        private static void ShowMainMenu(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("========================================");
            Console.WriteLine("                HUVUDMENY               ");
            Console.WriteLine("========================================");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine($" Spelare : {player.Name}");
            Console.WriteLine($" Poäng   : {player.Score}");
            Console.WriteLine($" Streak  : {player.CurrentStreak}");

            Console.WriteLine("\nVälj ett alternativ:\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [1] Starta QuizBattle");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [2] Visa spelarinfo");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [3] Avsluta spelet");

            Console.ResetColor();

            Console.Write("\nDitt val (1-3): ");
        }

        private static void PlayGame(QuestionManager manager, Player player)
        {
            List<Question> questions = manager.GetAllQuestions();

            if (questions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInga frågor hittades!");
                Console.ResetColor();

                Console.WriteLine("Tryck på valfri knapp för att återgå till menyn.");
                Console.ReadKey();
                return;
            }

            foreach (var question in questions)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("========================================");
                Console.WriteLine("               QUIZBATTLE               ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                Console.WriteLine($"\nSpelare: {player.Name}");
                Console.WriteLine($"Poäng: {player.Score}");

                Console.WriteLine();
                question.DisplayQuestion();

                Console.Write("\nDitt svar: ");
                string input = Console.ReadLine() ?? "";

                bool isCorrect = question.CheckAnswer(input);

                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nRÄTT! Du fick {question.Points} poäng!");
                    player.AddCorrectAnswer(question.Points);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFEL SVAR! Bättre lycka nästa gång.");
                    player.ResetStreak();
                }

                Console.ResetColor();

                Console.WriteLine($"\nTotalpoäng: {player.Score}");
                Console.WriteLine($"Nuvarande streak: {player.CurrentStreak}");

                Console.WriteLine("\nTryck på valfri knapp för nästa fråga...");
                Console.ReadKey();
            }

            ShowGameResults(player);
        }

        private static void ShowGameResults(Player player)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("========================================");
            Console.WriteLine("              SPELET ÄR SLUT!           ");
            Console.WriteLine("========================================");

            Console.ResetColor();

            Console.WriteLine($"\nBra kämpat, {player.Name}!");

            Console.WriteLine("\nDINA RESULTAT:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($" Totalpoäng       : {player.Score}");
            Console.WriteLine($" Rätta svar       : {player.CorrectAnswers}");
            Console.WriteLine($" Nuvarande streak : {player.CurrentStreak}");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("\nTryck på valfri knapp för att återgå till menyn...");
            Console.ReadKey();
        }

        private static void ShowPlayerStats(Player player)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("========================================");
            Console.WriteLine("             SPELARSTATISTIK            ");
            Console.WriteLine("========================================");

            Console.ResetColor();

            Console.WriteLine($"\n Namn              : {player.Name}");
            Console.WriteLine($" Totalpoäng        : {player.Score} p");
            Console.WriteLine($" Rätta svar        : {player.CorrectAnswers} st");
            Console.WriteLine($" Nuvarande streak  : {player.CurrentStreak} i rad");

            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Tryck på valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }

        private static void ShowGoodbyeScreen()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("========================================");
            Console.WriteLine("                                        ");
            Console.WriteLine("       TACK FÖR ATT DU SPELADE!         ");
            Console.WriteLine("          VÄLKOMMEN ÅTER!               ");
            Console.WriteLine("                                        ");
            Console.WriteLine("========================================");

            Console.ResetColor();
        }
    }
}

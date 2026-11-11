using System; // hantera grundläggande funktioner
using System.Collections.Generic; // hantera listor
using System.Linq; // filtrera och sortera listor

namespace QuizBattle
{
    public class GameEngine // klassen som styr spelet
    {
        private List<Player> players = new List<Player>(); // lista som håller alla spelare
        private QuestionManager questionManager; 
        private Random random = new Random(); // skapar en instans av Random för att slumpa frågor
        private int rounds;
        public GameEngine(QuestionManager manager)
        {
            questionManager = manager;
        }

        public void StartGame()
        {
            players.Clear();
            Console.WriteLine("=== Quiz Battle ===");
            int playerCount = ReadNumber("Number of players (2-4): ", 2, 4);
            for (int i = 1; i <= playerCount; i++)
            {
                string name; 
                while (true)
                {
                    Console.Write($"Name of player {i}: ");
                    name = (Console.ReadLine() ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Name cannot be emtpy");
                    }
                    else if (players.Any(p=> p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("Name already taken. Please choose a different name.");
                    }
                    else
                    {
                        break;
                    }
                }
                players.Add(new Player(name));
            }
            rounds = ReadNumber("Number of rounds (1-10): ", 1, 10);
        
        }
        private int ReadNumber(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine($"Enter a number between {min} and {max}.");
            }
        }
    }
}
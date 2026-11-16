using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizBattle
{
    public class GameEngine // klassen som styr spelet
    {
        private List<Player> players = new List<Player>(); // alla spelare
        public List<Player> Players => players;
        private QuestionManager questionManager;
        private Random random = new Random(); // för att slumpa kategori och fråga
        private int rounds;

        // Frågor som inte har ställts än i det här spelet (kategori -> frågor)
        private Dictionary<string, List<IQuestion>> remainingQuestions = new Dictionary<string, List<IQuestion>>();

        public GameEngine(QuestionManager manager)
        {
            questionManager = manager;
        }

        // Spelar ett helt spel från start till slut
        public void Play()
        {
            StartGame();
            LoadRemainingQuestions();
            PlayRounds();
            ShowFinalResult();
        }

        // Frågar efter antal spelare, namn och antal rundor
        public void StartGame()
        {
            players.Clear();
            Console.WriteLine();
            int playerCount = ReadNumber("Antal spelare (2-4): ", 2, 4);
            for (int i = 1; i <= playerCount; i++)
            {
                string name;
                while (true)
                {
                    Console.Write($"Namn på spelare {i}: ");
                    name = (Console.ReadLine() ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        WriteColored("Namnet får inte vara tomt.", ConsoleColor.Red);
                    }
                    else if (players.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        WriteColored("Namnet är redan taget. Välj ett annat namn.", ConsoleColor.Red);
                    }
                    else
                    {
                        break;
                    }
                }
                players.Add(new Player(name));
            }
            rounds = ReadNumber("Antal rundor (1-10): ", 1, 10);
        }

        // Kopierar frågorna så att vi kan ta bort dem när de har ställts
        private void LoadRemainingQuestions()
        {
            remainingQuestions.Clear();
            foreach (var category in questionManager.Categories)
            {
                if (category.Value.Count > 0)
                {
                    remainingQuestions[category.Key] = new List<IQuestion>(category.Value);
                }
            }
        }

        // Varje runda svarar varje spelare på en fråga
        private void PlayRounds()
        {
            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine();
                WriteColored($"=== Runda {round} av {rounds} ===", ConsoleColor.Cyan);

                foreach (Player player in players)
                {
                    // Är alla frågor slut avslutas spelet i förtid
                    if (remainingQuestions.Count == 0)
                    {
                        Console.WriteLine();
                        WriteColored("Alla frågor är slut. Spelet avslutas i förtid.", ConsoleColor.Yellow);
                        return;
                    }

                    PlayTurn(player);
                }
            }
        }

        // En spelares tur: välj kategori, svara på en fråga, få poäng
        private void PlayTurn(Player player)
        {
            string category = ChooseCategory(player);
            IQuestion question = TakeRandomQuestion(category);

            Console.WriteLine();
            WriteColored("Kategori: " + category, ConsoleColor.Magenta);
            question.DisplayQuestion();

            string answer = ReadAnswer(question);

            if (question.CheckAnswer(answer))
            {
                // Räknar ut hur mycket seriebonus spelaren fick
                int scoreBefore = player.Score;
                player.AddCorrectAnswer(question.Points);
                int bonus = player.Score - scoreBefore - question.Points;

                string text = $"Rätt! {player.Name} får {question.Points} poäng";
                if (bonus > 0)
                {
                    text += $" + {bonus} i seriebonus ({player.CurrentStreak} rätt i rad)";
                }
                WriteColored(text + $". Totalt: {player.Score} poäng.", ConsoleColor.Green);
            }
            else
            {
                // Fel svar nollställer serien
                player.ResetStreak();
                WriteColored($"Fel. Rätt svar var: {GetCorrectAnswerText(question)}. {player.Name} får 0 poäng. Totalt: {player.Score} poäng.", ConsoleColor.Red);
            }
        }

        // Visar kategorierna med antal frågor kvar, eller låter slumpen välja
        private string ChooseCategory(Player player)
        {
            List<string> names = remainingQuestions.Keys.ToList();

            Console.WriteLine();
            WriteColored($"{player.Name}, välj kategori:", ConsoleColor.Yellow);
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]} ({remainingQuestions[names[i]].Count} frågor kvar)");
            }
            Console.WriteLine($"{names.Count + 1}. Slumpa");

            int choice = ReadNumber("Val: ", 1, names.Count + 1);

            // Sista valet är "Slumpa": då väljer slumpen en kategori
            if (choice == names.Count + 1)
            {
                return names[random.Next(names.Count)];
            }
            return names[choice - 1];
        }

        // Tar en slumpad fråga och tar bort den så att den inte ställs igen
        private IQuestion TakeRandomQuestion(string category)
        {
            List<IQuestion> list = remainingQuestions[category];
            int index = random.Next(list.Count);
            IQuestion question = list[index];
            list.RemoveAt(index);

            // Är kategorin slut försvinner den ur listan
            if (list.Count == 0)
            {
                remainingQuestions.Remove(category);
            }

            return question;
        }

        // Läser svaret och kontrollerar att det är giltigt innan vi rättar
        private string ReadAnswer(IQuestion question)
        {
            // Flervalsfråga: spelaren svarar med en siffra
            if (question is MultipleChoiceQuestion multipleChoice)
            {
                int count = multipleChoice.Options.Count;
                int number = ReadNumber($"Ditt svar (1-{count}): ", 1, count);
                return number.ToString();
            }

            // Sant/falskt: spelaren svarar s eller f
            if (question is TrueFalseQuestion)
            {
                while (true)
                {
                    Console.Write("Sant (s) eller falskt (f)? ");
                    string input = (Console.ReadLine() ?? "").Trim().ToLower();

                    if (input == "s" || input == "1" || input == "sant") return "1";
                    if (input == "f" || input == "2" || input == "falskt") return "2";

                    WriteColored("Svara s (sant) eller f (falskt).", ConsoleColor.Red);
                }
            }

            Console.Write("Ditt svar: ");
            return Console.ReadLine() ?? "";
        }

        // Det rätta svaret som text, visas när spelaren svarar fel
        private string GetCorrectAnswerText(IQuestion question)
        {
            if (question is MultipleChoiceQuestion multipleChoice)
            {
                return multipleChoice.Options[multipleChoice.CorrectIndex];
            }
            if (question is TrueFalseQuestion trueFalse)
            {
                return trueFalse.CorrectAnswer ? "Sant" : "Falskt";
            }
            return "";
        }

        // Visar slutresultatet och vinnaren (eller delad seger)
        private void ShowFinalResult()
        {
            // Flest poäng först, vid lika poäng flest rätta svar
            List<Player> ranking = players
                .OrderByDescending(p => p.Score)
                .ThenByDescending(p => p.CorrectAnswers)
                .ToList();

            Console.WriteLine();
            WriteColored("=== Slutresultat ===", ConsoleColor.Cyan);
            for (int i = 0; i < ranking.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ranking[i].Name}  {ranking[i].Score} poäng  ({ranking[i].CorrectAnswers} rätt)");
            }

            // Alla med samma poäng och samma antal rätta som etta delar segern
            Player best = ranking[0];
            List<Player> winners = ranking
                .Where(p => p.Score == best.Score && p.CorrectAnswers == best.CorrectAnswers)
                .ToList();

            if (winners.Count == 1)
            {
                WriteColored($"{best.Name} vinner!", ConsoleColor.Yellow);
            }
            else
            {
                WriteColored("Delad seger: " + string.Join(", ", winners.Select(p => p.Name)), ConsoleColor.Yellow);
            }
        }

        // Frågar tills användaren skriver ett tal mellan min och max
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
                WriteColored($"Svara med en siffra mellan {min} och {max}.", ConsoleColor.Red);
            }
        }

        // Skriver en rad i en viss färg och återställer färgen efteråt
        private void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
using System;
using System.Text;
using QuizBattle;

public class Program
{
    public static void Main()
    {
        // Så att å, ä och ö visas rätt i konsolen
        Console.OutputEncoding = Encoding.UTF8;

        Menu menu = new Menu();
        HighscoreManager highscores = new HighscoreManager();
        QuestionManager questionManager = new QuestionManager();

        // Läser frågorna, avslutar om filen saknas
        try
        {
            questionManager.LoadQuestions("data/questions.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kunde inte läsa frågorna: " + ex.Message);
            return;
        }

        while (true)
        {
            int choice = menu.Show();

            if (choice == 1)
            {
                GameEngine engine = new GameEngine(questionManager);
                engine.Play();

                // Efter spelet sparas resultaten på topplistan
                highscores.AddResults(engine.Players);
                Console.WriteLine("Resultaten sparades i data/highscores.json");
            }
            else if (choice == 2)
            {
                highscores.Display();
            }
            else
            {
                Console.WriteLine("Hej då!");
                break;
            }
        }
    }
}
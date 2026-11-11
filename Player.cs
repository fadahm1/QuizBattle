using System;
namespace QuizBattle
{
    public class Player // skapar klass som representerar en spelare
  {
    public string Name { get; set; } // sparar spelarens namn
    public int Score { get; private set; } // sparar spelarens totala poäng
    public int CorrectAnswers { get; private set; } // Räknar antalet rätta svar
    public int CurrentStreak { get; private set; }

    
    public Player(string name) // konstruktor som tar spelarens namn som parameter
    {
        Name = name; // sätter spelarens namn
        Score = 0; // sätter spelarens poäng till 0
        CorrectAnswers = 0; // sätter antalet rätta svar till 0
         CurrentStreak = 0; // sätter antalet rätta svar i rad till 0
    }
           public void AddCorrectAnswer(int basePoints)
        {
            CorrectAnswersCount++;
            CurrentStreak++;

            int bonusPoints = (CurrentStreak > 1) ? 5 : 0;
            Score += basePoints + bonusPoints;
        }

        // LÄGG TILL DENNA METOD OM DEN SAKNAS:
        public void ResetStreak()
        {
            CurrentStreak = 0;
        }
    }
}

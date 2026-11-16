using System;

namespace QuizBattle
{
    // Representerar en spelare i spelet
    public class Player
    {
        public string Name { get; set; }                 // spelarens namn
        public int Score { get; private set; }           // totala poäng
        public int CorrectAnswers { get; private set; }  // antal rätta svar
        public int CurrentStreak { get; private set; }   // antal rätta svar i rad

        // Konstruktor: skapar en spelare med namn och börjar på noll
        public Player(string name)
        {
            Name = name;
            Score = 0;
            CorrectAnswers = 0;
            CurrentStreak = 0;
        }

        // Anropas vid rätt svar: räknar upp serien och lägger till poäng
        public void AddCorrectAnswer(int basePoints)
        {
            CorrectAnswers++;
            CurrentStreak++;

            // Seriebonus: +5 för varje rätt svar i rad efter det första, högst 20
            int bonusPoints = Math.Min((CurrentStreak - 1) * 5, 20);

            Score += basePoints + bonusPoints;
        }

        // Anropas vid fel svar: serien nollställs
        public void ResetStreak()
        {
            CurrentStreak = 0;
        }
    }
}
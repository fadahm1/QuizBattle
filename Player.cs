using System;
public class Player // skapar klass som representerar en spelare
{
    public string Name { get; set; } // sparar spelarens namn
    public int Score { get; private set; } // sparar spelarens totala poäng
    public int CorrectAnswers { get; private set; } // Räknar antalet rätta svar
    public int Streak { get; private set; } // Räknar antalet rätta svar i rad
    
    public Player(string name) // konstruktor som tar spelarens namn som parameter
    {
        Name = name; // sätter spelarens namn
        Score = 0; // sätter spelarens poäng till 0
        CorrectAnswers = 0; // sätter antalet rätta svar till 0
        Streak = 0; // sätter antalet rätta svar i rad till 0
    }
    public int AddAnswer(bool isCorrect, int points) //Hanterar spelarens svar och poäng
    {
        if (isCorrect) // kontrollerar om svaret är rätt
        {
            CorrectAnswers++; //Ökar antalet rätta svar med 1
            Streak++; //Ökar antalet rätta svar i rad med 1

            int bonus = Math.Min((Streak - 1) * 5, 20); //Beräknar bonuspoäng baserat på antalet rätta svar i rad, max 50 poäng
            int totalPoints = points + bonus; //lägger ihop frågans poäng och bonus
            Score += totalPoints; //Lägger till poängen i spelarens totalpoäng
            return totalPoints; //Returnerar poängen som spelaren fick för frågan
        }
        else // om svaret är fel
        {
            Streak = 0; // nollställer antalet rätta svar i rad
            return 0; //Returnerar 0 poäng för frågan
        }
    }
}
using System;

// Ett sparat resultat för en spelare efter ett spel
public class HighscoreEntry
{
    public string Name { get; set; } = "";   // spelarens namn
    public int Score { get; set; }           // totala poäng
    public int CorrectAnswers { get; set; }  // antal rätta svar
    public DateTime Date { get; set; }       // datum då spelet spelades
}
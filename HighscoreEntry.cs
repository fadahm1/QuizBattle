using System;

// One saved result for one player in one game.
public class HighscoreEntry
{
    public string Name { get; set; } = "";
    public int Score { get; set; }
    public int CorrectAnswers { get; set; }
    public DateTime Date { get; set; }
}
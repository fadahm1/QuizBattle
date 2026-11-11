using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using QuizBattle;

public class HighscoreManager
{
    private string filePath = "data/highscores.json";

    // Reads all saved results from the file
    public List<HighscoreEntry> Load()
    {
        if (!File.Exists(filePath))
        {
            return new List<HighscoreEntry>();
        }

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<HighscoreEntry>>(json);
        }
        catch
        {
            Console.WriteLine("Could not read the highscore file.");
            return new List<HighscoreEntry>();
        }
    }

    // Writes all results to the file
    private void Save(List<HighscoreEntry> entries)
    {
        Directory.CreateDirectory("data");
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        string json = JsonSerializer.Serialize(entries, options);
        File.WriteAllText(filePath, json);
    }

    // Saves the result of every player, with today's date
    public void AddResults(List<Player> players)
    {
        List<HighscoreEntry> entries = Load();

        foreach (Player player in players)
        {
            HighscoreEntry entry = new HighscoreEntry();
            entry.Name = player.Name;
            entry.Score = player.Score;
            entry.CorrectAnswers = player.CorrectAnswers;
            entry.Date = DateTime.Today;
            entries.Add(entry);
        }

        Save(entries);
    }

    // LINQ: highest score first, then most correct answers, take 10
    public List<HighscoreEntry> GetTopTen()
    {
        return Load()
            .OrderByDescending(e => e.Score)
            .ThenByDescending(e => e.CorrectAnswers)
            .Take(10)
            .ToList();
    }

    // Prints the top list
    public void Display()
    {
        List<HighscoreEntry> top = GetTopTen();

        Console.WriteLine();
        Console.WriteLine("=== Topplista ===");

        if (top.Count == 0)
        {
            Console.WriteLine("Inga resultat sparade än.");
            return;
        }

        for (int i = 0; i < top.Count; i++)
        {
            HighscoreEntry e = top[i];
            Console.WriteLine((i + 1) + ". " + e.Name + "  " + e.Score + " poäng  "
                + e.CorrectAnswers + " rätt  " + e.Date.ToString("yyyy-MM-dd"));
        }
    }
}
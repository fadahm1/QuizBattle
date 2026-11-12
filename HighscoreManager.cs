using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using QuizBattle;

// Sparar och läser topplistan från data/highscores.json
public class HighscoreManager
{
    // Sökvägen till filen där resultaten sparas
    private string filePath = "data/highscores.json";

    // Läser alla sparade resultat från filen
    public List<HighscoreEntry> Load()
    {
        // Finns ingen fil än (första spelet) returnerar vi en tom lista
        if (!File.Exists(filePath))
        {
            return new List<HighscoreEntry>();
        }

        try
        {
            // Läser texten i filen och gör om JSON till en lista med objekt
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<HighscoreEntry>>(json);
        }
        catch
        {
            // Om filen är trasig visar vi ett meddelande istället för att krascha
            Console.WriteLine("Could not read the highscore file.");
            return new List<HighscoreEntry>();
        }
    }

    // Skriver alla resultat till filen som JSON
    private void Save(List<HighscoreEntry> entries)
    {
        // Skapar mappen data om den inte finns
        Directory.CreateDirectory("data");

        // WriteIndented gör att filen blir lätt att läsa (radbrytningar och indrag)
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;

        string json = JsonSerializer.Serialize(entries, options);
        File.WriteAllText(filePath, json);
    }

    // Sparar resultatet för alla spelare efter ett spel, med dagens datum
    public void AddResults(List<Player> players)
    {
        // Hämtar de gamla resultaten först så att vi inte skriver över dem
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

    // LINQ: sorterar och tar de 10 bästa resultaten
    public List<HighscoreEntry> GetTopTen()
    {
        return Load()
            .OrderByDescending(e => e.Score)            // högst poäng först
            .ThenByDescending(e => e.CorrectAnswers)    // vid lika poäng: flest rätta svar först
            .Take(10)                                   // bara de 10 bästa
            .ToList();
    }

    // Skriver ut topplistan i konsolen
    public void Display()
    {
        List<HighscoreEntry> top = GetTopTen();

        Console.WriteLine();
        Console.WriteLine("=== Topplista ===");

        // Om listan är tom finns inga resultat sparade än
        if (top.Count == 0)
        {
            Console.WriteLine("Inga resultat sparade än.");
            return;
        }

        // Skriver ut en rad per resultat med placering, namn, poäng, rätta svar och datum
        for (int i = 0; i < top.Count; i++)
        {
            HighscoreEntry e = top[i];
            Console.WriteLine((i + 1) + ". " + e.Name + "  " + e.Score + " poäng  "
                + e.CorrectAnswers + " rätt  " + e.Date.ToString("yyyy-MM-dd"));
        }
    }
}
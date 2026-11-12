using System;

// Hanterar huvudmenyn
public class Menu
{
    // Visar menyn och returnerar valet (1, 2 eller 3)
    public int Show()
    {
        Console.WriteLine();
        Console.WriteLine("=== Quiz Battle ===");
        Console.WriteLine("1. Spela  2. Topplista  3. Avsluta");
        return ReadNumber("Val: ", 1, 3);
    }

    // Frågar igen och igen tills användaren skriver ett tal mellan min och max
    public static int ReadNumber(string text, int min, int max)
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine();
            int number;

            // TryParse försöker göra om texten till ett tal utan att krascha
            // om användaren skriver t.ex. en bokstav
            if (int.TryParse(input, out number) && number >= min && number <= max)
            {
                return number;
            }

            // Fel inmatning: berätta vad som var fel och fråga igen
            Console.WriteLine("Svara med en siffra mellan " + min + " och " + max + ".");
        }
    }
}
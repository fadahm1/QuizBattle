using System;

// Hanterar huvudmenyn
public class Menu
{
    // Visar menyn med färger och returnerar valet (1, 2 eller 3)
    public int Show()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════╗");
        Console.WriteLine("║        QUIZ BATTLE         ║");
        Console.WriteLine("╚════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" 1. Spela");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("   2. Topplista");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("   3. Avsluta");
        Console.ResetColor();

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
            if (int.TryParse(input, out number) && number >= min && number <= max)
            {
                return number;
            }

            // Fel inmatning visas i rött så att den syns tydligt
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Svara med en siffra mellan " + min + " och " + max + ".");
            Console.ResetColor();
        }
    }
}
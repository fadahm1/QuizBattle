using System;

public class Menu
{
    // Shows the menu and returns 1, 2 or 3
    public int Show()
    {
        Console.WriteLine();
        Console.WriteLine("=== Quiz Battle ===");
        Console.WriteLine("1. Spela  2. Topplista  3. Avsluta");
        return ReadNumber("Val: ", 1, 3);
    }

    // Asks again until the user writes a number between min and max
    public static int ReadNumber(string text, int min, int max)
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine();
            int number;

            if (int.TryParse(input, out number) && number >= min && number <= max)
            {
                return number;
            }

            Console.WriteLine("Svara med en siffra mellan " + min + " och " + max + ".");
        }
    }
}
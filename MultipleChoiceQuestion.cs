using System;
using System.Collections.Generic;

namespace QuizBattle
{
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }

        public MultipleChoiceQuestion(string text, string difficulty, List<string> options, int correctIndex)
            : base(text, difficulty)
        {
            Options = options;
            CorrectIndex = correctIndex;
        }

        public override void DisplayQuestion()
        {
            Console.WriteLine($"\n[Flervalsfråga - {Difficulty.ToUpper()}] (Poäng: {Points})");
            Console.WriteLine(Text);
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {Options[i]}");
            }
        }

        public override bool CheckAnswer(string playerInput)
        {
            if (int.TryParse(playerInput, out int choice))
            {
                return (choice - 1) == CorrectIndex;
            }
            return false;
        }
    }
}
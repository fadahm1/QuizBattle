using System;

namespace QuizBattle
{
    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public TrueFalseQuestion(string text, string difficulty, bool correctAnswer)
            : base(text, difficulty)
        {
            CorrectAnswer = correctAnswer;
        }

        public override void DisplayQuestion()
        {
            Console.WriteLine($"\n[Sant/Falskt - {Difficulty.ToUpper()}] (Poäng: {Points})");
            Console.WriteLine(Text);
            Console.WriteLine("  1. Sant");
            Console.WriteLine("  2. Falskt");
        }

        public override bool CheckAnswer(string playerInput)
        {
            string input = playerInput.Trim().ToLower();
            if (input == "1" || input == "sant" || input == "true")
            {
                return CorrectAnswer == true;
            }
            if (input == "2" || input == "falskt" || input == "false")
            {
                return CorrectAnswer == false;
            }
            return false;
        }
    }
}
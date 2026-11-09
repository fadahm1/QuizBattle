namespace QuizBattle
{
    public abstract class Question : IQuestion
    {
        public string Text { get; set; }
        public string Difficulty { get; set; }

        // Beräkna poäng baserat på svårighetsgrad (Lätt: 10, Medel: 20, Svår: 30)
        public int Points
        {
            get
            {
                return Difficulty switch
                {
                    "easy" => 10,
                    "medium" => 20,
                    "hard" => 30,
                    _ => 10
                };
            }
        }

        protected Question(string text, string difficulty)
        {
            Text = text;
            Difficulty = difficulty;
        }

        public abstract void DisplayQuestion();
        public abstract bool CheckAnswer(string playerInput);
    }
}
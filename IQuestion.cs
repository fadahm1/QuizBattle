namespace QuizBattle
{
    public interface IQuestion
    {
        string Text { get; }
        string Difficulty { get; }
        int Points { get; }
        void DisplayQuestion();
        bool CheckAnswer(string playerInput);
    }
}
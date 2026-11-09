using System;
using System.Collections.Generic;

namespace QuizBattle
{
    public class QuestionManager
    {
        private List<Question> questions = new List<Question>();

        public void AddQuestion(Question q)
        {
            questions.Add(q);
        }

        public List<Question> GetAllQuestions()
        {
            return questions;
        }

        public List<Question> GetQuestionsByDifficulty(string difficulty)
        {
            return questions.FindAll(q => q.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase));
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QuizBattle
{
    // Hjälpklasser för att matcha JSON-strukturen
    public class CategoryDto
    {
        public string Name { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }

    public class QuestionDto
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
        public bool Correct { get; set; }
        public string Difficulty { get; set; }
    }

    public class QuestionFileRoot
    {
        public List<CategoryDto> Categories { get; set; }
    }

    public class QuestionManager
    {
        public Dictionary<string, List<IQuestion>> Categories { get; private set; } = new Dictionary<string, List<IQuestion>>();

        public void LoadQuestions(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Hittade inte frågefilen på: {filePath}");
            }

            string jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<QuestionFileRoot>(jsonString, options);

            if (root?.Categories != null)
            {
                foreach (var catDto in root.Categories)
                {
                    var questionList = new List<IQuestion>();
                    foreach (var qDto in catDto.Questions)
                    {
                        if (qDto.Type == "multipleChoice")
                        {
                            questionList.Add(new MultipleChoiceQuestion(qDto.Text, qDto.Difficulty, qDto.Options, qDto.CorrectIndex));
                        }
                        else if (qDto.Type == "trueFalse")
                        {
                            questionList.Add(new TrueFalseQuestion(qDto.Text, qDto.Difficulty, qDto.Correct));
                        }
                    }
                    Categories[catDto.Name] = questionList;
                }
            }
        }

        public List<Question> GetAllQuestions()
        {
            var allQuestions = new List<Question>();
            foreach (var categoryList in Categories.Values)
            {
                foreach (var q in categoryList)
                {
                    if (q is Question questionObj)
                    {
                        allQuestions.Add(questionObj);
                    }
                }
            }
            return allQuestions;
        }

        public void AddQuestion(Question question)
        {
            if (!Categories.ContainsKey("Övrigt"))
            {
                Categories["Övrigt"] = new List<IQuestion>();
            }
            Categories["Övrigt"].Add(question);
        }
    }
}

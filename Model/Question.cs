using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Country_Guesser.Model
{
    internal class Question
    {
        public string ImagePath { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public string Answer { get; set; }

        public Question(string path, string option1, string option2, string option3,string option4, string answer)
        {
            this.ImagePath = path;
            this.Option1 = option1;
            this.Option2 = option2;
            this.Option3 = option3;
            this.Option4 = option4;
            this.Answer = answer;
        }


        public static List<Question> LoadQuestions(String difficulty)
        {
            List<Question> questions = new();
            List<Question> questionList = new();
            try
            {
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                StreamReader streamReader;
                if (difficulty == "Normal")
                {
                    var file = Path.Combine(projectFolder, @"Resources\dataNormal.txt");
                    streamReader = new StreamReader(file);
                }
                else
                {
                    var file = Path.Combine(projectFolder, @"Resources\dataHard.txt");
                    streamReader = new StreamReader(file);
                }
                
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    String[] parts = line.Split('#');
                    questions.Add(new Question(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]));
                }
                Random rnd = new Random();
                while (questionList.Count < 10)
                {
                    int index = rnd.Next(questions.Count);
                    if (!questionList.Contains(questions[index]))
                    {
                        questionList.Add(questions[index]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return questionList;
        }
    }
}

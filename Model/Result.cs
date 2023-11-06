using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Country_Guesser.Model
{
    internal class Result
    {
        public String Username { get; set; }
        public String Score { get; set; }

        public Result() { }

        public Result(String username, String score)
        {
            this.Username = username;
            this.Score = score;
        }


        public static List<Result> LoadResults(String difficulty)
        {
            List<Result> results = new();
            try
            {
                StreamReader streamReader;
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                if (difficulty == "Normal")
                {
                    var file = Path.Combine(projectFolder, @"resources\resultsNormal.txt");
                     streamReader = new StreamReader(file);
                }
                else
                {
                    var file = Path.Combine(projectFolder, @"resources\resultsHard.txt");
                     streamReader = new StreamReader(file);
                }
                
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    String[] parts = line.Split('#');
                    results.Add(new Result(parts[0], parts[1]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            results = results.OrderByDescending(r => int.Parse(r.Score)).ToList();
            return results;
        }
    }
}

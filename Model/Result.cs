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


        public static List<Result> LoadResults()
        {
            List<Result> results = new();
            try
            {
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                var file = Path.Combine(projectFolder, @"resources\results.txt");
                StreamReader streamReader = new StreamReader(file);
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
            results = results.OrderByDescending(r => r.Score).ToList();
            return results;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Country_Guesser.Model
{
    internal class Picture
    {
        public string ImagePath { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public string Answer { get; set; }

        public Picture(string path, string option1, string option2, string option3,string option4, string answer)
        {
            this.ImagePath = path;
            this.Option1 = option1;
            this.Option2 = option2;
            this.Option3 = option3;
            this.Option4 = option4;
            this.Answer = answer;
        }


        public static List<Picture> LoadPictures()
        {
            List<Picture> pictures = new();
            List<Picture> pictureList = new();
            try
            {
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                var file = Path.Combine(projectFolder, @"Resources\data.txt");
                StreamReader streamReader = new StreamReader(file);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    String[] parts = line.Split('#');
                    pictures.Add(new Picture(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]));
                }
                Random rnd = new Random();
                while (pictureList.Count < 10)
                {
                    int index = rnd.Next(pictures.Count);
                    if (!pictureList.Contains(pictures[index]))
                    {
                        pictureList.Add(pictures[index]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return pictureList;
        }
    }
}

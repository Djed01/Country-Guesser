using Country_Guesser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Country_Guesser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color buttonColor = Color.FromRgb(143, 188, 187);

        private String username;
        private String difficulty;
        private int numOfQuestions = 0;
        private int correctAnswers = 0;
        private int totalScore = 0;
        private DispatcherTimer? timer;
        private int currentTimerValue;
        private Picture? currentPicture;
        private readonly List<Picture> pictures;
        public MainWindow(String username,String difficulty)
        {
            InitializeComponent();
            pictures = Picture.LoadPictures();
            LoadQuestion();
            this.username = username;
        }

        private void LoadQuestion()
        {
            StartTimer();
            currentPicture = pictures[numOfQuestions];
            QuestionNumberLabel.Content = (numOfQuestions + 1).ToString();

            button1.Content = currentPicture.Option1;
            button2.Content = currentPicture.Option2;
            button3.Content = currentPicture.Option3;
            button4.Content = currentPicture.Option4;

            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                bitmap.UriSource = new Uri(projectFolder + "" + currentPicture.ImagePath);
                bitmap.EndInit();
                Image.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Try again." + ex.Message);
            }
            numOfQuestions++;
        }

        private void StartTimer()
        {
            currentTimerValue = 100;
            timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progressBar.Value = currentTimerValue;
            currentTimerValue -= 20;
            if (currentTimerValue < 0)
            {
                timer.Stop();
                if (numOfQuestions == pictures.Count)
                {
                    button1.IsEnabled = false;
                    button2.IsEnabled = false;
                    button3.IsEnabled = false;
                    timer.Stop();
                    progressBar.Foreground = new SolidColorBrush(Color.FromRgb(95, 158, 160));
                    // SaveResult();
                    new ResultWindow(correctAnswers, username,totalScore).Show();
                }
                else
                {
                    LoadQuestion();
                }
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)sender;
            string selectedAnswer = selectedButton.Content.ToString();

            timer.Stop(); // Stop the timer

            // Check if the selected answer is correct
            bool isCorrect = selectedAnswer.Trim() == currentPicture.Answer;

            int timeRemaining = currentTimerValue; // Time remaining when the answer was given
            int timePenalty = 10 - (timeRemaining / 10); // Deduct points based on time taken

            int questionScore = isCorrect ? 10 + timePenalty : 0; // Base score 10 for correct, deduct time penalty
            correctAnswers += isCorrect ? 1 : 0; // Increment correct answer count for correct answers
            totalScore += questionScore; // Update the total score

            if (isCorrect)
            {
                // If the selected answer is correct, set the selected button to green
                selectedButton.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                // If the selected answer is incorrect, find the correct answer button and set it to green
                foreach (Button button in new List<Button> { button1, button2, button3, button4 })
                {
                    string buttonText = button.Content.ToString();
                    if (buttonText.Trim() == currentPicture.Answer)
                    {
                        button.Background = new SolidColorBrush(Color.FromRgb(163, 190, 140));
                    }
                }

                // Set the selected incorrect button to red
                selectedButton.Background = new SolidColorBrush(Color.FromRgb(191, 97, 106));
            }

            // Delay for 2 seconds before loading the next question
            Task.Delay(2000).ContinueWith(_ =>
            {
                // Restore button colors
                Dispatcher.Invoke(() =>
                {
                    foreach (Button button in new List<Button> { button1, button2, button3, button4 })
                    {
                        button.Background = new SolidColorBrush(buttonColor);
                    }
                    if (numOfQuestions == pictures.Count)
                    {
                        timer.Stop();
                        progressBar.Foreground = new SolidColorBrush(Color.FromRgb(95, 158, 160));
                        SaveResult();
                        new ResultWindow(correctAnswers, username,totalScore).Show();
                        this.Close();
                    }
                    else
                    {
                        LoadQuestion();
                    }
                });
            });
        }




        private void SaveResult()
        {
            string filePath = "../../../Resources/results.txt"; // Path to the results file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(username + "#" + totalScore);
            }
        }
    }
}

﻿using Country_Guesser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Country_Guesser
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private string username;
        public ResultWindow(int correctAnswers,String username,int totalScore)
        {
            this.username = username;
            InitializeComponent();
            ResultsDataGrid.ItemsSource = Result.LoadResults();
            ScoreLabel.Content = "Vas skor je: " + totalScore;
            AnswrsLabel.Content = correctAnswers + "/10 Pitanja";
        }

        private void PlayAgainClick(object sender, RoutedEventArgs e)
        {
            new MainWindow(username,"normal").Show();
            this.Close();
        }

        private void EndClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
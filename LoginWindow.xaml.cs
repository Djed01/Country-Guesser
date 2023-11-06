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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                MessageBox.Show("You must enter the name.", "Try again", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (NormalRadioButton.IsChecked == false && HardRadioButton.IsChecked == false) {
                MessageBox.Show("You must chose the dificulty.", "Try again", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (NormalRadioButton.IsChecked == true) {
                    new MainWindow(UsernameTextBox.Text,"Normal").Show();
                    this.Close();
                }
                else if(HardRadioButton.IsChecked == true) {
                    new MainWindow(UsernameTextBox.Text,"Hard").Show();
                    this.Close();
                }

            }
        }
    }
}

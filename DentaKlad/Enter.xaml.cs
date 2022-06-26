using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DentaKlad.Core.Entity;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        User user = new User("Test", "test");
        public Enter()
        {
            InitializeComponent();
        }

        private void Button_enter(object sender, RoutedEventArgs e)
        {
            string username = login.Text;
            string parol = password.Password;
            if (username.Length == 0 || parol.Length == 0)
            {
                login.Foreground = Brushes.Red;
                password.Foreground = Brushes.Red;
                hint.Foreground = Brushes.Red;
                hint.Text = "Данные заполнены не полностью";
            }
            else if (login.Text != user.Username || password.Password != user.Password)
            {
                login.Text = "";
                password.Password = "";
                login.Foreground = Brushes.Red;
                password.Foreground = Brushes.Red;
                hint.Foreground = Brushes.Red;
                hint.Text = "Данные заполнены некорректно";

            }
            else if (login.Text == user.Username || password.Password == user.Password)
            {
                login.Foreground = Brushes.Green;
                password.Foreground = Brushes.Green;
                hint.Foreground = Brushes.Green;
                hint.Text = "Данные заполнены корректно!";
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private void Button_help(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show($"Login: {user.Username}{Environment.NewLine}Password: {user.Password}", "Помощь", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MouseDownstackpanel(object sender, MouseButtonEventArgs e)
        {
            login.Foreground = Brushes.Black;
            password.Foreground = Brushes.Black;
            hint.Foreground = Brushes.Gray;
            hint.Text = "Забыли логин и пароль? Нажмите кнопку Помощь";
        }
    }
}

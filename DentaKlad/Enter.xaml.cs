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
using DentaKlad.Entity;
using System.Windows.Navigation;

namespace DentaKlad
{
    /// <summary>
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        User user = new User("Yura Sanochkin", "thebest");
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
            else if (login.Text != user.username || password.Password != user.password)
            {
                login.Text = "";
                password.Password = "";
                login.Foreground = Brushes.Red;
                password.Foreground = Brushes.Red;
                hint.Foreground = Brushes.Red;
                hint.Text = "Данные заполнены некорректно";
            }
            else if (login.Text == user.username || password.Password == user.password)
            {
                login.Foreground = Brushes.Green;
                password.Foreground = Brushes.Green;
                hint.Foreground = Brushes.Green;
                hint.Text = "Данные заполнены корректно!";
            }
        }

        private void Button_help(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show($"Login: {user.username}{Environment.NewLine}Password: {user.password}", "Помощь", MessageBoxButton.OK, MessageBoxImage.Information);
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

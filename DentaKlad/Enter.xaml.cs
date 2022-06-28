using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DentaKlad.Core;
using DentaKlad.Core.Entity;
using System.Linq;
using System.Security.Cryptography;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        ApplicationContext db = new ApplicationContext();
        public Enter()
        {
            InitializeComponent();
            /*using (db = new ApplicationContext())
            {
                var user1 = new User();
                user1.Username = "test";
                user1.Password = md5.hashPassword("test");

                var user2 = new User();
                user2.Username = "1234";
                user2.Password = md5.hashPassword("1234");
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
            }*/
        }

        private void Button_enter(object sender, RoutedEventArgs e)
        {
            string username = login.Text;
            string parol = md5.hashPassword(password.Password);
            if (username.Length == 0 || password.Password.Length == 0)  
            {
                login.Foreground = Brushes.Red;
                password.Foreground = Brushes.Red;
                hint.Foreground = Brushes.Red;
                hint.Text = "Данные заполнены не полностью";
            }
            else if (db.Users.FirstOrDefault(c => c.Username == username & c.Password == parol) == null)
            {
                login.Text = "";
                password.Password = "";
                login.Foreground = Brushes.Red;
                password.Foreground = Brushes.Red;
                hint.Foreground = Brushes.Red;
                hint.Text = "Данные заполнены некорректно";

            }
            else if (db.Users.FirstOrDefault(c => c.Username == username & c.Password == parol) != null)
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
            var usname = "1234";
            var paswrd = "1234";
            MessageBox.Show($"Login: {usname}{Environment.NewLine}Password: {paswrd}", "Помощь", MessageBoxButton.OK, MessageBoxImage.Information);
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

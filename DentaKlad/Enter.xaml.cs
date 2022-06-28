using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DentaKlad.Core;
using DentaKlad.Core.Entity;
using System.Linq;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        ApplicationContext db;
        public Enter()
        {
            InitializeComponent();
            using (db = new ApplicationContext())
            {
                var user1 = new User();
                user1.Username = "test";
                user1.Password = "test";

                /*var user2 = new User()
                {
                    Username = "1234",
                    Password = "1234"
                };*/
                db.Users.Add(user1);
                /*db.Users.Add(user2);*/
                db.SaveChanges();
            }
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
            var usname = db.Users.First();
            var paswrd = db.Users.First();
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

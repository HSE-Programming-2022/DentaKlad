using DentaKlad.Core;
using DentaKlad.Core.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Depts(object sender, RoutedEventArgs e)
        {
            Departments departments = new Departments();
            departments.Show();
        }

        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            Clients clients = new Clients();
            clients.Show();
        }

        private void Button_Click_Services(object sender, RoutedEventArgs e)
        {
            //Services services = new Services();
            //services.Show();
        }

        private void Button_Click_Doctors(object sender, RoutedEventArgs e)
        {
            //Doctor doctors = new Doctors();
            //doctors.Show();
        }

        private void Button_Click_Appointments(object sender, RoutedEventArgs e)
        {
            //Appointments appointment = new Appointments();
            //appointment.Show();
        }
    }
}

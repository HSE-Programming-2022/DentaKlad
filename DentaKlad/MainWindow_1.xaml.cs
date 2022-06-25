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
    public partial class MainWindow_1 : Window
    {
        public MainWindow_1()
        {
            InitializeComponent();
            Recording recording = new Recording();
        }

        public class Recording
        {
            public string id { get; set; }
            public string name { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string doctor { get; set; }
            public string service { get; set; }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Recording recording = new Recording();
            recording.name = nameTB.Text;
            recording.date = dateTB.Text;
            recording.time = timeTB.Text;
            recording.doctor = doctorTB.Text;
            recording.service = serviceTB.Text;
            bd.Items.Add(recording);
        }
    }
}

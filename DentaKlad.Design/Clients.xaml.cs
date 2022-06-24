using DentaKlad.Core;
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

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        Context context;
        List<TextBox> textBoxes;
        public Clients()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>
            {
                NameBox,
                NumberBox,
                EmailBox,
                SNBox,
                AddressBox
            };
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }
            LoadData();
        }

        public void LoadData()
        {
            context = new Context();
            ClientTable.ItemsSource = context.Clients.ToList();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ClientTable.SelectedItem != null)
            {
                if (!context.Appointments.ToList().Where(a => a.Client == (ClientTable.SelectedItem as Client)).Any())
                {
                    context.Clients.Remove(ClientTable.SelectedItem as Client);
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить данные о клиенте, так как есть связанные записи.");
                }

            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            
            if (!textBoxes.Where(b => String.IsNullOrEmpty(b.Text)).Any())
            {
                var newClient = new Client(textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text, textBoxes[3].Text, textBoxes[4].Text);
                foreach (TextBox textBox in textBoxes)
                {
                    textBox.Clear();
                }
                if (context.Clients.Where(c => c.Name == newClient.Name & c.PhoneNumber == newClient.PhoneNumber & c.Email == newClient.Email & c.SeriesAndNumber == newClient.SeriesAndNumber & c.Address == newClient.Address).Any())
                {
                    MessageBox.Show("Информация об этом клиенте уже есть.");
                }
                else
                {
                    context.Clients.Add(newClient);
                    context.SaveChanges();
                    LoadData();
                }                
            }
        }

        
    }
}

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
using System.Windows.Shapes;

namespace DentaKlad.Design
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        ApplicationContext context;
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
            context = new ApplicationContext();
            ClientTable.ItemsSource = context.Clients.ToList();
        }

        public bool ClientExists(Client client)
        {
            if (context.Clients.Where(c => c.Name == client.Name & c.PhoneNumber == client.PhoneNumber & c.Email == client.Email & c.SeriesAndNumber == client.SeriesAndNumber & c.Address == client.Address).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTheSame(Client client)
        {
            if (textBoxes[0].Text == client.Name & textBoxes[1].Text == client.PhoneNumber & textBoxes[2].Text == client.Email & textBoxes[3].Text == client.SeriesAndNumber & textBoxes[4].Text == client.Address)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                if (ClientExists(newClient))
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

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            textBoxes[0].Text = (ClientTable.SelectedItem as Client).Name;
            textBoxes[1].Text = (ClientTable.SelectedItem as Client).PhoneNumber;
            textBoxes[2].Text = (ClientTable.SelectedItem as Client).Email;
            textBoxes[3].Text = (ClientTable.SelectedItem as Client).SeriesAndNumber;
            textBoxes[4].Text = (ClientTable.SelectedItem as Client).Address;
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {
            if (ClientTable.SelectedItem != null & !textBoxes.Where(b => String.IsNullOrEmpty(b.Text)).Any())
            {
                if (!ClientExists(ClientTable.SelectedItem as Client) | IsTheSame(ClientTable.SelectedItem as Client))
                {
                    (ClientTable.SelectedItem as Client).Name = textBoxes[0].Text;
                    (ClientTable.SelectedItem as Client).PhoneNumber = textBoxes[1].Text;
                    (ClientTable.SelectedItem as Client).Email = textBoxes[2].Text;
                    (ClientTable.SelectedItem as Client).SeriesAndNumber = textBoxes[3].Text;
                    (ClientTable.SelectedItem as Client).Address = textBoxes[4].Text;
                    foreach (TextBox textBox in textBoxes)
                    {
                        textBox.Clear();
                    }
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Информация об этом отделении уже есть.");
                }
            }
        }


    }
}

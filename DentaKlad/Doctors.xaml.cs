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
    /// Логика взаимодействия для Doctors.xaml
    /// </summary>
    public partial class Doctors : Window
    {
        ApplicationContext context;
        public Doctors()
        {
            InitializeComponent();
            NameBox.Clear();
            LoadData();
        }

        public void LoadData()
        {
            context = new ApplicationContext();
            DoctorTable.ItemsSource = context.Doctors.ToList();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (DoctorTable.SelectedItem != null)
            {
                if (!context.Appointments.ToList().Where(a => a.Doctor == (DoctorTable.SelectedItem as Doctor)).Any())
                {
                    context.Doctors.Remove(DoctorTable.SelectedItem as Doctor);
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить данные о враче, так как есть связанные записи.");
                }

            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameBox.Text) & !context.Doctors.Where(d => d.Name == NameBox.Text).Any())
            {
                var newDoctor = new Doctor(NameBox.Text);
                NameBox.Clear();
                context.Doctors.Add(newDoctor);
                context.SaveChanges();
                LoadData();
            }
            else if (context.Doctors.Where(d => d.Name == NameBox.Text).Any())
            {
                NameBox.Clear();
                MessageBox.Show("Информация об этом враче уже есть.");
            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            NameBox.Text = (DoctorTable.SelectedItem as Doctor).Name;
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {
            if (DoctorTable.SelectedItem != null & !String.IsNullOrEmpty(NameBox.Text))
            {
                if (!context.Doctors.Where(d => d.Name == NameBox.Text).Any() | NameBox.Text == (DoctorTable.SelectedItem as Doctor).Name)
                {
                    (DoctorTable.SelectedItem as Doctor).Name = NameBox.Text;
                    NameBox.Clear();
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    NameBox.Clear();
                    MessageBox.Show("Информация об этом враче уже есть.");
                }
            }
        }
    }
}

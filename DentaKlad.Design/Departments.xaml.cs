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
    /// Логика взаимодействия для Departments.xaml
    /// </summary>
    public partial class Departments : Window
    {
        Context context;
        public Departments()
        {
            InitializeComponent();
            NameBox.Clear();
            LoadData();
        }

        public void LoadData()
        {
            context = new Context();
            DeptTable.ItemsSource = context.Departments.ToList();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (DeptTable.SelectedItem != null)
            {
                if ( !context.Services.ToList().Where(s => s.Department == (DeptTable.SelectedItem as Department)).Any() )
                {
                    context.Departments.Remove(DeptTable.SelectedItem as Department);
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить данные об отделении, так как есть связанные процедуры.");
                }
                
            }   
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameBox.Text) & !context.Departments.Where(d => d.Name == NameBox.Text).Any())
            {
                var newDept = new Department(NameBox.Text);
                NameBox.Clear();
                context.Departments.Add(newDept);
                context.SaveChanges();
                LoadData();
            }
            else if (context.Departments.Where(d => d.Name == NameBox.Text).Any())
            {
                NameBox.Clear();
                MessageBox.Show("Информация об этом отделении уже есть.");
            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            NameBox.Text = (DeptTable.SelectedItem as Department).Name;
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {
            if (DeptTable.SelectedItem != null & !String.IsNullOrEmpty(NameBox.Text))
            {
                if (!context.Departments.Where(d => d.Name == NameBox.Text).Any() | NameBox.Text == (DeptTable.SelectedItem as Department).Name)
                {
                    (DeptTable.SelectedItem as Department).Name = NameBox.Text;
                    NameBox.Clear();
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    NameBox.Clear();
                    MessageBox.Show("Информация об этом отделении уже есть.");
                }
            }            
        }
    }
}

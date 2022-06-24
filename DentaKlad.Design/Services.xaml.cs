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
    public partial class Services : Window
    {
        Context context;
        List<TextBox> textBoxes;
        public Services()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>
            {
                NameBox,
                PriceBox
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
            
            var linqQuery = context.Departments
                            .Join(context.Services, department => department, service => service.Department, (department, service) => new { Id = service.Id, Name = service.Name, Price = service.Price, Department = department.Name });

            ServiceTable.ItemsSource = linqQuery.ToList();

            Depts.ItemsSource = context.Departments.Select(d => d.Name).ToList();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ServiceTable.SelectedItem != null)
            {
                if (!context.Appointments.ToList().Where(a => a.Service == (ServiceTable.SelectedItem as Service)).Any())
                {
                    int index = int.Parse(ServiceTable.SelectedItem.ToString().Split()[3].TrimEnd(','));
                    context.Services.Remove(context.Services.Where(s => s.Id == index).FirstOrDefault());
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить данные о процедуре, так как есть связанные записи.");
                }

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (!Double.TryParse(textBoxes[1].Text, out double price) | price <= 0)
            {
                textBoxes[1].Clear();
                textBoxes[1].Focus();
            }

            if (!textBoxes.Where(b => String.IsNullOrEmpty(b.Text)).Any() & Depts.SelectedItem != null & Double.TryParse(textBoxes[1].Text, out price) & price > 0)
            {
                var newService = new Service(textBoxes[0].Text, Double.Parse(textBoxes[1].Text), context.Departments.Where(d => d.Name == Depts.SelectedItem).FirstOrDefault());
                
                if (context.Services.Where(s => s.Name == newService.Name & s.Price == newService.Price & s.Department.Name == newService.Department.Name).Any())
                {
                    MessageBox.Show("Информация об этой услуге уже есть.");
                }
                else
                {
                    foreach (TextBox textBox in textBoxes)
                    {
                        textBox.Clear();
                    }
                    context.Services.Add(newService);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }
    }
}

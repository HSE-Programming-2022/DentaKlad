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

        public bool ServiceExists(Service service)
        {
            if (context.Services.Where(s => s.Name == service.Name & s.Price == service.Price & s.Department.Name == service.Department.Name).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ServiceExistsByTextBoxes()
        {
            string name = textBoxes[0].Text;
            string price = textBoxes[1].Text;
            string deptName = Depts.SelectedItem.ToString();

            if (context.Services.Where(s => s.Name == name & s.Price.ToString() == price & s.Department.Name == deptName).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public bool IsTheSame(Service service)
        {
            if (textBoxes[0].Text == service.Name & textBoxes[1].Text == service.Price.ToString() & Depts.SelectedItem.ToString() == service.Department.Name)
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
                

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (!Double.TryParse(textBoxes[1].Text, out double price) | price <= 0)
            {
                textBoxes[1].Clear();
                textBoxes[1].Focus();
            }

            if (!textBoxes.Where(b => String.IsNullOrEmpty(b.Text)).Any() & Depts.SelectedItem != null & Double.TryParse(textBoxes[1].Text, out price) & price > 0)
            {
                var newService = new Service(textBoxes[0].Text, Double.Parse(textBoxes[1].Text), context.Departments.Where(d => d.Name == Depts.SelectedItem.ToString()).FirstOrDefault());
                
                if (ServiceExists(newService))
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

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(ServiceTable.SelectedItem.ToString().Split()[3].TrimEnd(','));
            var service = context.Services.Where(s => s.Id == index).FirstOrDefault();
            textBoxes[0].Text = service.Name;
            textBoxes[1].Text = service.Price.ToString();
            Depts.SelectedItem = service.Department.Name;
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {
            if (!Double.TryParse(textBoxes[1].Text, out double price) | price <= 0)
            {
                textBoxes[1].Clear();
                textBoxes[1].Focus();
            }
            if (Depts.SelectedItem != null & !textBoxes.Where(b => String.IsNullOrEmpty(b.Text)).Any() & Double.TryParse(textBoxes[1].Text, out price) & price > 0)
            {
                int index = int.Parse(ServiceTable.SelectedItem.ToString().Split()[3].TrimEnd(','));
                var selectedService = context.Services.Where(s => s.Id == index).FirstOrDefault();
                if (!ServiceExistsByTextBoxes() | IsTheSame(selectedService))
                {
                    selectedService.Name = textBoxes[0].Text;
                    selectedService.Price = Double.Parse(textBoxes[1].Text);
                    selectedService.Department = context.Departments.Where(d => d.Name == Depts.SelectedItem.ToString()).FirstOrDefault();

                    foreach (TextBox textBox in textBoxes)
                    {
                        textBox.Clear();
                    }
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Информация об этой услуге уже есть.");
                }
            }
        }

    }
}

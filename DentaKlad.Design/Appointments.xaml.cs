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
    public partial class Appointments : Window
    {
        Context context;
        List<ListBox> listBoxes;
        public Appointments()
        {
            InitializeComponent();
            listBoxes = new List<ListBox>
            {
                Clients,
                Doctors,
                Services
            };
            DateTimeBox.Clear();
            LoadData();
        }

        public void LoadData()
        {
            context = new Context();

            var linqQuery = context.Doctors
                            .Join(context.Appointments, doctor => doctor, apt => apt.Doctor, (doctor, apt) => new { Apt = apt, Doctor = doctor })
                            .Join(context.Clients, docApt => docApt.Apt.Client, client => client, (docApt, client) => new { Apt = docApt.Apt, Doctor = docApt.Doctor, Client = client })
                            .Join(context.Services, clientDocApt => clientDocApt.Apt.Service, service => service, (clientDocApt, serice) => new { Id = clientDocApt.Apt.Id, DateAndTime = clientDocApt.Apt.DateAndTime, Doctor = clientDocApt.Doctor.Name, Client = clientDocApt.Client.Name, Service = serice.Name });

            AptTable.ItemsSource = linqQuery.ToList();

            Clients.ItemsSource = context.Clients.Select(c => c.Name).ToList();
            Doctors.ItemsSource = context.Doctors.Select(d => d.Name).ToList();
            Services.ItemsSource = context.Services.Select(s => s.Name).ToList();

        }

        public bool AptExists(Appointment apt)
        {
            if (context.Appointments.Where(a => a.DateAndTime == apt.DateAndTime & a.Client.Name == apt.Client.Name & a.Doctor.Name == apt.Doctor.Name & a.Service.Name == apt.Service.Name).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AptExistsByTextBoxes()
        {
            string dt = DateTimeBox.Text;
            string clientName = Clients.SelectedItem.ToString();
            string serviceName = Services.SelectedItem.ToString();
            string doctorName = Doctors.SelectedItem.ToString();

            if (context.Appointments.Where(a => a.DateAndTime.ToString() == dt & a.Client.Name == clientName & a.Service.Name == serviceName & a.Doctor.Name == doctorName).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTheSame(Appointment apt)
        {
            if (DateTimeBox.Text == apt.DateAndTime.ToString() & Clients.SelectedItem.ToString() == apt.Client.Name & Doctors.SelectedItem.ToString() == apt.Doctor.Name & Services.SelectedItem.ToString() == apt.Service.Name)
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
            if (AptTable.SelectedItem != null)
            {
                int index = int.Parse(AptTable.SelectedItem.ToString().Split()[3].TrimEnd(','));
                context.Appointments.Remove(context.Appointments.Where(apt => apt.Id == index).FirstOrDefault());
                context.SaveChanges();
                LoadData();
                //MessageBox.Show(AptTable.SelectedItem.ToString().Split()[3].TrimEnd(','));


            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(DateTimeBox.Text, out DateTime dt) | dt <= DateTime.Now)
            {
                DateTimeBox.Clear();
                DateTimeBox.Focus();
            }
            if (!listBoxes.Where(b => b.SelectedItem == null).Any() & !String.IsNullOrEmpty(DateTimeBox.Text))
            {
                var newApt = new Appointment(context.Clients.Where(c => c.Name == Clients.SelectedItem).FirstOrDefault(), context.Services.Where(s => s.Name == Services.SelectedItem).FirstOrDefault(), context.Doctors.Where(d => d.Name == Doctors.SelectedItem).FirstOrDefault(), DateTimeBox.Text);

                if (context.Appointments.Where(a => a.DateAndTime == newApt.DateAndTime & a.Client.Name == newApt.Client.Name & a.Doctor.Name == newApt.Doctor.Name & a.Service.Name == newApt.Service.Name).Any())
                {
                    MessageBox.Show("Информация об этой записи уже есть.");
                }
                else
                {
                    DateTimeBox.Clear();
                    context.Appointments.Add(newApt);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(AptTable.SelectedItem.ToString().Split()[3].TrimEnd(','));
            var service = context.Appointments.Where(apt => apt.Id == index).FirstOrDefault();
            listBoxes[0].SelectedItem = service.Client.Name;
            listBoxes[1].SelectedItem = service.Doctor.Name;
            listBoxes[2].SelectedItem = service.Service.Name;
            DateTimeBox.Text = service.DateAndTime.ToString();
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(DateTimeBox.Text, out DateTime dt) | dt <= DateTime.Now)
            {
                DateTimeBox.Clear();
                DateTimeBox.Focus();
            }
            if (!listBoxes.Where(b => b.SelectedItem == null).Any() & !String.IsNullOrEmpty(DateTimeBox.Text))
            {
                int index = int.Parse(AptTable.SelectedItem.ToString().Split()[3].TrimEnd(','));
                var selectedAppointment = context.Appointments.Where(a => a.Id == index).FirstOrDefault();
                if (!AptExistsByTextBoxes() | IsTheSame(selectedAppointment))
                {
                    selectedAppointment.DateAndTime =DateTime.Parse(DateTimeBox.Text);
                    selectedAppointment.Client = context.Clients.Where(c => c.Name == Clients.SelectedItem.ToString()).FirstOrDefault();
                    selectedAppointment.Doctor = context.Doctors.Where(d => d.Name == Doctors.SelectedItem.ToString()).FirstOrDefault();
                    selectedAppointment.Service = context.Services.Where(s => s.Name == Services.SelectedItem.ToString()).FirstOrDefault();

                    DateTimeBox.Clear();
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Информация об этой записи уже есть.");
                }
            }
        }

    }
}

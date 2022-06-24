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
        public Appointments()
        {
            InitializeComponent();
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
    }
}

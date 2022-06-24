using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaKlad.Core
{
    public class Appointment
    {
        public int Id { get; set; }
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
        public virtual Doctor Doctor { get; set; }
        public DateTime DateAndTime { get; set; }

        public Appointment()
        {

        }

        public Appointment(Client client, Service service, Doctor doctor, string dt)
        {
            Client = client;
            Service = service;
            Doctor = doctor;
            DateAndTime = DateTime.Parse(dt);
        }
    }
}

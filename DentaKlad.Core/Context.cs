using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaKlad.Core
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public Context() : base("DBConnection")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DentaKlad.Entity;
using MySql.Data.EntityFramework;

namespace DentaKlad
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("MySqlConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaKlad.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SeriesAndNumber { get; set; }
        public string Address { get; set; }
        public Client()
        {

        }
        public Client(string name, string number, string email, string sn, string address)
        {
            Name = name;
            PhoneNumber = number;
            Email = email;
            SeriesAndNumber = sn;
            Address = address;
        }
    }
}

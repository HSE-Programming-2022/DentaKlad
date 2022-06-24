using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaKlad.Core
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual Department Department { get; set; }
        public Service()
        {

        }
        public Service(string name, double price, Department dept)
        {
            Name = name;
            Price = price;
            Department = dept;
        }
    }
}

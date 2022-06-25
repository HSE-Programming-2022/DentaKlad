using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaKlad.Core.Entity
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Doctor(string name)
        {
            Name = name;
        }

        public Doctor()
        {

        }
    }
}

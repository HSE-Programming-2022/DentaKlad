using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaKlad.Core
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department()
        {

        }

        public Department(string name)
        {
            Name = name;
        }
    }
}

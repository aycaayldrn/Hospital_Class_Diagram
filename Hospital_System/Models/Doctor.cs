using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public abstract class Doctor 
    {
        int Id { get; set; }
        string Name { get; set; }

        public Doctor(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Doctor(){}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal interface Appointment
    {
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }

    }
}

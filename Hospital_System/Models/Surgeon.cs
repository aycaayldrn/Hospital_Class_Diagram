using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Surgeon
    {
        public List<string> Surgeries { get; set; }
        public int MaxSurgeriesPerShift { get; set; } = 2;
    }
}

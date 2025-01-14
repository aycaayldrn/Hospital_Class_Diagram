using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Resident: Doctor
    {
        public Resident(int id, string name, List<Shift> initialShifts)
            : base(id, name, initialShifts)
        {
        }
    }
}

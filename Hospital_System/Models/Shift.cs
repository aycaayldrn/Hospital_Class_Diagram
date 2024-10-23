using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Shift
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Day {  get; set; }

        public TimeSpan GetShiftDuration()
        {
            return EndTime - StartTime;
        }
        
    }
}

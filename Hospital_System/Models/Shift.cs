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
        public string Day { get; set; }

        public Shift(DateTime startTime, DateTime endTime, string day )
        {
            if(endTime <= startTime)
            {
                throw new ArgumentException("End time must be after start time");
            }
            if (string.IsNullOrEmpty(day))
            {
                throw new ArgumentException("Day cannot be empty");
            }

            StartTime = startTime; EndTime = endTime;
            Day = day;
        }

        public TimeSpan GetShiftDuration()
        {
            return EndTime - StartTime;
        }

    }
}

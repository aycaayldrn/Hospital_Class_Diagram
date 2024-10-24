using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Appointment
    {
        private DateOnly _date;
        public DateOnly Date
        {
            get => _date;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Date cannot be null");
                }
                _date = value;
            }
        }

        private TimeOnly _time;
        public TimeOnly Time
        {
            get => _time;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Date cannot be null");
                }
                _time = value;

            }
        }

        public Appointment(DateOnly date, TimeOnly time) {
            Date = date;
            Time = time;
        }

        
    }

    internal interface IAppointment
    {
        DateOnly Date { get; set; }
        TimeOnly Time { get; set; }
    }
}

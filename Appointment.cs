using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Appointment
    {
        
        private static List<Appointment> _appointmentList = new List<Appointment>();
        private DateTime _date;
        public DateTime Date
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

        private DateTime _time;
        public DateTime Time
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

        public Appointment(DateTime date, DateTime time) {
            Date = date;
            Time = time;
            addAppointment(this);
        }
        public Appointment(){}
    
        
        private static void addAppointment(Appointment appointment)
        {
            if (appointment== null)
            {
                throw new ArgumentException("Apointment cannot be null");
            }
           

            if (_appointmentList.Exists(a=>a.Equals(appointment)))
            {
                throw new InvalidOperationException("Appointment already added");
            }
            _appointmentList.Add(appointment);
        }


        private static void removeAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentException("Appointment cannot be null");
            }

            if (!_appointmentList.Contains(appointment))
            {
                throw new InvalidOperationException("appointment not found!");
            }
            _appointmentList.Remove(appointment);
        }

        public static List<Appointment> GetAppointments()
        {
            return new List<Appointment>(_appointmentList.AsReadOnly());
        }
        public static void SetAppointments(List<Appointment> containerAppointments)
        {
            _appointmentList =containerAppointments?? new List<Appointment>();
        }

        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Appointment))
            {
                return false;
            }

            Appointment a = (Appointment)obj;

            return this._time == a._time && this._date == a._date;
        }

        public override int GetHashCode()
        {
            return _time.GetHashCode()^_date.GetHashCode();
        }

        public override string ToString()
        {
            return "time: " + _time + " " + "date: " + _date;
        }
    }

    internal interface IAppointment
    {
        DateTime Date { get; set; }
        DateTime Time { get; set; }
    }
    
    
    
}

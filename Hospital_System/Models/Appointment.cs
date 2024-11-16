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
        [Serializable]
        public enum AppointmentType
        {
            Surgery,
            FollowUp,
            Consultation
        }

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
                }else if (value < DateTime.Now)
                {
                    throw new ArgumentException("Date cannot be earlier than today");
                }
                _date = value;
            }
        }

        public AppointmentType Type { get; set; }

        public object AssignedDoctor { get; set; }

        public Appointment(DateTime date, AppointmentType type, object assignedDoctor) 
        {
            if(type == AppointmentType.Surgery && assignedDoctor is not Surgeon)
            {
                throw new InvalidOperationException("Only surgeons can be assigned to surgery appointments. ");
            }
            
            Date = date;
            Type = type;
            AssignedDoctor = assignedDoctor;
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


        public static void removeAppointment(Appointment appointment)
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
            _appointmentList = containerAppointments.Count == 0 ? new List<Appointment>(): containerAppointments;
        }

        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Appointment))
            {
                return false;
            }

            Appointment a = (Appointment)obj;

            return _date == a._date;
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode();
        }

        public override string ToString()
        {
            return "date: " + _date;
        }
    }
    
 
    
    
    
}

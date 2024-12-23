using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public abstract class Nurse_Shift
    {
        public Nurse Nurse;
        public Patient Patient;
        public DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("Start time cannot be set to a past date/time.");
                }
                _startTime = value;
            }
        }

        public DateTime _endTime;
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("End time cannot be set to a past date/time.");
                }
                if (value < StartTime)
                {
                    throw new ArgumentException("End time cannot be earlier than start time.");
                }
                _endTime = value;
            }
        }

        protected Nurse_Shift( Nurse nurse, Patient patient, DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            Patient = patient;
            Nurse = nurse;

            //Nurse.AddShiftToNurseForPatient(this);
            //Patient.AddShiftToNurseForPatient(this);
        }

        public override string ToString()
        {
            return "Start date: " + StartTime + "End date: " + EndTime;
        }
    }
}

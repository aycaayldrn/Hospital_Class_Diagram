using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Nurse_Shift
    {
        public List<Nurse> Nurses;
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

        public Nurse_Shift( List<Nurse> nurseList, Patient patient, DateTime startTime, DateTime endTime)
        {
            if(nurseList == null || !nurseList.Any())
            {
                throw new ArgumentNullException(nameof(patient), "Nurse cannot be null or empty.");
            }

            if(patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");
            }

            if (startTime >= endTime)
            {
                throw new ArgumentException("Start time must be earlier than end time.");
            }

            StartTime = startTime;
            EndTime = endTime;
            Patient = patient;
            
            foreach (Nurse nurse in nurseList)
            {
                if (nurse == null)
                {
                    throw new ArgumentNullException(nameof(nurse), " Nurse cannot be null");
                }
                nurse.GetNurseShiftsInternal().Add(this);
            }

            Patient.GetNurseShiftsInternal().Add(this);
        }

        public override string ToString()
        {
            return "Start date: " + StartTime + "End date: " + EndTime;
        }

    }
}

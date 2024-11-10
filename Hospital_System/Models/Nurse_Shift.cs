using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public abstract class Nurse_Shift
    {
        public int ShiftId { get; set; }
        public int NurseId { get; set; }
        public int PatientId { get; set; }

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

        protected Nurse_Shift(int shiftId, int nurseId, int patientId, DateTime startTime, DateTime endTime)
        {
            ShiftId = shiftId;
            NurseId = nurseId;
            PatientId = patientId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString()
        {
            return "Shift Id: " + ShiftId + "Nurse Id :" + NurseId + "Patient ID: " + PatientId + "Start date: " + StartTime + "End date: " + EndTime;
        }
    }
}

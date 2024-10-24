using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Shift
    {

        public Shift()
        {
            
        }
        private static List<Shift> _shiftList = new List<Shift>();
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
            AddShift(this);
        }

        public TimeSpan GetShiftDuration()
        {
            return EndTime - StartTime;
        }
        
        
        private static void AddShift(Shift shift)
        {
            if (shift == null)
            {
                throw new ArgumentException("Shift cannot be null");
            }

            if (_shiftList.Exists(s => s.Equals(shift)))
            {
                throw new InvalidOperationException("Shift already added");
            }

            _shiftList.Add(shift);
        }
        
        
        public static void RemoveShift(Shift shift)
        {
            if (shift == null)
            {
                throw new ArgumentException("Shift cannot be null");
            }

            if (!_shiftList.Contains(shift))
            {
                throw new InvalidOperationException("Shift not found");
            }

            _shiftList.Remove(shift);
        }
        
        
        public static IReadOnlyList<Shift> GetShifts()
        {
            return _shiftList.AsReadOnly();
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Shift))
            {
                return false;
            }

            Shift other = (Shift)obj;

            return this.StartTime == other.StartTime &&
                   this.EndTime == other.EndTime &&
                   string.Equals(this.Day, other.Day, StringComparison.OrdinalIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(StartTime, EndTime, Day.ToLowerInvariant());
        }
        
        public override string ToString()
        {
            return "Shift:"+Day+" Start Time: "+StartTime+" End Time: "+EndTime+ "Duration: "+GetShiftDuration();
        }

        public static void SetShifts(List<Shift> containerShifts)
        {
            _shiftList = containerShifts ?? new List<Shift>();
        }
    }
}

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
        private Staff _staff;

        public Staff Staff
        {
            get { return _staff; }
        }

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

//==================================================================================================================
//Association: shift-staff agregation


        public void asssignStaffToShift(Staff staff)
        {
            if (staff==null)
            {
                throw new ArgumentException("Staff cannot be null");
            }
            
            if (_staff!= null)
            {
                throw new InvalidOperationException("Staff already assigned to department");
            }

            _staff = staff;
            if (!staff.GetShifts().Contains(this))
            {
                staff.addShiftToStaff(this);
            }
        }


        public void changestaff(Staff difrentStaff)
        {
            {
                if (difrentStaff == null)
                {
                    throw new ArgumentException("Staff cannot be null");
                }

                if (_staff == difrentStaff)
                {
                    throw new InvalidOperationException("staffs are the same!");
                }

                if (_staff != null)
                {
                    _staff.removeShiftFromStaff(this);
                }

                difrentStaff.addShiftToStaff(this);
                _staff = difrentStaff;
            }

        }

        public void deleteStaff()
        {
            if (_staff != null && _staff.GetShifts().Contains(this))
            {
                _staff.removeShiftFromStaff(this);
            }
            _staff = null;

           
        }

//==================================================================================================================
        internal static void AddShift(Shift shift)
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

        // public static void SetShifts(List<Shift> containerShifts)
        // {
        //     _shiftList = containerShifts ?? new List<Shift>();
        // }

        public static void LoadExtent(IEnumerable<Shift> containerShifts)
        {
            _shiftList.Clear();
            
            foreach (var shift in containerShifts)
            {

                new Shift(shift.StartTime,shift.EndTime,shift.Day);
            }
        }
    }
}

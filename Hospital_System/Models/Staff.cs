using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Staff
    {
        public Staff(){}
        private static List<Staff> _staffList = new List<Staff>();

        private List<Appointment> _appointments = new List<Appointment>();
        private List<Shift> _shifts = new List<Shift>();
        public IReadOnlyList<Appointment> Appointments => _appointments.AsReadOnly();

        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name of staff can't be empty");
                }
                _name = value;
            }
        }

        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Position of staff can't be empty");
                }
                _position = value;
            }
        }

        private static readonly int MaxWorkingHours = 12;

        public Staff(int id, string name, string position, List<Shift> initialShifts)
        {
            Id = id;
            Name = name;
            Position = position;

            if(initialShifts == null || initialShifts.Count == 0)
            {
                throw new ArgumentException("A staff member must be assigned to at least one shift");
            }

            if (initialShifts.Distinct().Count() != initialShifts.Count)
            {
                throw new ArgumentException("Duplicate shifts are not allowed.");
            }

            foreach (var shift in initialShifts)
            {
                if (!_shifts.Contains(shift)){
                    addShiftToStaff(shift);
                } 
            }
            
            AddStaff(this);
        }
        
//==================================================================================================================
//Associations: Agregation:Staff-part of-Shift

        public void addShiftToStaff(Shift shift)
        {
            if (shift==null)
            {
                throw new ArgumentException("Shift can't be null");
            }

            if (_shifts.Contains(shift))
            {
                throw new InvalidOperationException("Shift is already assigned to this staff");
                
            }
            _shifts.Add(shift);
            if (shift.Staff==null||!shift.Staff.Equals(this))
            {
                shift.asssignStaffToShift(this);
            }
            
        }
        public void removeShiftFromStaff(Shift shift)
        {
            if (shift==null)
            {
                throw new ArgumentException("Shift can't be null");
            }

            if (!_shifts.Contains(shift))
            {
                throw new InvalidOperationException("No such shift in list");
                
            }
            _shifts.Remove(shift);
            if (shift.Staff == null || shift.Staff.Equals(this))
            {
                shift.deleteStaff();
            }
        }
        public IReadOnlyList<Shift> GetShifts()
        {
            return _shifts.AsReadOnly();
        }

//====================================================================================================================
        internal static void AddStaff(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentException("Staff cannot be null");
            }

            if (_staffList.Exists(s => s.Equals(staff)))
            {
                throw new InvalidOperationException("Staff already added");
            }

            _staffList.Add(staff);
        }
        
        
        public static void RemoveStaff(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentException("Staff cannot be null");
            }

            if (!_staffList.Contains(staff))
            {
                throw new InvalidOperationException("Staff not found");
            }

            _staffList.Remove(staff);
        }
        
        public static IReadOnlyList<Staff> GetStaffMembers()
        {
            return _staffList.AsReadOnly();
        }

        //==================================================================================================================
        //Staff-supports- Appointments
            public void AddAppointmentToStaff(Appointment appointment)
            {
                if (appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment));
                }
                if (_appointments.Contains(appointment))
                {
                    throw new InvalidOperationException("This staff member is already supports the appointment.");
                }

                _appointments.Add(appointment);
                if (!appointment.Staffs.Contains(this))
                {
                    appointment.addStaffToAppointment(this);
                } 
            }

            public void RemoveAppointmentFromStaff(Appointment appointment)
            {
                if (appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment));
                }
                if (!_appointments.Contains(appointment))
                {
                    throw new InvalidOperationException("The appointment doesn't supported by this staff member.");
                }
                _appointments.Remove(appointment);
                if (appointment.Staffs.Contains(this))
                {
                    appointment.removeStaffFromAppointment(this);
                } 
            }
        //==================================================================================================================

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Staff))
            {
                return false;
            }

            Staff other = (Staff)obj;

            return this.Id == other.Id &&
                   string.Equals(this._name, other._name, StringComparison.OrdinalIgnoreCase);
        }
        
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _name.ToLowerInvariant());
        }
        
        
        public override string ToString()
        {
            return "Staff Id: "+Id+"Name: "+ Name+ "Position: "+Position;
        }


        // public static void SetStaffMembers(List<Staff> containerStaff)
        // {
        //     _staffList =containerStaff?? new List<Staff>();
        // }

        public static void LoadExtent(IEnumerable<Staff> containerStaff)
        {
            _staffList.Clear();
            foreach (var staf in containerStaff)
            {
                if(staf.GetShifts ==  null || staf.GetShifts().Count == 0)
                {
                    throw new InvalidOperationException("Each staff member must be assigned to least one shift.");
                }

                var initialShift = staf.GetShifts().First();
                var newStaff = new Staff(
                    staf.Id,
                    staf.Name,
                    staf.Position,
                    staf.GetShifts().ToList()
                );

                foreach(var appointment in staf.Appointments)
                {
                    newStaff.AddAppointmentToStaff(appointment);
                }
    
            }
        }
    }
}

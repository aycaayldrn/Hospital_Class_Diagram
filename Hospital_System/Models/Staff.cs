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

        public Staff(int id, string name, string position)
        {
            Id = id;
            Name = name;
            Position = position;
            AddStaff(this);
        }
        
        private static void AddStaff(Staff staff)
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


        public static void SetStaffMembers(List<Staff> containerStaff)
        {
            _staffList =containerStaff?? new List<Staff>();
        }
    }
}

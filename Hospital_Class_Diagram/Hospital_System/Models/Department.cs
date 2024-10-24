using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Department
    {
        
        
        private static List<Department> _departmentList = new List<Department>();
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Department name can't be empty");
                }
                _name = value;
            }
        }

        public Department(string name)
        {
            Name = name;
            addDepartment(this);
        }
        public Department(){}
        
        
        private static void addDepartment(Department department)
        {
            if (department== null)
            {
                throw new ArgumentException("Department cannot be null");
            }
           

            if (_departmentList.Exists(a=>a.Equals(department)))
            {
                throw new InvalidOperationException("Department already added");
            }
            _departmentList.Add(department);
        }
        
        private static void removeDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentException("Department cannot be null");
            }

            if (!_departmentList.Contains(department))
            {
                throw new InvalidOperationException("Department not found!");
            }
            _departmentList.Remove(department);
        }
        
        public static IReadOnlyList<Department> GetDepartments()
        {
            return _departmentList.AsReadOnly();
        }
        public static void SetDepartments(List<Department> departments)
        {
            _departmentList = departments ?? new List<Department>();
        }
        
        
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Department))
            {
                return false;
            }

            Department a = (Department)obj;

            return string.Equals(this._name,a._name,StringComparison.OrdinalIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            return _name.ToLowerInvariant().GetHashCode();
        }
        
        public override string ToString()
        {
            return "Department " + _name;
        }
        
        
        
    }
}

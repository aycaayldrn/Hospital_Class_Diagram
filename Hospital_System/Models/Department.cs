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
        private List<Equipment> _equipmentsList = new List<Equipment>();
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

        public void addEquipmentToDepartment(Equipment equipment)
        {
            if (equipment==null)
            {
                throw new ArgumentException("Equipment cannot be null!");

            }

            if (_equipmentsList.Contains(equipment))
            {
                throw new InvalidOperationException("Equipment already exists in the list");

            }
            _equipmentsList.Add(equipment);
        }

        public void removeEquipmentFromDepartment(Equipment equipment)
        {
            
            if (equipment == null)
            {
                throw new ArgumentException("Equipment cannot be null!");
            }

            if (!_equipmentsList.Contains(equipment))
            {
                throw new  InvalidOperationException("No such element in the list");
            }

            _equipmentsList.Remove(equipment);
            
            if (equipment.Department == this)
            {
                equipment.deleteEquipment();
            }
        }
        

        public Department(string name)
        {
            Name = name;
            addDepartment(this);
        }
         public Department(){}


         internal static void addDepartment(Department department)
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
        
        public static void removeDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentException("Department cannot be null");
            }

            if (!_departmentList.Contains(department))
            {
                throw new InvalidOperationException("Department not found!");
            }
            //have to remove all of equipment before deleting dep!
            var equipmentsListCount = department._equipmentsList.Count;
            for (int i = equipmentsListCount-1; i >=0; i--)
            {
                department._equipmentsList[i].deleteEquipment();
            }
            department._equipmentsList.Clear();
            _departmentList.Remove(department);
        }
        
        public static IReadOnlyList<Department> GetDepartments()
        {
            return _departmentList.AsReadOnly();
        }

        public IReadOnlyList<Equipment> GetEquipments()
        {
            return _equipmentsList.AsReadOnly();
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


        public static void LoadExtent(IEnumerable<Department> containerDepartments)
        {
            _departmentList.Clear();
            foreach (var dep in containerDepartments)
            {
                
                new Department(dep.Name);
            }
        }
    }
}

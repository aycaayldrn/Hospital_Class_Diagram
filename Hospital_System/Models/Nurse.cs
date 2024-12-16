using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Nurse

    {
        private static List<Nurse> _nursesList = new List<Nurse>();
       
       
        public int Id { get; set; }
        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Nurse name can't be empty");
                }
                _name = value;
            }
        }
        
        private List<string> _certification = new List<string>();
        public List<string> Certifications 
        { 
            get => _certification;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(nameof(value), "Certification cannot be null");
                }
                _certification = value.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
        }
        
        private Department _department;
        public Department Department
        {
            get { return _department; }
        }
        
        



        public Nurse(){}
        public Nurse(int id, string name, List<string>? certifications = null)
        {
            Id = id;
            Name = name;
            Certifications = certifications ?? new List<string>();
            addNurse(this);
        }
//==================================================================================================================
//Associations: Agregation nurse-department
        public void asssignNurseToDepartment(Department department)
        {
            if (department==null)
            {
                throw new ArgumentException("department cannot be null");
            }
            
            if (_department!= null)
            {
                throw new InvalidOperationException("Nurse already assigned to department");
            }

            _department = department;
            if (!department.GetNurses().Contains(this))
            {
                department.addNurseToDepartment(this);
            }
        }
        
        
        public void changeDepartment(Department difrentDepartment)
        {
            if (difrentDepartment== null)
            {
                throw new ArgumentException("department cannot be null");
            }

            if (_department==difrentDepartment)
            {
                throw new InvalidOperationException("Departments are the same!");
            }

            if (_department!=null)
            {
                _department.removeNurseFromDepartment(this);
            }
            difrentDepartment.addNurseToDepartment(this);
            _department = difrentDepartment;
        }
        
        
        public void deleteNurse()
        {
            if (_department != null && _department.GetNurses().Contains(this))
            {
                _department.removeNurseFromDepartment(this);
            }
            _department = null;

           
        }



    
//==================================================================================================================
//Class Extent Methods

        internal static void addNurse(Nurse nurse)
        {
            if (nurse== null)
            {
                throw new ArgumentException("Nurse cannot be null");
            }
           

            if (_nursesList.Exists(a=>a.Equals(nurse)))
            {
                throw new InvalidOperationException("Nurse already added");
            }
            _nursesList.Add(nurse);
        }
        
        
        public static void removeNurse(Nurse nurse)
        {
            if (nurse == null)
            {
                throw new ArgumentException("Nurse cannot be null");
            }

            if (!_nursesList.Contains(nurse))
            {
                throw new InvalidOperationException("Nurse not found!");
            }
            _nursesList.Remove(nurse);
        }
        
        
        public static IReadOnlyList<Nurse> GetNurses()
        {
            return _nursesList.AsReadOnly();
        }
        
        
        public static void LoadExtent(IEnumerable<Nurse> containerNurses)
        {
            _nursesList.Clear();
            foreach (var nurse in containerNurses)
            {

                new Nurse(nurse.Id, nurse.Name, nurse.Certifications);
            }
        }
//==================================================================================================================  
//Helper methods
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Nurse))
            {
                return false;
            }

            Nurse a = (Nurse)obj;

            return this.Id==a.Id&& string.Equals(this._name, a._name, StringComparison.OrdinalIgnoreCase);
        }
        
        
        
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _name.ToLowerInvariant());
        }
        
        
        public override string ToString()
        {
            return "Id: " +" "+ Id +" "+ "Name: " + _name;
        }

        public void DisplayNurseInfo()
        {
            Console.WriteLine($"Nurse ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Certifications: {Certifications}");
            
            if (Certifications.Count > 0)
            {
                foreach (var cert in Certifications)
                {
                    Console.WriteLine($"{cert} - ");
                }
            }
            else
            {
                Console.WriteLine("No certifications available");
            }

        }
      

        
    }
}

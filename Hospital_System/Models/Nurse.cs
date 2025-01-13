using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Nurse : Staff

    {
        private static List<Nurse> _nursesList = new List<Nurse>();

        private List<Shift> _shifts = new List<Shift>();
        public IReadOnlyList<Shift> Shifts => _shifts.AsReadOnly();

        private List<Nurse_Shift> _nurseShifts = new List<Nurse_Shift>();
        public IReadOnlyList<Nurse_Shift> Nurse_Shifts => _nurseShifts.AsReadOnly();

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
        public Nurse(int id, string name, List<Shift> initialShifts, List<string>? certifications = null) : base(id,name,"Nurse", initialShifts)
        {
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
        
        
        public void deleteNurseDepartment()
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

                new Nurse(nurse.Id, nurse.Name, nurse.GetShifts().ToList(), nurse.Certifications);
            }
        }
        //==================================================================================================================
        //Asspciation with attribute: nurse-Patient

        //public void AddPatient(Nurse_Shift shift, Patient patient, DateTime nurseShiftStart, DateTime nurseShiftEnd)
        //{
        //    if (shift == null)
        //        throw new ArgumentNullException(nameof(shift), "Shift cannot be null.");

        //    if (patient == null)
        //        throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");

        //    bool alreadyExists = _nurseShifts.Any(ns => ns.Patient == patient);
        //    if (alreadyExists)
        //    {
        //        throw new InvalidOperationException("This Patient is already assigned to this Nurse.");
        //    }

        //    bool hasOverlap = _nurseShifts.Any(ns => ns.Patient == patient && 
        //    (
        //        (nurseShiftStart < ns.EndTime && nurseShiftEnd > ns.StartTime) || 
        //        (nurseShiftStart == ns.StartTime && nurseShiftEnd == ns.EndTime) 
        //    ));

        //    if (hasOverlap)
        //    {
        //        throw new InvalidOperationException("Overlapping shift detected for this patient and nurse.");
        //    }

        //    if (!_nurseShifts.Contains(shift))
        //    {
        //        new Nurse_Shift(new List<Nurse> { this }, patient, nurseShiftStart, nurseShiftEnd);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("The nurse already assigned to this patient-realated shift");
        //    }
        //}

        //public void RemoveShiftFromNurseForPatient(Nurse_Shift shift)
        //{
        //    if (shift == null)
        //    { throw new ArgumentNullException(nameof(shift), "Shift cannot be null."); }

        //    if (!_nurseShifts.Contains(shift))
        //    {
        //        throw new InvalidOperationException("This shift is not assigned to nurse");
        //    }

        //    _nurseShifts.Remove(shift);

        //    if (shift.Patient != null && shift.Patient.GetNurseShiftsInternal().Contains(shift))
        //    {
        //        shift.Patient.GetNurseShiftsInternal().Remove(shift);
        //    }
        //}

        //public  IReadOnlyCollection<Nurse_Shift> GetNurseShifts(){
        //    return _nurseShifts.AsReadOnly();
        //}

        //public List<Nurse_Shift> GetNurseShiftsInternal() => _nurseShifts;

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

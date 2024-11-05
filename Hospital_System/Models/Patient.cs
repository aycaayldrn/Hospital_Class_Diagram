using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Patient
    {
        public Patient(){}
        private static List<Patient> _patientsList = new List<Patient>();
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Insurance provider name can't be empty");
                }
                _name = value;
            }
        }
        public DateTime BirthDate { get; set; }
        public int Age => CalculateAge();
        public List<string> Diagnoses { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Treatments { get; set; }
        public bool HasHealthInsurance { get; set; }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Year;

            if (BirthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public Patient(int id, string name, DateTime birthDate, bool hasHealthInsurance)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            HasHealthInsurance = hasHealthInsurance;

            Diagnoses = new List<string>();
            Allergies = new List<string>();
            Treatments = new List<string>();
            AddPatient(this);
        }

        private static void AddPatient(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentException("Patient cannot be null");
            }

            // Check if the patient already exists (based on Id)
            if (_patientsList.Exists(p => p.Equals(patient)))
            {
                throw new InvalidOperationException("Patient already added");
            }

            _patientsList.Add(patient);
        }
        
        
        public static void RemovePatient(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentException("Patient cannot be null");
            }

            if (!_patientsList.Contains(patient))
            {
                throw new InvalidOperationException("Patient not found");
            }

            _patientsList.Remove(patient);
        }
        
        public static IReadOnlyList<Patient> GetPatients()
        {
            return _patientsList.AsReadOnly();
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Patient))
            {
                return false;
            }

            Patient other = (Patient)obj;

            return this.Id == other.Id && 
                   string.Equals(this._name, other._name, StringComparison.OrdinalIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _name.ToLowerInvariant());
        }
        
        public override string ToString()
        {
            return "Patient Id: "+Id+ "Name: " +Name+ "Age: "+ Age+" Has Health Insurance: "+HasHealthInsurance;
        }


        public static void SetPatients(List<Patient> cPatients)
        {
            _patientsList = cPatients ?? new List<Patient>();
        }
    }
}

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
                    throw new ArgumentException("Patient name can't be empty");
                }
                _name = value;
            }
        }

        private DateTime _birthDate;
        
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value>DateTime.Now)
                {
                    throw new ArgumentException("Birth date can't be in the future");
                }
                _birthDate = value;
            }
        }
        public int Age => CalculateAge();

        private List<string> _diagnoses = new List<string>();
        public List<string> Diagnoses 
        { 
            get => _diagnoses;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(nameof(value), "Diagnose cannot be null");
                }
                _diagnoses = value.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
        }

        private List<string> _allergies = new List<string>();
        public List<string> Allergies 
        { 
            get => _allergies;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(nameof(value), "Allergie cannot be null");
                }
                _allergies = value.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
        }

        private List<string> _treatments = new List<string>();
        public List<string> Treatments 
        { 
            get => _treatments;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(nameof(value), "Treatment cannot be null");
                }
                _treatments = value.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
        }

        public List<Insurance_Provider> _patientProviders = new List<Insurance_Provider>();
        public IReadOnlyList<Insurance_Provider> PatientProviders => _patientProviders.AsReadOnly();

        public bool HasHealthInsurance => _patientProviders.Count > 0;
        
        public Patient(int id, string name, DateTime birthDate)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;

            Diagnoses = new List<string>();
            Allergies = new List<string>();
            Treatments = new List<string>();
            AddPatient(this);
        }

        public void AddInsuranceProviderToPatient(Insurance_Provider provider)
        {
            if (provider == null)
                throw new ArgumentException("Provider cannot be null");

            if (_patientProviders.Contains(provider))
                throw new InvalidOperationException("Provider already added for this patient.");

            _patientProviders.Add(provider);
        }

        public void RemoveInsuranceProviderFromPatient(Insurance_Provider provider)
        {
            if (provider == null)
                throw new ArgumentException("Provider cannot be null");

            if (!_patientProviders.Contains(provider))
                throw new InvalidOperationException("Provider not found for this patient.");

            _patientProviders.Remove(provider);
        }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - _birthDate.Year;

            if (_birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        internal static void AddPatient(Patient patient)
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

        

        public static void LoadExtent(IEnumerable<Patient> containerPatients)
        {
           _patientsList.Clear();
           foreach (var patient in containerPatients)
           {

               new Patient(patient.Age,patient.Name,patient.BirthDate);
           }
        }
    }
}

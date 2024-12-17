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
        private static List<Patient> _patientsList = new List<Patient>();
        
        
        public List<Appointment> _appointments = new List<Appointment>();
        
        
        public List<Prescription> _prescriptions = new List<Prescription>();

        private List<Bill> _bills = new List<Bill>();
        public IReadOnlyList<Bill> Bills => _bills;
        
        
        private List<Insurance_Provider> _patientProviders = new List<Insurance_Provider>();
        public IReadOnlyList<Insurance_Provider> PatientProviders => _patientProviders.AsReadOnly();

        public Room _room;

        public bool HasHealthInsurance => _patientProviders.Count > 0;
        
        
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
        
        

        
        
        public Patient(){}
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

 

        
        
//==================================================================================================================
//Associations: Patient- agrees with- Provider

        public void AddInsuranceProviderToPatient(Insurance_Provider provider)
        {
            if (provider == null)
                throw new ArgumentException("Provider cannot be null");

            if (_patientProviders.Contains(provider))
                throw new InvalidOperationException("Provider already added for this patient.");

            _patientProviders.Add(provider);
            provider.AddPatientToProvider(this);
        }

        public void RemoveInsuranceProviderFromPatient(Insurance_Provider provider)
        {
            if (provider == null)
                throw new ArgumentException("Provider cannot be null");

            if (!_patientProviders.Contains(provider))
                throw new InvalidOperationException("Provider not found for this patient.");

            _patientProviders.Remove(provider);
            provider.RemovePatientFromProvider(this);
        }



//==================================================================================================================
//Patient-Room -> patient can occupy only one room

        public void AssignRoomToPatient(Room room)
        {
            if(room == null) throw new ArgumentException(nameof(room),"Room cannot be empty");

            if (_room == room)
            {
                throw new InvalidOperationException("Patient has already assigned in this room");
            }
            
            _room?.RemovePatientFromRoom(this);

            _room = room;
            if (!room.GetRoomsPatients().Contains(this))
            {
                room.assignPatientToRoom(this);
            }
        }

        public void RemoveRoomFromPatient(Room room)
        {
            if (_room != null && _room.GetRoomsPatients().Contains(this))
            {
                _room.RemovePatientFromRoom(this);
            }
            _room = null;
        }
//==================================================================================================================
//Associations:Compostion: Patient->"visits"-Appointment
        public void addAppointmentForPatient(Appointment appointment)
        {
            if (appointment==null)
            {
                throw new ArgumentException("Appointment cannot be null!");

            }

            if (_appointments.Contains(appointment))
            {
                throw new InvalidOperationException("room already exists in the list");

            }
            _appointments.Add(appointment);
        }

        public void removeAppointmentFromPatient(Appointment appointment)
        {
            
            
            if (appointment == null)
            {
                throw new ArgumentException("Appointment cannot be null!");
            }

            if (!_appointments.Contains(appointment))
            {
                throw new  InvalidOperationException("No such element in the list");
            }

            _appointments.Remove(appointment);
            
            if (appointment.Patient == this)
            {
                appointment.deletePatient();
            }
        }

    public IReadOnlyCollection<Appointment> GetPatientsAppointments()
    {
        return _appointments.AsReadOnly();
    }


//==================================================================================================================
//Associations:Composition: Patient->"receives"-Prescription
        public void addPrescriptionForPatient(Prescription prescription)
        {
            if (prescription==null)
            {
                throw new ArgumentException("Prescription cannot be null!");

            }

            if (_prescriptions.Contains(prescription))
            {
                throw new InvalidOperationException("Prescription already exists in the list");

            }
            _prescriptions.Add(prescription);
        }
        
        public void removePrescriptionFromPatient(Prescription prescription)
        {
            
            
            if (prescription == null)
            {
                throw new ArgumentException("Presscription cannot be null!");
            }

            if (!_prescriptions.Contains(prescription))
            {
                throw new  InvalidOperationException("No such element in the list");
            }

            _prescriptions.Remove(prescription);
            
            if (prescription.Patient == this)
            {
                prescription.deletePrescription();
            }
        }
        
        public IReadOnlyCollection<Prescription> GetPatientsPrescriptions()
        {
            return _prescriptions.AsReadOnly();
        }
        
//==================================================================================================================
//Associations: Composition:Patient->"pays"-Bill
        public void addBillForPatient(Bill bill)
        {
            if (bill==null)
            {
                throw new ArgumentException("Bill cannot be null!");

            }

            if (_bills.Contains(bill))
            {
                throw new InvalidOperationException("Bill already exists in the list");

            }
            _bills.Add(bill);
        }
        public void removeBillFromPatient(Bill bill)
        {
            
            
            if (bill == null)
            {
                throw new ArgumentException("Bill cannot be null!");
            }

            if (!_bills.Contains(bill))
            {
                throw new  InvalidOperationException("No such element in the list");
            }

            _bills.Remove(bill);
            
            if (bill.Patient == this)
            {
                bill.deleteBill();
            }
        }
        public IReadOnlyCollection<Bill> GetPatientsBills()
        {
            return _bills.AsReadOnly();
        }


//==================================================================================================================

//Class Extent Methods
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

            for (int i = patient._appointments.Count-1; i >=0; i--)
            {
                patient._appointments[i].deletePatient();
            }
            
            
            for (int i = patient._prescriptions.Count-1; i >=0; i--)
            {
                patient._prescriptions[i].deletePrescription();
            }
            
            for (int i = patient._bills.Count-1; i >=0; i--)
            {
                patient._bills[i].deleteBill();
            }
            
            
            patient._appointments.Clear();
            patient._prescriptions.Clear();
            patient._bills.Clear();
            _patientsList.Remove(patient);
            
        }
        
        public static IReadOnlyList<Patient> GetPatients()
        {
            return _patientsList.AsReadOnly();
        }
        public static void LoadExtent(IEnumerable<Patient> containerPatients)
        {
            _patientsList.Clear();
            foreach (var patient in containerPatients)
            {

                new Patient(patient.Id,patient.Name, patient.BirthDate);
            }
        } 
        
//==================================================================================================================
//Helper methods
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
        

        
    }
}

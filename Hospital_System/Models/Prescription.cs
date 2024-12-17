using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Prescription
    {
        private static List<Prescription> _prescriptionList = new List<Prescription>();
        
        public int Id { get; set; }
        
        private string _medicationName;
        public string MedicationName
        {
            get => _medicationName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Medicane name can't be empty");
                }
                _medicationName = value;
            }
        }
        
        public float Dosage { get; set; } 
        public int Duration { get; set; }
        public bool RedPrescription { get; set; }
        
        
        private Patient _patient;
        public Patient Patient
        {
            get { return _patient; }
        }
        
        
        private Physician _physician;
        public Physician Physician
        {
            get { return _physician; }
        }
        
        
        
        public Prescription(int id, string medicationName, float dosage, int duration, bool redPrescription)
        {
            Id = id;
            MedicationName = medicationName;
            Dosage = dosage;
            Duration = duration;
            RedPrescription = redPrescription;
            AddPrescription(this);
        }
        public Prescription(){}
//==================================================================================================================
//Association: Agregation-Doctor-Prescription

        public void assignPhysicianToPrescription(Physician physician)
        {
            if (physician == null)
            {
                throw new ArgumentException("Doctor cannot be null");
            }

            if (_physician != null)
            {
                throw new InvalidOperationException("Doctor already assigned to patient");
            }

            _physician = physician;
            if (!physician.GetPrescriptions().Contains(this))
            {
                physician.addPrescriptiont(this);
            }
        }
        
        
        
        
        //Not applicable?
        // public void changePhysician(Physician diffrentDoctor)
        // {
        //     if (diffrentDoctor== null)
        // {
        //     throw new ArgumentException("Doctor cannot be null");
        // }
        //
        //      if (_physician==diffrentDoctor)
        // {
        //     throw new InvalidOperationException("Doctors are the same!");
        // }
        //
        //     if (_physician!=null)
        // {
        //      _physician.removePrescriptiont(this);
        // }
        //      diffrentDoctor.addPrescriptiont(this);
        //     _physician = diffrentDoctor;
        // }
        
        public void deletePrescriptionOfPhysician()
        {
            if (_physician != null && _physician.GetPrescriptions().Contains(this))
            {
                _physician.removePrescriptiont(this);
            }
            _physician = null;

           
        }


        
//==================================================================================================================
//

        public void assignPatientPrescription(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentException("Patient cannot be null");
            }

            if (_patient != null)
            {
                throw new InvalidOperationException("Patient already assigned to this");
            }

            _patient = patient;
            if (!patient.GetPatientsPrescriptions().Contains(this))
            {
                patient.addPrescriptionForPatient(this);
            }
        }


        public void deletePrescription()
        {
            if (_patient != null && _patient.GetPatientsPrescriptions().Contains(this))
            {
                _patient.removePrescriptionFromPatient(this);
            }
            _patient = null;
        }
        
        
        
        
        // public void changePrescription(Patient newPatient)
        // {
        //     if (newPatient== null)
        //     {
        //         throw new ArgumentException("Patient cannot be null");
        //     }
        //
        //     if (_patient==newPatient)
        //     {
        //         throw new InvalidOperationException("Patients are the same!");
        //     }
        //
        //     if (_patient!=null)
        //     {
        //         _patient.removePrescriptionFromPatient(this);
        //     }
        //     newPatient.addPrescriptionForPatient(this);
        //     _patient = newPatient;
        // }
//==================================================================================================================
//Class Extent Methods
        internal static void AddPrescription(Prescription prescription)
        {
            if (prescription == null)
            {
                throw new ArgumentException("Prescription cannot be null");
            }

            if (_prescriptionList.Exists(p => p.Equals(prescription)))
            {
                throw new InvalidOperationException("Prescription already added");
            }

            _prescriptionList.Add(prescription);
        }
        
        public static void RemovePrescription(Prescription prescription)
        {
            if (prescription == null)
            {
                throw new ArgumentException("Prescription cannot be null");
            }

            if (!_prescriptionList.Contains(prescription))
            {
                throw new InvalidOperationException("Prescription not found");
            }
            prescription.deletePrescription();
            _prescriptionList.Remove(prescription);
        }
        
        public static IReadOnlyList<Prescription> GetPrescriptions()
        {
            return _prescriptionList.AsReadOnly();
        }
        
        public static void LoadExtent(IEnumerable<Prescription> containerPrescriptions)
        {
            _prescriptionList.Clear();
            foreach (var pre in containerPrescriptions)
            {

                new Prescription(pre.Id, pre.MedicationName, pre.Dosage, pre.Duration, pre.RedPrescription);
            }
        }
//==================================================================================================================
//Helper methods
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Prescription))
            {
                return false;
            }

            Prescription other = (Prescription)obj;

            return this.Id == other.Id &&
                   string.Equals(this._medicationName, other._medicationName, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _medicationName.ToLowerInvariant());
        }
        
        public override string ToString()
        {
            return "Prescription Id: "+Id+ "Medication :"+MedicationName +"Dosage: "+Dosage+ "Duration: "+ Duration + "Red Prescription: "+RedPrescription;
        }

       

        

       
    }
}

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
        public Prescription(int id, string medicationName, float dosage, int duration, bool redPrescription)
        {
            Id = id;
            MedicationName = medicationName;
            Dosage = dosage;
            Duration = duration;
            RedPrescription = redPrescription;
            AddPrescription(this);
        }

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
        
        private static void AddPrescription(Prescription prescription)
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

            _prescriptionList.Remove(prescription);
        }
        
        public static IReadOnlyList<Prescription> GetPrescriptions()
        {
            return _prescriptionList.AsReadOnly();
        }
        
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

        public static void SetPrescriptions(List<Prescription> containerPrescriptions)
        {
            _prescriptionList = containerPrescriptions ?? new List<Prescription>();
        }
    }
}

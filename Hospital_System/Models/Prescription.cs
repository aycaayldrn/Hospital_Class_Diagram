using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_System.Models
{
    internal class Prescription
    {
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

        public Prescription(int id, string medicationName, float dosage, int duration, bool redPrescription)
        {
            Id = id;
            MedicationName = medicationName;
            Dosage = dosage;
            Duration = duration;
            RedPrescription = redPrescription;
        }
    }
}

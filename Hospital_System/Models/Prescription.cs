using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Prescription
    {
        public int Id { get; set; }
        public string Medication_Name { get; set; }
        public float Dosage { get; set; }
        public int Duration { get; set; }
        public bool RedPrescription { get; set; }
    }
}

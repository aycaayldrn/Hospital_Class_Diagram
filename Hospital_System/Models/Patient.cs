using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_System.Models
{
    public class Patient
    {
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
        }


    }
}

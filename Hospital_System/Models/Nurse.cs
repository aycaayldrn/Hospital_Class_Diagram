using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Nurse 
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
                    throw new ArgumentException("Nurse name can't be empty");
                }
                _name = value;
            }
        }
        public List<string> Certifications { get; set; }

        public Nurse(int id, string name, List<string>? certifications = null)
        {
            Id = id;
            Name = name;
            Certifications = certifications ?? new List<string>();
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

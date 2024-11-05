using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Nurse

    {
        public Nurse(){}
        private static List<Nurse> _nursesList = new List<Nurse>();
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
            addNurse(this);
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
        
        
        private static void addNurse(Nurse nurse)
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
        
        
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Fellow))
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


        public static void SetNurses(List<Nurse> CNurses)
        {
            _nursesList = CNurses ?? new List<Nurse>();
        }
    }
}

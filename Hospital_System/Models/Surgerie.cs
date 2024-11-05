using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Surgerie
    {
        public Surgerie(){}
        private static List<Surgerie> _surgeryList = new List<Surgerie>();
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Surgery type can't be empty");
                }
                _type = value;
            }
        }

        public Surgerie(string type) 
        {
            Type = type;
            AddSurgery(this);
        }
        
        
        private static void AddSurgery(Surgerie surgery)
        {
            if (surgery == null)
            {
                throw new ArgumentException("Surgery cannot be null");
            }

            if (_surgeryList.Exists(s => s.Equals(surgery)))
            {
                throw new InvalidOperationException("Surgery already added");
            }

            _surgeryList.Add(surgery);
        }
        
        public static void RemoveSurgery(Surgerie surgery)
        {
            if (surgery == null)
            {
                throw new ArgumentException("Surgery cannot be null");
            }

            if (!_surgeryList.Contains(surgery))
            {
                throw new InvalidOperationException("Surgery not found");
            }

            _surgeryList.Remove(surgery);
        }
        
        
        public static IReadOnlyList<Surgerie> GetSurgeries()
        {
            return _surgeryList.AsReadOnly();
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Surgerie))
            {
                return false;
            }

            Surgerie other = (Surgerie)obj;

            return string.Equals(this._type, other._type, StringComparison.OrdinalIgnoreCase);
        }
        
        
        public override int GetHashCode()
        {
            return _type.ToLowerInvariant().GetHashCode();
        }
        
        public override string ToString()
        {
            return "Surgery Type: "+Type;
        }

        public static void SetSurgeries(List<Surgerie> containerSurgeries)
        {
            _surgeryList = containerSurgeries?? new List<Surgerie>();
        }
    }
}

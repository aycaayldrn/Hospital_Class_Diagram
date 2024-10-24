using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Insurance_Provider
    {
        private static List<Insurance_Provider> _insuranceProviders = new List<Insurance_Provider>();
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

        public Insurance_Provider(int id, string name)
        {
            if(id < 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }

            Id = id;
            Name = name;
            addProvider(this);
        }
        public Insurance_Provider(){}
        
        
        
        private static void addProvider(Insurance_Provider provider)
        {
            if (provider== null)
            {
                throw new ArgumentException("Provider cannot be null");
            }
           

            if (_insuranceProviders.Exists(a=>a.Equals(provider)))
            {
                throw new InvalidOperationException("Provider already added");
            }
            _insuranceProviders.Add(provider);
        }
        
        
        private static void removeProvider(Insurance_Provider provider)
        {
            if (provider == null)
            {
                throw new ArgumentException("Provider cannot be null");
            }

            if (!_insuranceProviders.Contains(provider))
            {
                throw new InvalidOperationException("Provider not found!");
            }
            _insuranceProviders.Remove(provider);
        }
        
        
        public static IReadOnlyList<Insurance_Provider> GetProvider()
        {
            return _insuranceProviders.AsReadOnly();
        }
        
        
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Insurance_Provider))
            {
                return false;
            }

            Insurance_Provider a = (Insurance_Provider)obj;

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

        public static void SetProviders(List<Insurance_Provider> InsuranceProviders)
        {
            _insuranceProviders = InsuranceProviders ?? new List<Insurance_Provider>();
        }
    }
}

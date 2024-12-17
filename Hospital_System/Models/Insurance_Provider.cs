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

        private List<Service> _services = new List<Service>();
        public IReadOnlyList<Service> Services => _services.AsReadOnly();

        private List<Patient> _patients = new List<Patient>();
        public IReadOnlyList<Patient> Patients => _patients.AsReadOnly();

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


        internal static void addProvider(Insurance_Provider provider)
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
        
        
        public static void removeProvider(Insurance_Provider provider)
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
        
        //==================================================================================================
        //Insurance provider - covers- Service

        public void AddServiceToProvide(Service service)
        {
            if (service == null)
            { throw new ArgumentNullException(nameof(service)); }

            if (_services.Contains(service))
            {
                throw new InvalidOperationException("The specified service is already under covarage");
            }

            _services.Add(service);
            service.assignInsuranceProviderToService(this);
        }

        public void RemoveServiceFromProvider(Service service)
        {
            if (service == null)
            { throw new ArgumentNullException(nameof(service)); }

            if (!_services.Contains(service))
            {
                throw new InvalidOperationException("The specified service is not already under covarage by provider.");
            }

            //Insurance provider must cover at least one service.
            if(_services.Count == 1)
            {
                throw new InvalidOperationException("At least a service must be included in the insurance.Cannot remove the last service.");
            }

            _services.Remove(service);
            service.removeInsuranceProviderFromService(this);

        }

        //==================================================================================================
        //Insurance providers - Patient

        public void AddPatientToProvider(Patient patient)
        {
            if(patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }
            if (_patients.Contains(patient))
            {
                throw new InvalidOperationException("The patient has already agreed with the provider");
            }
            _patients.Add(patient);
            patient.AddInsuranceProviderToPatient(this);
        }

        public void RemovePatientFromProvider(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }
            if (!_patients.Contains(patient))
            {
                throw new InvalidOperationException("The patient already doesnt have an agreement with the provider");
            }
            _patients.Remove(patient);
            patient.RemoveInsuranceProviderFromPatient(this);
        }
        //==================================================================================================

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

        // public static void SetProviders(List<Insurance_Provider> InsuranceProviders)
        // {
        //     _insuranceProviders = InsuranceProviders ?? new List<Insurance_Provider>();
        // }

        public static void LoadExtent(IEnumerable<Insurance_Provider> containerInsuranceProviders)
        {
            _insuranceProviders.Clear();
            foreach (var provider in containerInsuranceProviders)
            {

                new Insurance_Provider(provider.Id, provider.Name);
            }
        }
    }
}

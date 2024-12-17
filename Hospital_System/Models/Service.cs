using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Service
    {
        public Service()
        {
            
        }
        private static List<Service> _serviceList = new List<Service>();
        private string _serviceName;

        private List<Bill> _bills = new List<Bill>();
        public IReadOnlyList<Bill> Bills => _bills.AsReadOnly();

        private List<Insurance_Provider> _insuranceProviders = new List<Insurance_Provider>();
        public IReadOnlyList<Insurance_Provider> Insurance_Providers => _insuranceProviders.AsReadOnly();
        public string Name
        {
            get => _serviceName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Service name can't be empty");
                }
                _serviceName = value;
            }
        }

        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price must be greater than zero");
                }
                _price = value;
            }
        }

        public Service(string serviceName, double price) 
        {
            Name = serviceName;
            Price = price;
            AddService(this);
        }

        internal static void AddService(Service service)
        {
            if (service == null)
            {
                throw new ArgumentException("Service cannot be null");
            }

            if (_serviceList.Exists(s => s.Equals(service)))
            {
                throw new InvalidOperationException("Service already added");
            }

            _serviceList.Add(service);
        }

        public static void RemoveService(Service service)
        {
            if (service == null)
            {
                throw new ArgumentException("Service cannot be null");
            }

            if (!_serviceList.Contains(service))
            {
                throw new InvalidOperationException("Service not found");
            }

            _serviceList.Remove(service);
        }
        
        public static IReadOnlyList<Service> GetServices()
        {
            return _serviceList.AsReadOnly();
        }
        
        //==================================================================================================================
        //Service-Bill

        public void assignBillToService(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentNullException(nameof(bill));
            }

            if (!_bills.Contains(bill))
            {
                _bills.Add(bill);
                bill.AddServiceToBill(this);
            }
            else
            {
                throw new InvalidOperationException("Bill already assigned to this service.");
            }
        }

        public void RemoveBillFromService(Bill bill)
        {
            if (bill == null) throw new ArgumentNullException(nameof(bill));

            // can be empty becouse service may included in zero to many bill
            if (_bills.Count == 0)
                throw new InvalidOperationException("No bills to remove from. The list is empty.");

            if (_bills.Contains(bill))
            {
                _bills.Remove(bill);
                bill.RemoveServiceFromBill(this);
            }
            else
            {
                throw new InvalidOperationException("The specified bill is not associated with this service.");
            }

        }
        //==================================================================================================================
        //Service- under covarage by- insurance provider
        public void assignInsuranceProviderToService(Insurance_Provider insurance_provider)
        {
            if(insurance_provider == null)
            {
                throw new ArgumentNullException(nameof(insurance_provider));
            }

            if (_insuranceProviders.Contains(insurance_provider)){
                throw new InvalidOperationException("Insurance provider already covers this service");
            }

            _insuranceProviders.Add(insurance_provider);
            insurance_provider.AddServiceToProvide(this);
        }

        public void removeInsuranceProviderFromService(Insurance_Provider insurance_provider)
        {
            if(insurance_provider == null)
            {
                throw new ArgumentNullException(nameof(insurance_provider));
            }
            if (!_insuranceProviders.Contains(insurance_provider))
            {
                throw new InvalidOperationException("Specified insurance provider is not covering service");
            }

            _insuranceProviders.Remove(insurance_provider);
            insurance_provider.RemoveServiceFromProvider(this);
        }
        //===================================================================================================================
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Service))
            {
                return false;
            }

            Service other = (Service)obj;

            return string.Equals(this._serviceName, other._serviceName, StringComparison.OrdinalIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            return _serviceName.ToLowerInvariant().GetHashCode();
        }
        
        public override string ToString()
        {
            return "Service Name: "+Name+ "Price: "+Price;
        }


        // public static void SetServices(List<Service> containerServices)
        // {
        //     _serviceList =containerServices?? new List<Service>();
        // }

        public static void LoadExtent(IEnumerable<Service> containerServices)
        {
            _serviceList.Clear();
            foreach (var ser in containerServices)
            {

                new Service(ser.Name, ser.Price);
            }
        }
    }
}

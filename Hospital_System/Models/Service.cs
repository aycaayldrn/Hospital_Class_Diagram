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
        
        private static void AddService(Service service)
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


        public static void SetServices(List<Service> containerServices)
        {
            _serviceList =containerServices?? new List<Service>();
        }
    }
}

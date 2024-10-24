using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Service
    {
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
        public double Price { get; set; }

    }
}

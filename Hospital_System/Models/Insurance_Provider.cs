using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public class Insurance_Provider
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

        public Insurance_Provider(int id, string name)
        {
            if(id < 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }

            Id = id;
            Name = name;
        }
    }
}

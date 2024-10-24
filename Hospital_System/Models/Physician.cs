using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Physician
    {
        private string _specialization;
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Specialization can't be empty");
                }
                _specialization = value;
            }
        }

        public Physician(string specialization)
        {
            Specialization = specialization;
        }
    }
}

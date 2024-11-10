using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_System.Models
{
    public class General_Practitioner
    {
        private string _primaryCareSpecialty;

        public string PrimaryCareSpecialty
        {
            get => _primaryCareSpecialty;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Primary care specialty can't be empty");
                }
                _primaryCareSpecialty = value;
            }
        }

        public General_Practitioner(string primaryCareSpecialty)
        {
            PrimaryCareSpecialty = primaryCareSpecialty;
        }

        public override string ToString()
        {
            return $"Specialty: {PrimaryCareSpecialty}";
        }

    }
}

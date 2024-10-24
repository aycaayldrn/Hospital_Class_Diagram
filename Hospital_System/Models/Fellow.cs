using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Fellow
    {
        public string? ResearchProject { get; set; }

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

        public Fellow(string specialization, string? researchProject = null) 
        {
            Specialization = specialization;
            ResearchProject = researchProject;
        }
    }
        
}

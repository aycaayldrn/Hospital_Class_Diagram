using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public class Surgerie
    {
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
        }
    }
}

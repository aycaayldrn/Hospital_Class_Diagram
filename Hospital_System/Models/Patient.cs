﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_System.Models
{
    internal class Patient
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
        public DateTime BirthDate { get; set; }
        public List<string> Diagnoses { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Treatments { get; set; }
        public bool HasHealthInsurance { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Equipment
    {
        public int Id { get; set; }
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Equipment type can't be empty");
                }
                _type = value;
            }
        }
        public List<string> MaintenanceHistory { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Staff
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
                    throw new ArgumentException("Name of staff can't be empty");
                }
                _name = value;
            }
        }

        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Position of staff can't be empty");
                }
                _position = value;
            }
        }

        private static readonly int MaxWorkingHours = 12;

        public Staff(int id, string name, string position)
        {
            Id = id;
            Name = name;
            Position = position;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Room
    {
        public int Number { get; set; }
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Room type can't be empty");
                }
                _type = value;
            }
        }

        private string _availability;
        public string Availability
        {
            get => _availability;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Room availability info can't be empty");
                }
                _availability = value;
            }
        }

        public Room(int number, string type, string availability)
        {
            if(number <= 0)
            {
                throw new ArgumentException("Room number must be grater than zero");
            }

            Number = number;   
            Type = type;
            Availability = availability;
        }
        
    }
}

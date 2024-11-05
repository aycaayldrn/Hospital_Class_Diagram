using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Room
    {
        public Room(){}
        private static List<Room> _roomList = new List<Room>();
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
            AddRoom(this);
        }
        
        private static void AddRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentException("Room cannot be null");
            }

            if (_roomList.Exists(r => r.Equals(room)))
            {
                throw new InvalidOperationException("Room already added");
            }

            _roomList.Add(room);
        }

        public static void RemoveRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentException("Room cannot be null");
            }

            if (!_roomList.Contains(room))
            {
                throw new InvalidOperationException("Room not found");
            }

            _roomList.Remove(room);
        }
        
        public static IReadOnlyList<Room> GetRooms()
        {
            return _roomList.AsReadOnly();
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Room))
            {
                return false;
            }

            Room other = (Room)obj;

            return this.Number == other.Number;
        }
        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
        
        public override string ToString()
        {
            return "Room Number: "+Number+ "Type: "+Type+ "Availability: " + Availability;
        }

        public static void SetRooms(List<Room> containerRooms)
        {
            _roomList = containerRooms ?? new List<Room>();
        }
    }
}

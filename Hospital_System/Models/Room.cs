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
        [Serializable]
        public enum RoomType
        {
            Single,
            Double,
            ICU
        }
        
        [Serializable]
        public enum RoomAvailability
        {
            Available,
            Occupied,
            UnderMaintenance
        }
        private RoomAvailability _availability;
        public RoomAvailability Availability
        {
            get => _availability;
            set => _availability = value;
        }


        private static List<Room> _roomList = new List<Room>();

        private List<Patient> _patients = new List<Patient>();
        public IReadOnlyList<Patient> Patients => _patients.AsReadOnly();


        
        private RoomType _type;
        public RoomType Type
        {
            get => _type;
            set => _type = value;
        }
        

        public Room(RoomType type, RoomAvailability availability) //new changes: room number deleted bc it is qualifer 
        {  
            Type = type;
            Availability = availability;
            AddRoom(this);
        }

        public Room() { }
        
        
        private Department _department; //association 1
        public Department Department
        {
            get { return _department; }
        }
        
//==================================================================================================================
//Associations Room-Department

       
        public void assignRoomToDepartment(Department department)
        {
            if (department==null)
            {
                throw new ArgumentException("Room cannot be null");
            }
            
            if (_department != null)
            {
                throw new InvalidOperationException("Room already assigned to department");
            }

            _department = department;

            if (!department.GetDepartmentRooms().Contains(this))
            {
                department.addRoomToDepartment(this);
            }
        }
        public void deleteRoom()
        {
            Department department = _department;
            _department = null;
            if (department!=null&&department.GetDepartmentRooms().Contains(this))
            {
                department.removeRoomFromDepartment(this);
            }
        }
        
  
//==================================================================================================================
//Class Extent Methods
        internal static void AddRoom(Room room)
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
            room.deleteRoom();
        }
        
        public static IReadOnlyList<Room> GetRooms()
        {
            return _roomList.AsReadOnly();
        }
        public static void LoadExtent(IEnumerable<Room> containerRooms)
        {
            _roomList.Clear();
            foreach (var room in containerRooms)
            {

                new Room( room.Type, room.Availability);
            }
        }
//==================================================================================================================
//Room--patient 
        public void assignPatientToRoom(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");

            if(Availability != RoomAvailability.Available)
                throw new InvalidOperationException("Room is not available for assignment.");
            

            if(Type == RoomType.Single && _patients.Count >= 1)
                throw new InvalidOperationException("Single room can only have one patient.");
            else if (Type == RoomType.Double && _patients.Count >= 2)
                throw new InvalidOperationException("Double room can only have two patients.");
            else if (Type == RoomType.ICU && _patients.Count >= 1)
                throw new InvalidOperationException("ICU room can only have one patient.");

            if (_patients.Contains(patient))
            {
                throw new InvalidOperationException("This patient is already assigned to the room.");
            }  

            _patients.Add(patient);
            patient.AssignRoomToPatient(this);

            Availability = RoomAvailability.Occupied;
        }

        public void RemovePatientFromRoom(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");

            if (!_patients.Contains(patient))
                throw new InvalidOperationException("The patient is not in this room.");

            _patients.Remove(patient);
            patient.RemoveRoomFromPatient(this);

            Availability = _patients.Count > 0 ? RoomAvailability.Occupied : RoomAvailability.Available;
        }
        public bool IsAvailable()
        {
            return Availability == RoomAvailability.Available;
        }

        public IReadOnlyCollection<Patient> GetRoomsPatients()
        {
            return _patients.AsReadOnly();
        }
        //==================================================================================================================
        //Helper methods
        //public override bool Equals(object? obj)
        //{
        //    if (obj == null || !(obj is Room))
        //    {
        //        return false;
        //    }

        //    Room other = (Room)obj;

        //    return this.Number == other.Number;
        //}

        //since number is not inside constructor, we check equality by reference, not sure if it correct?
        public override bool Equals(object? obj)
        {
            if(obj == null || !(obj is Room))
            {
                return false;
            }
            return this ==  (Room)obj;
        }


        public override int GetHashCode()
        {
            //return Number.GetHashCode();
            return base.GetHashCode();
        }
        
        public override string ToString()
        {
            return "Type: "+Type+ "Availability: " + Availability;
        }

       
    }
}

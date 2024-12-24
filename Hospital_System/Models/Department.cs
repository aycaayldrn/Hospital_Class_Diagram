using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Department
    {
        private static List<Department> _departmentList = new List<Department>();
        
        private Dictionary<int, Room> _roomNumber = new Dictionary<int, Room>(); //qualified association
        
        private List<Equipment> _equipmentsList = new List<Equipment>();

        private List<Nurse> _nursesInDepartment = new List<Nurse>();
        private List<Doctor> _doctorsInDepartment = new List<Doctor>();
        
        
        private Doctor? _headOfDepartment;
        public Doctor? HeadOfDepaartment
        {
            get { return _headOfDepartment; }
            set { _headOfDepartment = value; }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Department name can't be empty");
                }
                _name = value;
            }
        }
        
        
        public Department(string name, Room initialRoom) // orijinal association before showing qualifier association is depart 1---- 1..* room
        {                                                // since department cant be exist without a room assigned
            Name = name;

            if(initialRoom == null)
            {
                throw new ArgumentException("Each department must have at least one room");
            }
            addRoomToDepartment(initialRoom);
            addDepartment(this);
        }
        public Department(){}
        
        
//==================================================================================================================
//Associations: Department-HeadDoctor Agregation
        public Doctor GetHeadOfDepartment()
        {
            return _headOfDepartment;
            
        }

        public void assignHeadOfDepartment(Doctor doctor)
        {
            if (doctor==null)
            {
                throw new ArgumentException("Doctor cannot be null");
            }

            if (_headOfDepartment == doctor)
            {
                return;
            }

            if (!_doctorsInDepartment.Contains(doctor))
            {
                throw new InvalidOperationException("Doctor must be part of the department to become head");
            }

            if (doctor.HeadedDepartment != null && doctor.HeadedDepartment != this)
            {
                throw new InvalidOperationException("Doctor is already heading another department.");
            }

            _headOfDepartment = doctor;

            if (doctor.HeadedDepartment != this)
            {
                doctor.becomeHeadOfDepartment(this);
            }
        }

        public void removeHeadOfDepartment(Doctor doctor)
        {
            if(_headOfDepartment == null){ 
   
                throw new InvalidOperationException("No head assigned to this department.");
            }

            if (_headOfDepartment != doctor)
            {
                throw new InvalidOperationException("The specified doctor is not the head of this department.");
            }

            var currentHead = _headOfDepartment;
            HeadOfDepaartment = null;

            if (currentHead.HeadedDepartment!=null&&currentHead.HeadedDepartment.Equals(this))
            {
                currentHead.deleteDoctorFromBeingHead(this);
            }
        }
        
        
        
        

//==================================================================================================================
//Associations: Department-Doctor Agregation
        public void addDoctorToDepartment(Doctor doctor)
        {
            if (doctor==null)
            {
                throw new ArgumentException("Doctor can't be null");
            }

            if (_doctorsInDepartment.Contains(doctor))
            {
                throw new InvalidOperationException("Doctor is already assigned to this department");
                
            }
            _doctorsInDepartment.Add(doctor);
            if(doctor.Department != this)
            {
                doctor.asssignDoctorToDepartment(this);
            }
        }
        public void removeDoctorFromDepartment(Doctor doctor)
        {
            if (doctor==null)
            {
                throw new ArgumentException("Doctor can't be null");
            }

            if (!_doctorsInDepartment.Contains(doctor))
            {
                throw new InvalidOperationException("No such doctor in list");
                
            }
            _doctorsInDepartment.Remove(doctor);
            if (doctor.Department == this)
            {
                doctor.deleteDoctor();
            }
        }
        
        
        
        public IReadOnlyList<Doctor> GetDoctors()
        {
            return _doctorsInDepartment.AsReadOnly();
        }


//==================================================================================================================
//Associations: Department-Nurse Agregation
        public void addNurseToDepartment(Nurse nurse)
        {
            if (nurse==null)
            {
                throw new ArgumentException("Nurse can't be null");
            }

            if (_nursesInDepartment.Contains(nurse))
            {
                throw new InvalidOperationException("Nurse is already assigned to this department");
            }
            _nursesInDepartment.Add(nurse);
            if (nurse.Department != this)
            {
                nurse.asssignNurseToDepartment(this);
            }
        }
        public void removeNurseFromDepartment(Nurse nurse)
        {
            if (nurse==null)
            {
                throw new ArgumentException("Nurse can't be null");
            }

            if (!_nursesInDepartment.Contains(nurse))
            {
                throw new InvalidOperationException("No such nurse in list");
                
            }
            _nursesInDepartment.Remove(nurse);
            if (nurse.Department == this)
            {
                nurse.deleteNurseDepartment();
            }
        }
        
        
        
        public IReadOnlyList<Nurse> GetNurses()
        {
            return _nursesInDepartment.AsReadOnly();
        }
        

//==================================================================================================================

//Associations Room-Department

        public Dictionary<int, Room> GetRooms() => _roomNumber != null ? new Dictionary<int, Room>(_roomNumber) : new Dictionary<int, Room>();
        public void addRoomToDepartment(Room room)
        {
            if (room==null)
            {
                throw new ArgumentException("Room cannot be null!");

            }

            if (_roomNumber.ContainsValue(room))
            {
                throw new InvalidOperationException("room already exists in the list");

            }

            _roomNumber.Add(room.GetHashCode(), room);

            if (room.Department != this)
            {
                room.assignRoomToDepartment(this);
            }

        } 
        public void removeRoomFromDepartment(Room room)
        {
            
            if (room == null)
            {
                throw new ArgumentException("Room cannot be null!");
            }

            if (!_roomNumber.ContainsValue(room))
            {
                throw new  InvalidOperationException("No such element in the list");
            }

            _roomNumber.Remove(room.GetHashCode());
            
            if (room.Department == this)
            {
                room.deleteRoom();
            }
        }
        
        public IReadOnlyList<Room> GetDepartmentRooms()
        {
            return _roomNumber.Values.ToList().AsReadOnly();
        }

//==================================================================================================================        
//Associations Equipment-Department

        public void addEquipmentToDepartment(Equipment equipment)
        {
            if (equipment==null)
            {
                throw new ArgumentException("Equipment cannot be null!");

            }

            if (_equipmentsList.Contains(equipment))
            {
                throw new InvalidOperationException("Equipment already exists in the list");

            }
            _equipmentsList.Add(equipment);
            if (equipment.Department != this)
            {
                equipment.assignToDepartment(this);
            }
        }

        public void removeEquipmentFromDepartment(Equipment equipment)
        {
            
            if (equipment == null)
            {
                throw new ArgumentException("Equipment cannot be null!");
            }

            if (!_equipmentsList.Contains(equipment))
            {
                throw new  InvalidOperationException("No such element in the list");
            }

            _equipmentsList.Remove(equipment);
            
            if (equipment.Department == this)
            {
                equipment.deleteEquipment();
            }
        }
        public IReadOnlyList<Equipment> GetEquipments()
        {
            return _equipmentsList.AsReadOnly();
        }

        
//==================================================================================================================
//Class Extent Methods

         internal static void addDepartment(Department department)
        {
            if (department== null)
            {
                throw new ArgumentException("Department cannot be null");
            }
           

            if (_departmentList.Exists(a=>a.Equals(department)))
            {
                throw new InvalidOperationException("Department already added");
            }
            _departmentList.Add(department);
        }
        
        public static void removeDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentException("Department cannot be null");
            }

            if (!_departmentList.Contains(department))
            {
                throw new InvalidOperationException("Department not found!");
            }
            //have to remove all of equipment,rooms before deleting dep!
            
            for (int i = department._equipmentsList.Count-1; i >=0; i--)
            {
                department._equipmentsList[i].deleteEquipment();
            }

            foreach (var room in department._roomNumber.Values.ToList())
            {
                room.deleteRoom();
            }

            department._equipmentsList.Clear();
            department._roomNumber.Clear();
            _departmentList.Remove(department);
        }

        
        public static void LoadExtent(IEnumerable<Department> containerDepartments)
        {
            _departmentList.Clear();
            foreach (var dep in containerDepartments)
            {
                if(dep._roomNumber == null || dep._roomNumber.Count == 0)
                {
                    throw new InvalidOperationException("Each department must have at least one room.");
                }

                var initialRoom = dep._roomNumber.Values.First();
                var newDepartment = new Department(
                    dep.Name,
                    initialRoom);

                foreach (var aditionalRoom in dep._roomNumber.Values.Skip(1)){
                    newDepartment.addRoomToDepartment(aditionalRoom);
                }
                foreach(var equipment in dep._equipmentsList)
                {
                    newDepartment.addEquipmentToDepartment(equipment);
                }
                foreach(var doctor in dep.GetDoctors())
                {
                    newDepartment.addDoctorToDepartment(doctor);
                }
                if(dep._headOfDepartment != null)
                {
                    newDepartment.assignHeadOfDepartment(dep.HeadOfDepaartment);
                }
            }
        }
        
        public static IReadOnlyList<Department> GetDepartments()
        {
            return _departmentList.AsReadOnly();
        }
        
//==================================================================================================================
//Helper methods

        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Department))
            {
                return false;
            }

            Department a = (Department)obj;

            return string.Equals(this._name,a._name,StringComparison.OrdinalIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            return _name.ToLowerInvariant().GetHashCode();
        }
        
        public override string ToString()
        {
            return "Department " + _name;
        }


 

        
    }
}

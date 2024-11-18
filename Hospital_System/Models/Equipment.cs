using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Equipment
    {
        private static List<Equipment> _equipmentList = new List<Equipment>();
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

        private List<string> _maintenanceHistory = new List<string>();
        public List<string> MaintenanceHistory
        {
            get => _maintenanceHistory;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(nameof(value), "Maintenance history cannot be null");
                }
                _maintenanceHistory = value.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
        }

        public Equipment(){}
        public Equipment(int id, string type)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }

            Id = id;
            Type = type;
            MaintenanceHistory = new List<string>();
            addEqiupment(this);
        }


        internal static void addEqiupment(Equipment equipment)
        {
            if (equipment== null)
            {
                throw new ArgumentException("equpiment cannot be null");
            }
           

            if (_equipmentList.Exists(a=>a.Equals(equipment)))
            {
                throw new InvalidOperationException("equipment already added");
            }
            _equipmentList.Add(equipment);
        }


        public static void removeEquipment(Equipment equipment)
        {
            if (equipment == null)
            {
                throw new ArgumentException("equipment cannot be null");
            }

            if (!_equipmentList.Contains(equipment))
            {
                throw new InvalidOperationException("appointment not found!");
            }
            _equipmentList.Remove(equipment);
        }
        
        
        public static IReadOnlyList<Equipment> GetEquipments()
        {
            return _equipmentList.AsReadOnly();
        }
        
        
        
        
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Equipment))
            {
                return false;
            }

            Equipment a = (Equipment)obj;

            return this.Id==a.Id&& string.Equals(this._type, a._type, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _type.ToLowerInvariant());
        }

        public override string ToString()
        {
            return "type: " + _type + "Id: " + Id;
        }

        // public static void SetEquipments(List<Equipment> eq)
        // {
        //     _equipmentList = eq?? new List<Equipment>();
        // }

        public static void LoadExtent(IEnumerable<Equipment> containerEquipments)
        {
            _equipmentList.Clear();
            foreach (var equ in containerEquipments)
            {

                new Equipment(equ.Id,equ.Type);
            }
        }
    }
}

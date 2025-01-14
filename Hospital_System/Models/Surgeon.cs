using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Surgeon:Doctor
    {
        public Surgeon(){}
        private static List<Surgeon> _surgeonList = new List<Surgeon>();

        private List<string> _surgeries = new List<string>();
        public List<string> Surgeries 
        { 
            get => _surgeries;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(nameof(value), "Surgery cannot be null");
                }
                _surgeries = value.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
        }
        public static readonly int MaxSurgeriesPerShift = 2;

        public Surgeon(int id,string name,List<Shift> initialShifts,List<string> surgeries = null): base(id, name, initialShifts)
        {
            Surgeries = surgeries ?? new List<string>(); 
            AddSurgeon(this);
        }

        public void PerformSurgery (string surgery)
        {
            if(Surgeries.Count >= MaxSurgeriesPerShift)
            {
                throw new InvalidOperationException("Cannot perform more than 2 surgeries per shift");         
            }
            Surgeries.Add(surgery);
        }


        internal static void AddSurgeon(Surgeon surgeon)
        {
            if (surgeon == null)
            {
                throw new ArgumentException("Surgeon cannot be null");
            }

            if (_surgeonList.Exists(s => s.Equals(surgeon)))
            {
                throw new InvalidOperationException("Surgeon already added");
            }

            _surgeonList.Add(surgeon);
        }
        
        public static void RemoveSurgeon(Surgeon surgeon)
        {
            if (surgeon == null)
            {
                throw new ArgumentException("Surgeon cannot be null");
            }

            if (!_surgeonList.Contains(surgeon))
            {
                throw new InvalidOperationException("Surgeon not found");
            }

            _surgeonList.Remove(surgeon);
        }
        
        public static IReadOnlyList<Surgeon> GetSurgeons()
        {
            return _surgeonList.AsReadOnly();
        }
        
        
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Surgeon))
            {
                return false;
            }

            Surgeon other = (Surgeon)obj;

            return this.Surgeries.Count == other.Surgeries.Count;
        }
        
        public override int GetHashCode()
        {
            return Surgeries.Count.GetHashCode();
        }
        
        public override string ToString()
        {
            return "Surgeon with"+Surgeries.Count+" surgeries performed.";
        }


        // public static void SetSurgeons(List<Surgeon> containerSurgeons)
        // {
        //     _surgeonList = containerSurgeons?? new List<Surgeon>();
        // }

        public Appointment ScheduleSurgery(DateTime date, Bill initialBill, Staff staff)
        {
            if (initialBill == null)
            {
                throw new ArgumentException("An appointment must be included in at least one bill.");
            }

            if (staff == null)
            {
                throw new ArgumentException("An appointment must be supported by at least one staff member.");
            }


            List<Bill> initialBillL = new List<Bill>();
            initialBillL.Add(initialBill);
            
            List<Staff> staffL = new List<Staff>();
            staffL.Add(staff);
            return new Appointment(date, Appointment.AppointmentType.Surgery, this, initialBillL, staffL);
        }

        public static void LoadExtent(IEnumerable<Surgeon> containerSurgeons)
        {
          _surgeonList.Clear();
          foreach (var surgeon in containerSurgeons)
          {

              new Surgeon(surgeon.Id,surgeon.Name,new List<Shift>(),surgeon.Surgeries);
          }
        }
    }
}

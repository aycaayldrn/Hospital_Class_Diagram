using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Appointment
    {
        [Serializable]
        public enum AppointmentType
        {
            Surgery,
            FollowUp,
            Consultation
        }
        public AppointmentType Type { get; set; }

        private static List<Appointment> _appointmentList = new List<Appointment>();

        private List<Bill> _bills = new List<Bill>();
        public IReadOnlyList<Bill> Bills => _bills.AsReadOnly();

        private List<Staff> _staffMembers = new List<Staff>();
        public IReadOnlyList<Staff> Staffs => _staffMembers.AsReadOnly();


        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Date cannot be null");
                }else if (value < DateTime.Now)
                {
                    throw new ArgumentException("Date cannot be earlier than today");
                }
                _date = value;
            }
        }

        
        public object AssignedDoctor { get; set; }
        
        
        private Patient _patient;
        public Patient Patient
        {
            get { return _patient; }
        }
        
        

        public Appointment(DateTime date, AppointmentType type, object assignedDoctor) 
        {
            if(type == AppointmentType.Surgery && assignedDoctor is not Surgeon)
            {
                throw new InvalidOperationException("Only surgeons can be assigned to surgery appointments. ");
            }
            
            Date = date;
            Type = type;
            AssignedDoctor = assignedDoctor;
            addAppointment(this);
        }
        public Appointment(){}

        //==================================================================================================================        
        //Associations: Appointment->"supported by"-Staff

        public void addStaffToAppointment(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentException("Staff member cannot be null");
            }
            if(_staffMembers.Contains(staff))
            {
                throw new InvalidOperationException("Staff member already assigned to appointment");
            }

            _staffMembers.Add(staff);
            staff.AddAppointmentToStaff(this);
        }

        public void removeStaffFromAppointment(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentException("Staff member cannot be null");
            }

            if (!_staffMembers.Contains(staff))
            {
                throw new InvalidOperationException("Staff member is not assigned to this appointment");
            }

            if(_staffMembers.Count == 1)
            {
                throw new InvalidOperationException("There should be at least one staff member assigned. Cannot delete last member.");
            }

            _staffMembers.Remove(staff);
            staff.RemoveAppointmentFromStaff(this);
        }



//==================================================================================================================        
//Associations: Patient->"visits"-Appointment


        public void assignPatient(Patient patient)
        {
            if (patient==null)
            {
                throw new ArgumentException("Patient cannot be null");
            }
            
            if (_patient!= null)
            {
                throw new InvalidOperationException("Patient already assigned to appointment");
            }

            _patient = patient;
            if (!patient.GetPatientsAppointments().Contains(this))
            {
                patient.addAppointmentForPatient(this);
            }
        }
        
        
        public void deletePatient()
        {
            if (_patient != null && _patient.GetPatientsAppointments().Contains(this))
            {
                _patient.removeAppointmentFromPatient(this);
            }
            _patient = null;
            
           
        }

//==================================================================================================================
//Appointment-included in-Bill (one to many bills)

        public void AddBillToAppointment(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentException("Bill cannot be null");
            }

            if (_bills.Contains(bill)){
                _bills.Add(bill);
                bill.AddAppointmentToBill(this);
            }
            else
            {
                throw new InvalidOperationException("Bill already assigned to this appointment.");
            }
        }
        
        public void RemoveBillFromAppointment(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentException("Bill cannot be null");
            }
            if (!_bills.Contains(bill))
            {
                throw new InvalidOperationException("The specified appointment is not associated with this bill.");
            }
            if (_bills.Count == 1)
            {
                throw new InvalidOperationException("An appointment must be included in at least one bill.");
            }
            
            _bills.Remove(bill);
            bill.RemoveAppointmentFromBill(this);
        }

//==================================================================================================================
//Class extent methods
        internal static void addAppointment(Appointment appointment)
        {
            if (appointment== null)
            {
                throw new ArgumentException("Apointment cannot be null");
            }
           

            if (_appointmentList.Exists(a=>a.Equals(appointment)))
            {
                throw new InvalidOperationException("Appointment already added");
            }
            _appointmentList.Add(appointment);
        }


        public static void removeAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentException("Appointment cannot be null");
            }

            if (!_appointmentList.Contains(appointment))
            {
                throw new InvalidOperationException("appointment not found!");
            }
            
            appointment.deletePatient();
            _appointmentList.Remove(appointment);
        }

        public static List<Appointment> GetAppointments()
        {
            return new List<Appointment>(_appointmentList.AsReadOnly());
        }
        
        public static void LoadExtent(IEnumerable<Appointment> newAppointments)
        {
            _appointmentList.Clear(); 

            foreach (var appointment in newAppointments)
            {
                
                new Appointment(appointment.Date, appointment.Type, appointment.AssignedDoctor);
            }
        }
        
        
//==================================================================================================================
//Helper methods
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Appointment))
            {
                return false;
            }

            Appointment a = (Appointment)obj;

            return _date == a._date;
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode();
        }

        public override string ToString()
        {
            return "Date: " + Date
                +" Type: "+Type
                +" Assigned doctor: "+ AssignedDoctor;
        }

       
    }
    
 
    
    
    
}
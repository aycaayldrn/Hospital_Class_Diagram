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
        
        private Physician _physician;
        public Physician Physician
        {
            get { return _physician; }
        }  
        

        public Appointment(DateTime date, AppointmentType type, object assignedDoctor, List<Bill> initialBills, List<Staff> staffs) 
        {
            if(type == AppointmentType.Surgery && assignedDoctor is not Surgeon)
            {
                throw new InvalidOperationException("Only surgeons can be assigned to surgery appointments. ");
            }
            
            Date = date;
            Type = type;
            AssignedDoctor = assignedDoctor;

            if(initialBills == null || initialBills.Count == 0)
            {
                throw new ArgumentException("An appointment must be included in at least one bill");
            }

            foreach (var bill in initialBills)
            {
                if (!_bills.Contains(bill))
                {
                    AddBillToAppointment(bill);
                }
            }
            
            if(staffs == null || staffs.Count == 0)
            {
                throw new ArgumentException("An appointment must be supported by at least one staff member");
            }

            foreach (var staff in staffs)
            {
                if (!_staffMembers.Contains(staff))
                {
                    addStaffToAppointment(staff);
                }
            }
            
            addAppointment(this);
        }

        public Appointment(){}

        //==================================================================================================================        
        //Associations: Appointment-Physician

        public void AddPhysicianToAppointment(Physician physician)
        {
            if(physician == null) { throw new ArgumentNullException("Physician can't be null");}

            if(_physician == physician)
            {
                throw new InvalidOperationException("Physician already assigned");
            }
            _physician = physician;

            if (physician.GetAppointments().Contains(this))
            {
                physician.addAppointmentForPhysician(this);
            }
        }

        public void RemovePhysicianFromAppointment(Physician physician)
        {
            if (physician == null) { throw new ArgumentNullException("Physician can't be null"); }

            if ( _physician.GetAppointments().Contains(this))
            {
                _physician.RemoveAppointmentFromPhysician(this);
            }
            _physician= null;
        }

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
            if (!staff.Appointments.Contains(this))
            {
                staff.AddAppointmentToStaff(this);
            }
            
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

            if (staff.Appointments.Contains(this))
            {
                staff.RemoveAppointmentFromStaff(this);
            }
            
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

            if (_bills.Contains(bill))
            {
                throw new InvalidOperationException("Bill already added");
            }
            _bills.Add(bill);

            if (!bill.Appointments.Contains(this))
            {
                bill.AddAppointmentToBill(this);
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
            if (bill.Appointments.Contains(this))
            {
                bill.RemoveAppointmentFromBill(this);
            }
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

                if (appointment.Bills == null || appointment.Bills.Count == 0)
                {
                    throw new InvalidOperationException("Each appointment must have at least one bill.");
                }
                if(appointment.Staffs ==null || appointment.Staffs.Count == 0)
                {
                    throw new InvalidOperationException("Each appointment must be supported by at least one staff member.");
                }
                
                var newAppointment = new Appointment(
                   appointment.Date,
                   appointment.Type,
                   appointment.AssignedDoctor,
                   appointment.Bills.ToList(),
                   appointment.Staffs.ToList()
                );

                foreach (var additionalBill in appointment.Bills.Skip(1))
                {
                    newAppointment.AddBillToAppointment(additionalBill);
                }
                if (appointment.Patient != null)
                {
                    newAppointment.assignPatient(appointment.Patient);
                }
                foreach (var additionalStaff in appointment.Staffs.Skip(1))
                {
                    newAppointment.addStaffToAppointment(additionalStaff);
                }
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
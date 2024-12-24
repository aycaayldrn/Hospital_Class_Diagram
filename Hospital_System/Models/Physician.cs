using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models;

    [Serializable]
    public class Physician: Doctor
    {

        private static List<Physician> _physicianList = new List<Physician>();
        private string _specialization;
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Specialization can't be empty");
                }

                _specialization = value;
            }
        }

        public Physician(int id,string name,string specialization):base(id,name)
        {
            Specialization = specialization;
            AddPhysician(this);
        }
        
        public Physician()
        {
        }
        
        private List<Prescription> _prescriptions = new List<Prescription>();
        private List<Patient> _patients = new List<Patient>();
        private List<Appointment> _appointments = new List<Appointment>();

    //==================================================================================================================
    //Associations: Agregation Physician-appointment

    public void addAppointmentForPhysician(Appointment appointment)
    {
        if (appointment == null) { throw new ArgumentNullException("Appointment cant be null"); }

        if (_appointments.Contains(appointment))
        {
            throw new InvalidOperationException("Appointment already exists ");
        }

        _appointments.Add(appointment);

        if(appointment.Physician != this)
        {
            appointment.AddPhysicianToAppointment(this);
        }
    }

    public void RemoveAppointmentFromPhysician(Appointment appointment)
    {
        if (appointment == null) { throw new ArgumentNullException("Appointment cant be null"); }

        if (!_appointments.Contains(appointment))
        {
            throw new InvalidOperationException("Appointment is not on the list ");
        }

        _appointments.Remove(appointment);

        if (appointment.Physician == this)
        {
            appointment.RemovePhysicianFromAppointment(this);
        }
    }

    public IReadOnlyList<Appointment> GetAppointments()
    {
        return _appointments.AsReadOnly();
    }
    //==================================================================================================================
    //Associations: Agregation Physician-prescription

    public void addPrescriptiont(Prescription prescription)
        {
            if (prescription==null)
            {
                throw new ArgumentException("Prescription can't be null");
            }

            if (_prescriptions.Contains(prescription))
            {
                throw new InvalidOperationException("Prerscription already exists ");
                
            }
            _prescriptions.Add(prescription);

            if (prescription.Physician != this)
            {
                prescription.assignPrescriptionToPhysycian(this);
            }
        }    
        
        public void removePrescriptiont(Prescription prescription)
        {
            if (prescription==null)
            {
                throw new ArgumentException("Prescription can't be null");
            }

            if (!_prescriptions.Contains(prescription))
            {
                throw new InvalidOperationException("No such element in list");
                
            }
            _prescriptions.Remove(prescription);

            if (prescription._physician!=null&&prescription._physician.Equals(this))
            {
                prescription.deletePrescriptionByPhyscian();
            }
        }

        public IReadOnlyList<Prescription> GetPrescriptions()
        {
            return _prescriptions.AsReadOnly();
        }

    //==================================================================================================================
    //Associations: Agregation Physician-patient

    public void addPatientToPhysician(Patient patient)
    {
        if (patient == null)
        {
            throw new ArgumentException("Patient can't be null");
        }

        if (_patients.Contains(patient))
        {
            throw new InvalidOperationException("Patient is already assigned to this department");

        }
        _patients.Add(patient);
        if (patient.Physician != this)
        {
            patient.assignPhysicianToPatient(this);
        }
    }

    public void removePatientFromPhysician(Patient patient)
    {
        if (patient == null)
        {
            throw new ArgumentException("Patient can't be null");
        }

        if (!_patients.Contains(patient))
        {
            throw new InvalidOperationException("No such patient in list");

        }
        _patients.Remove(patient);
        if (patient.Physician == this)
        {
            patient.deletePatient();
        }
    }
    public IReadOnlyList<Patient> GetPatients()
    {
        return _patients.AsReadOnly();
    }
    //==================================================================================================================

    //class extent methods
    internal static void AddPhysician(Physician physician)
        {
            if (physician == null)
            {
                throw new ArgumentException("Physician cannot be null");
            }

            if (_physicianList.Exists(p => p.Equals(physician)))
            {
                throw new InvalidOperationException("Physician already added");
            }

            _physicianList.Add(physician);
        }


        public static void RemovePhysician(Physician physician)
        {
            if (physician == null)
            {
                throw new ArgumentException("Physician cannot be null");
            }

            if (!_physicianList.Contains(physician))
            {
                throw new InvalidOperationException("Physician not found");
            }

            _physicianList.Remove(physician);
        }

        public static IReadOnlyList<Physician> GetPhysicians()
        {
            return _physicianList.AsReadOnly();
        }
//==================================================================================================================
//Helper methods
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Physician))
            {
                return false;
            }

            Physician other = (Physician)obj;

            return string.Equals(this._specialization, other._specialization, StringComparison.OrdinalIgnoreCase);
        }


        public override int GetHashCode()
        {
            return _specialization.ToLowerInvariant().GetHashCode();
        }


        public override string ToString()
        {
            return "Physician Specialization:" + Specialization;
        }


     

        public Prescription WritePrescription(int id, string medicationName, float dosage, int duration,
            bool redPrescription, Bill initialBill)
        {
            return new Prescription(id, medicationName, dosage, duration, redPrescription, initialBill);
        }

        public Appointment ScheduleAppointment(DateTime date, Appointment.AppointmentType type, Bill initialBill, Staff staff)
        {
            if (type == Appointment.AppointmentType.Surgery)
            {
                throw new InvalidOperationException("Only surgeons can schedule surgery appointments.");
            }
            if (initialBill == null)
            {
                throw new ArgumentException("An appointment must be included in at least one bill.");
            }

            if (staff == null)
            {
                throw new ArgumentException("An appointment must be supported by at least one staff member.");
            }
            return new Appointment(date, type, this, initialBill,staff  );
        }

        public void AssignPatient(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException("Patient cannot be null");
            }

            if (_patients.Contains(patient))
            {
                throw new InvalidOperationException("This patient is already assigned to this physician.");
            }

            _patients.Add(patient);

        }

        public static void LoadExtent(IEnumerable<Physician> containerPhysicians)
        {
            _physicianList.Clear();
            foreach (var physician in containerPhysicians)
            {

                new Physician(physician.Id,physician.Name,physician.Specialization);
            }
        }
    }

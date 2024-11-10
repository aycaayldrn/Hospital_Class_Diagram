using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models;

    [Serializable]
    public class Physician
    {
        public Physician()
        {
        }

        private static List<Physician> _physicianList = new List<Physician>();
        private List<Patient> _patients = new List<Patient>();

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

        public Physician(string specialization)
        {
            Specialization = specialization;
            AddPhysician(this);
        }

        private static void AddPhysician(Physician physician)
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


        public static void SetPhysicians(List<Physician> containerPhysicians)
        {
            _physicianList = containerPhysicians ?? new List<Physician>();
        }

        public Prescription WritePrescription(int id, string medicationName, float dosage, int duration,
            bool redPrescription)
        {
            return new Prescription(id, medicationName, dosage, duration, redPrescription);
        }

        public Appointment ScheduleAppointment(DateTime date, Appointment.AppointmentType type)
        {
            if (type == Appointment.AppointmentType.Surgery)
            {
                throw new InvalidOperationException("Only surgeons can schedule surgery appointments.");
            }

            return new Appointment(date, type, this);
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

        public IReadOnlyList<Patient> GetPatients()
        {
            return _patients.AsReadOnly();
        }
    }

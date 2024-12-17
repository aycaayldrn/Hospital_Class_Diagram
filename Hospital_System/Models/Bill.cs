using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable]
    public class Bill
    {
        private static List<Bill> _billList = new List<Bill>();

        private List<Prescription> _prescriptions = new List<Prescription>();
        public IReadOnlyList<Prescription> Prescriptions => _prescriptions;

        private List<Service> _services = new List<Service>();
        public IReadOnlyList<Service> Services => _services.AsReadOnly();

        private List<Appointment> _appointments = new List<Appointment>();
        public IReadOnlyList<Appointment> Appointments => _appointments.AsReadOnly();

        private static double _taxRate = 0.15;
        public static double TaxRate
        {
            get => _taxRate;
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Tax rate must between 0 and 1");
                }
                _taxRate = value;
            }
        }



        private Patient _patient;
        public Patient Patient
        {
            get { return _patient; }
        }


        public int Number { get; set; }


        private double _totalCost;
        public double TotalCost
        {
            get => _totalCost;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Total cost can't be negative");
                }
                _totalCost = value;
            }
        }
        public double FinalCost => CalculateFinalCost();




        public Bill(int number, double totalCost)
        {
            Number = number;
            TotalCost = totalCost;
            addBill(this);
        }
        public Bill() { }

        //==================================================================================================================
        //Composition:Patient->"pays"-Bill
        public void assignPatientBill(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentException("Patient cannot be null");
            }

            if (_patient != null)
            {
                throw new InvalidOperationException("Patient already assigned to this");
            }

            _patient = patient;
            if (!patient.GetPatientsBills().Contains(this))
            {
                patient.addBillForPatient(this);
            }
        }

        public void deleteBill()
        {
            if (_patient != null && _patient.GetPatientsBills().Contains(this))
            {
                _patient.removeBillFromPatient(this);
            }
            _patient = null;
        }


        public void changeBill(Patient newPatient)
        {
            if (newPatient == null)
            {
                throw new ArgumentException("Patient cannot be null");
            }

            if (_patient == newPatient)
            {
                throw new InvalidOperationException("the same patients are the same!");
            }

            if (_patient != null)
            {
                _patient.removeBillFromPatient(this);
            }
            newPatient.addBillForPatient(this);
            _patient = newPatient;
        }

        //==================================================================================================================
        //Bill->"includes"-Prescription

        public void assignPrescriptionToBill(Prescription prescription)
        {
            if (prescription == null)
            {
                throw new ArgumentException(nameof(prescription), "Prescription cannot be null!");

            }

            if (_prescriptions.Contains(prescription))
            {
                throw new InvalidOperationException("Prescription already exists in the bill");

            }

            _prescriptions.Add(prescription);
            prescription.addBillToPrescription(this);


        }

        public void removePrescriptionFromBill(Prescription prescription)
        {

            if (prescription == null)
            {
                throw new ArgumentNullException(nameof(prescription), "Prescription cannot be null!");
            }

            if (!_prescriptions.Contains(prescription))
            {
                throw new InvalidOperationException("The specified prescription is not part of this bill.");
            }
            

            _prescriptions.Remove(prescription);
            prescription.RemoveBillFromPrescription(this);
        }


        //==================================================================================================================
        //Bill-includes- Service
        public void AddServiceToBill(Service service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service), "Service cannot be null");
            }

            if (_services.Contains(service))
            {
                throw new InvalidOperationException("This service is already included in the bill.");
            }

            _services.Add(service);
            service.assignBillToService(this);
            UpdateTotalCost();
        }
    

        public void RemoveServiceFromBill(Service service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));   
            }
            if (!_services.Contains(service))
            {
                throw new InvalidOperationException("The specified service is not associated with this bill.");
            }

            if (_services.Count == 1)
            {
                throw new InvalidOperationException("A bill must include at least one service. Cannot remove the last service.");
            }

            _services.Remove(service);
            service.RemoveBillFromService(this);
            UpdateTotalCost();
        }

        //==================================================================================================================
        //Bill->"includes"-Appointment
        public void AddAppointmentToBill(Appointment appointment)
        {
            if(appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }
            if (_appointments.Contains(appointment)){
                throw new InvalidOperationException("This Bill is already includes the appointment.");
            }
            _appointments.Add(appointment);
            appointment.AddBillToAppointment(this);
            UpdateTotalCost();
        }

        public void RemoveAppointmentFromBill(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }
            if (!_appointments.Contains(appointment)){
                throw new InvalidOperationException("This Bill does not includes the appointment.");
            }
            _appointments.Remove(appointment);
            appointment.RemoveBillFromAppointment(this);
            UpdateTotalCost();
        }

        //==================================================================================================================
        //Class Extent Methods
        internal static void addBill(Bill bill)
    {
        if (bill == null)
        {
            throw new ArgumentException("Bill cannot be null");
        }


        if (_billList.Exists(a => a.Equals(bill)))
        {
            throw new InvalidOperationException("Bill already added");
        }
        _billList.Add(bill);
    }





    public static void removeBill(Bill bill)
    {
        if (bill == null)
        {
            throw new ArgumentException("Bill cannot be null");
        }

        if (!_billList.Contains(bill))
        {
            throw new InvalidOperationException("Bill not found!");
        }
        bill.deleteBill();
        _billList.Remove(bill);
    }


    public static IReadOnlyList<Bill> GetBills()
    {
        return _billList.AsReadOnly();
    }


    public static void LoadExtent(IEnumerable<Bill> containerBills)
    {
        _billList.Clear();
        foreach (var bill in containerBills)
        {

            new Bill(bill.Number, bill.TotalCost);
        }
    }
    //==================================================================================================================        
    //Helper methods
    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Bill))
        {
            return false;
        }

        Bill a = (Bill)obj;

        return this.Number == a.Number && this._totalCost == a._totalCost;
    }

    public override int GetHashCode()
    {
        return Number.GetHashCode() ^ _totalCost.GetHashCode();
    }

    public override string ToString()
    {
        return "Bill: " + Number + " " + "Total cost: " + _totalCost + " " + "Final cost: " + FinalCost;
    }



    // internal static void Clear()
    // {
    //     _billList.Clear();
    // }



    private double CalculateFinalCost()
    {
        double taxRate = TotalCost * TaxRate;
        return TotalCost + taxRate;
    }

    private void UpdateTotalCost()
    {
        TotalCost = 0;
        foreach (var service in _services)
        {
            TotalCost += service.Price;
        }
    }

}
    
}

namespace Hospital_System.Models;
[Serializable]
public class ContainerForSavingClasses
{
    public List<Appointment> Appointments { get; set; }
    public List<Bill> Bills { get; set; }
    
    public List<Department> Departments { get; set; }

    public List<Equipment> Equipments { get; set; }
    public List<Fellow> Fellows { get; set; }
    public List<Insurance_Provider> InsuranceProviders { get; set; }
    public List<Nurse> Nurses { get; set; }
    public List<Patient> Patients { get; set; }
    
    public List<Physician> Physicians { get; set; }
    public List<Prescription> Prescriptions { get; set; }
    public List<Room> Rooms { get; set; }
    public List<Service> Services { get; set; }
    public List<Shift> Shifts { get; set; }
    public List<Staff> Staff { get; set; }
    public List<Surgeon> Surgeons { get; set; }
    
    //
    public ContainerForSavingClasses()
    {
        Appointments = new List<Appointment>();
        Bills = new List<Bill>();
       
        Departments = new List<Department>();
        
        Equipments = new List<Equipment>();
        
        Fellows = new List<Fellow>();
        InsuranceProviders = new List<Insurance_Provider>();
        Nurses = new List<Nurse>();
        Patients = new List<Patient>();
        Physicians = new List<Physician>();
        Prescriptions = new List<Prescription>();
        Rooms = new List<Room>();
        Services = new List<Service>();
        Shifts = new List<Shift>();
        Staff = new List<Staff>();
        Surgeons = new List<Surgeon>();
    }
}
using System.Xml;
using System.Xml.Serialization;

namespace Hospital_System.Models;

public static class SerializeToFIle
{

    public static void saveAll(String path = "hospital.xml")
    {
        ContainerForSavingClasses container = new ContainerForSavingClasses
        { 
            Appointments = Appointment.GetAppointments().ToList(),
            Bills = Bill.GetBills().ToList(),
            Departments = Department.GetDepartments().ToList(),
            Equipments = Equipment.GetEquipments().ToList(),
            Fellows = Fellow.GetFellows().ToList(),
            InsuranceProviders = Insurance_Provider.GetProvider().ToList(),
            Nurses = Nurse.GetNurses().ToList(),
            Patients = Patient.GetPatients().ToList(),
            Physicians = Physician.GetPhysicians().ToList(),
            Prescriptions = Prescription.GetPrescriptions().ToList(),
            Rooms = Room.GetRooms().ToList(),
            Services = Service.GetServices().ToList(),
            Shifts = Shift.GetShifts().ToList(),
            Staff = Staff.GetStaffMembers().ToList(),
            Surgeries = Surgerie.GetSurgeries().ToList(),
            Surgeons = Surgeon.GetSurgeons().ToList()
        };

        try
        {
            using (StreamWriter file = File.CreateText(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContainerForSavingClasses));
                using (XmlTextWriter writer = new XmlTextWriter(file))
                {
                    xmlSerializer.Serialize(writer, container);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }



    public static bool loadAll(String path = "hospital.xml")
    {

        if (!File.Exists(path))
        {
            Console.WriteLine("Data file not found.");
            return false;
        }

        try
        {
            using (StreamReader file = File.OpenText(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContainerForSavingClasses));
                ContainerForSavingClasses container = (ContainerForSavingClasses)xmlSerializer.Deserialize(file);

                Appointment.SetAppointments(container.Appointments);
                Bill.SetBills(container.Bills);
                Department.SetDepartments(container.Departments);
                Equipment.SetEquipments(container.Equipments);
                Fellow.SetFellows(container.Fellows);
                Insurance_Provider.SetProviders(container.InsuranceProviders);
                Nurse.SetNurses(container.Nurses);
                Patient.SetPatients(container.Patients);
                Physician.SetPhysicians(container.Physicians);
                Prescription.SetPrescriptions(container.Prescriptions);
                Room.SetRooms(container.Rooms);
                Service.SetServices(container.Services);
                Shift.SetShifts(container.Shifts);
                Staff.SetStaffMembers(container.Staff);
                Surgerie.SetSurgeries(container.Surgeries);
                Surgeon.SetSurgeons(container.Surgeons);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
            return false;
        }
    }
}

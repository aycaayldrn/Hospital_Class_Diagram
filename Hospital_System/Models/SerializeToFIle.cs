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
               
                Appointment.LoadExtent(container.Appointments);
                Bill.LoadExtent(container.Bills);
                Department.LoadExtent(container.Departments);
                Equipment.LoadExtent(container.Equipments);
                Fellow.LoadExtent(container.Fellows);
                Insurance_Provider.LoadExtent(container.InsuranceProviders);
                Nurse.LoadExtent(container.Nurses);
                Patient.LoadExtent(container.Patients);
                Physician.LoadExtent(container.Physicians);
                Prescription.LoadExtent(container.Prescriptions);
                Room.LoadExtent(container.Rooms);
                Service.LoadExtent(container.Services);
                Shift.LoadExtent(container.Shifts);
                Staff.LoadExtent(container.Staff);
                Surgeon.LoadExtent(container.Surgeons);
                
                
                
                
                // foreach (var service in container.Services)
                // {
                //     Service.AddService(service);
                // }
                //
                // foreach (var shift in container.Shifts)
                // {
                //     Shift.AddShift(shift);
                // }
                //
                // foreach (var staffMem in container.Staff)
                // {
                //     Staff.AddStaff(staffMem);
                // }
                
                // foreach (var surgeon in container.Surgeons)
                // {
                //     Surgeon.AddSurgeon(surgeon);
                // }
                
                
                // foreach (var dep in container.Departments)
                // {
                //     Department.addDepartment(dep);
                // }
                // foreach (var equipment in container.Equipments)
                // {
                //     Equipment.addEqiupment(equipment);
                // }
                //
                // foreach (var fellow in container.Fellows)
                // {
                //     Fellow.addFellow(fellow);
                // }
                //
                // foreach (var insuranceP in container.InsuranceProviders)
                // {
                //     Insurance_Provider.addProvider(insuranceP);
                // }
                //
                // foreach (var nurse in container.Nurses)
                // {
                // }
                //
                // foreach (var patient in container.Patients)
                // {
                //     Patient.AddPatient(patient);
                // }
                //
                // foreach (var physician in container.Physicians)
                // {
                //     Physician.AddPhysician(physician);
                // }
                //
                //
                // foreach (var prescription in container.Prescriptions)
                // {
                //     Prescription.AddPrescription(prescription);
                // }
                
                
                // foreach (var room in container.Rooms)
                // {
                //     Room.AddRoom(room);
                // }
                
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

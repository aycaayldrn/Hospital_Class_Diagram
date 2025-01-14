namespace Hospital_System.Models;

public class DoctorDoctorRoleManager
{
    public static Doctor ChangeRole(Doctor doctor, string newRole)
    {
        if (doctor is Surgeon || doctor is General_Practitioner)
        {
            throw new InvalidOperationException("Cannot change roles for Surgeon or General Practitioner.");
        }

        switch (newRole.ToLower())
        {
            case "resident":
                if (doctor is Fellow)
                {
                    Fellow.removeFellow((Fellow)doctor);
                    
                }else if (doctor is Physician)
                {
                    Physician.RemovePhysician((Physician)doctor);
                }
                else
                {
                    Doctor.RemoveStaff(doctor);
                }
                return new Resident(doctor.Id, doctor.Name, new List<Shift>(){new Shift()});
            case "fellow":
                if (doctor is Fellow)
                {
                    Fellow.removeFellow((Fellow)doctor);
                    
                }else if (doctor is Physician)
                {
                    Physician.RemovePhysician((Physician)doctor);
                }
                else
                {
                    Doctor.RemoveStaff(doctor);
                }
                return new Fellow(doctor.Id, doctor.Name, new List<Shift>(){new Shift()}, "New Research");
            case "physician":
                if (doctor is Fellow)
                {
                    Fellow.removeFellow((Fellow)doctor);
                    
                }else if (doctor is Physician)
                {
                    Physician.RemovePhysician((Physician)doctor);
                }
                else
                {
                    Doctor.RemoveStaff(doctor);
                }
                return new Physician(doctor.Id, doctor.Name,"Smth", new List<Shift>(){new Shift()});
            default:
                throw new ArgumentException("Invalid role.");
        }
    }
}
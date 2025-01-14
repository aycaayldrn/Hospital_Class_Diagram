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
                return new Resident(doctor.Id, doctor.Name, new List<Shift>());
            case "fellow":
                return new Fellow(doctor.Id, doctor.Name, new List<Shift>(), "New Research");
            case "physician":
                return new Physician(doctor.Id, doctor.Name,"", new List<Shift>());
            default:
                throw new ArgumentException("Invalid role.");
        }
    }
}
namespace Tests;
using Hospital_System.Models;

public class DoctorRoleManagerTests
{
    [Test]
    public void Trying_to_create_Doctor_and_change_role_to_resident()
    {
        Physician physician = new Physician(1,"Test1","Test1", new List<Shift>(){new Shift()});
        Resident res = (Resident)DoctorDoctorRoleManager.ChangeRole(physician, "resident");
        Staff.RemoveStaff(res);
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_create_Doctor_and_change_role_to_fellow()
    {
        Physician physician = new Physician(1,"Test1","Test1", new List<Shift>(){new Shift()});
        Fellow res = (Fellow)DoctorDoctorRoleManager.ChangeRole(physician, "fellow");
        Fellow.removeFellow(res);
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_create_Doctor_and_change_role_to_physician()
    {
        Resident res = new Resident(1,"Test1", new List<Shift>(){new Shift()});
        Physician physician = (Physician)DoctorDoctorRoleManager.ChangeRole(res, "physician");
        Physician.RemovePhysician(physician);
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_change_role_of_Surgeon_should_throw_InvalidOperationException()
    {
        Surgeon surgeon = new Surgeon(1, "Test1", new List<Shift>(){new Shift()});
        try
        {
            DoctorDoctorRoleManager.ChangeRole(surgeon, "gfdsfjjs");
            Assert.Fail();
        }
        catch (InvalidOperationException e)
        {
            Surgeon.RemoveSurgeon(surgeon);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_change_role_of_General_Practitioner_should_throw_InvalidOperationException()
    {
        General_Practitioner pract = new General_Practitioner(1, "Test1", new List<Shift>(){new Shift()}, "Test1");
        try
        {
            DoctorDoctorRoleManager.ChangeRole(pract, "gfdsfjjs");
            Assert.Fail();
        }
        catch (InvalidOperationException e)
        {
            Staff.RemoveStaff(pract);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Doctor_and_change_to_wrong_role_should_throw_ArgumentException()
    {
        Resident res = new Resident(1,"Test1", new List<Shift>(){new Shift()});
        try
        {
            DoctorDoctorRoleManager.ChangeRole(res, "gfdsfjjs");
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
            Staff.RemoveStaff(res);
            Assert.Pass();
        }
    }
}
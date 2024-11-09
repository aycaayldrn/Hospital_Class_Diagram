namespace Tests;
using Hospital_System.Models;

public class PatientTests
{
    [Test]
    public void Trying_to_create_patient_with_null_name_should_throw_ArgumentNullException()
    {
        try
        {
            Patient p = new Patient(1, null, new DateTime(2024, 8, 18), false);

            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_patient_with_birthday_in_future_should_throw_ArgumentNullException()
    {
        try
        {
            Patient p = new Patient(1, null, new DateTime(3000, 8, 18), false);

            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Patient_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String name = "Test2";
        Patient p = new Patient(1, name, new DateTime(2024, 8, 18), false);

        Assert.That(p.Name, Is.EqualTo(name));
    }

    [Test]
    public void Trying_to_get_Age()
    {
        DateTime date = new DateTime(2003, 12, 18);
        
        Patient p = new Patient(1, "name", date, false);
        Assert.That(p.Age, Is.EqualTo(date.Month > DateTime.Today.Month ? DateTime.Today.Year - date.Year - 1 : DateTime.Today.Year - date.Year));
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Patients_and_SetAppointments()
    {
        
        Patient.SetPatients(new List<Patient>());
        List<Patient> lp = new List<Patient>{new ( 1,"Test1",new DateTime(2005),false), new ( 2,"Test2",new DateTime(2005),false), new (3,"Test3",new DateTime(2005),false)};
        
        Patient.SetPatients(lp);
        
        Assert.That(Patient.GetPatients(), Is.EqualTo(lp));
        
        Patient.SetPatients(new List<Patient>());
    }
    
    [Test]
    public void Trying_to_create_same_Patient_throws_InvalidOperationException()
    {
        Patient b = new Patient(1,"Test1",new DateTime(2005),false);
        try
        {
            Patient b2 = new Patient(1,"Test1",new DateTime(2005),false);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Patient_InvalidOperationException_excepted()
    {
        try
        {
            Patient.RemovePatient(new Patient());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Patients_and_save_them_to_file()
    {
        Patient.SetPatients(new List<Patient>());
        List<Patient> la = new List<Patient>{new ( 1,"Test1",new DateTime(2005),false), new ( 2,"Test2",new DateTime(2005),false), new (3,"Test3",new DateTime(2005),false)};
        
        SerializeToFIle.saveAll();
        
        Patient.SetPatients(new List<Patient>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Patient.GetPatients(), Is.EqualTo(la));
    }
}
namespace Tests;
using Hospital_System.Models;

public class PatientTests
{
    [Test]
    public void Trying_to_create_patient_with_null_name_should_throw_ArgumentNullException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        try
        {
            Patient p = new Patient(1, null, new DateTime(2024, 8, 18));

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
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        try
        {
            Patient p = new Patient(1, null, new DateTime(3000, 8, 18));

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
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        String name = "Test2";
        Patient p = new Patient(1, name, new DateTime(2024, 8, 18));

        Assert.That(p.Name, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_set_Diagnoses_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        Patient p = new Patient(1, "Test2", new DateTime(2024, 8, 18));

        try
        {
            p.Diagnoses = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_set_Treatments_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        Patient p = new Patient(1, "Test2", new DateTime(2024, 8, 18));

        try
        {
            p.Treatments = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_set_Allergies_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        Patient p = new Patient(1, "Test2", new DateTime(2024, 8, 18));

        try
        {
            p.Allergies = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_get_Age()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

        DateTime date = new DateTime(2003, 12, 18);
        
        Patient p = new Patient(1, "name", date);
        Assert.That(p.Age, Is.EqualTo(date.Month > DateTime.Today.Month || date.Day > DateTime.Today.Day ? DateTime.Today.Year - date.Year - 1 : DateTime.Today.Year - date.Year));
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Patients_and_SetAppointments()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        
        List<Patient> lp = new List<Patient>{new ( 1,"Test1",new DateTime(2005)), new ( 2,"Test2",new DateTime(2005)), new (3,"Test3",new DateTime(2005))};
        
        Assert.That(Patient.GetPatients(), Is.EqualTo(lp));
    }
    
    [Test]
    public void Trying_to_create_same_Patient_throws_InvalidOperationException()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        
        Patient b = new Patient(1,"Test1",new DateTime(2005));
        try
        {
            Patient b2 = new Patient(1,"Test1",new DateTime(2005));
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Patient.RemovePatient(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Patient_InvalidOperationException_excepted()
    {
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }

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
        foreach (var o in Patient.GetPatients().ToList())
        {
            Patient.RemovePatient(o);
        }
        
        List<Patient> la = new List<Patient>{new ( 1,"Test34",new DateTime(2009)), new ( 2,"Test2",new DateTime(2005)), new (3,"Test3",new DateTime(2005))};
        
        SerializeToFIle.saveAll();
        
        foreach (Patient o in la.ToList())
        {
            Patient.RemovePatient(o);
        }
        
        SerializeToFIle.loadAll();

        Assert.That(Patient.GetPatients(), Is.EqualTo(la));
    }
}
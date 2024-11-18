namespace Tests;
using Hospital_System.Models;

public class PrescriptionTests
{
    [Test]
    public void Trying_to_create_Prescription_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        String name = "Test1";
        Prescription p = new Prescription(1, name, 0.3f, 4, false);

        Assert.That(p.MedicationName, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_null_name_throws_ArgumentNullException()
    {
        try
        {
            Prescription p = new Prescription(1, null, 0.3f, 4, false);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_specific_Dosage_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        float dosage = 1.4f;
        Prescription p = new Prescription(1, "Test2", dosage, 4, false);

        Assert.That(p.Dosage, Is.EqualTo(dosage));
        Prescription.RemovePrescription(p);
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_specific_Duration_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        int duration = 14;
        Prescription p = new Prescription(1, "Test3", 1.2f, duration, false);

        Assert.That(p.Duration, Is.EqualTo(duration));
        Prescription.RemovePrescription(p);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Prescriptions_and_SetAppointments()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        List<Prescription> lp = new List<Prescription>{new ( 1, "Test1", 1.2f, 4, false), new (2, "Test2", 1.2f, 4, false), new ( 3, "Test3", 1.2f, 4, false)};
        
        
        Assert.That(Prescription.GetPrescriptions(), Is.EqualTo(lp));
    }
    
    [Test]
    public void Trying_to_create_same_Prescription_throws_InvalidOperationException()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        Prescription b = new Prescription(5, "Test5", 1.2f, 4, false);
        try
        {
            Prescription b2 = new Prescription(5, "Test5", 1.2f, 4, false);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Prescription.RemovePrescription(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Prescription_InvalidOperationException_excepted()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        try
        {
            Prescription.RemovePrescription(new Prescription());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Prescriptions_and_save_them_to_file()
    {
        foreach (var o in Prescription.GetPrescriptions().ToList())
        {
            Prescription.RemovePrescription(o);
        }
        
        List<Prescription> la = new List<Prescription>{new ( 1, "Test1", 1.2f, 4, false), new (2, "Test2", 1.2f, 4, false), new ( 3, "Test3", 1.2f, 4, false)};
        
        SerializeToFIle.saveAll();
        
        foreach (Prescription o in la)
        {
            Prescription.RemovePrescription(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Prescription.GetPrescriptions(), Is.EqualTo(la));
    }
}
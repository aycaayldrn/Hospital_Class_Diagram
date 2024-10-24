namespace Tests;
using Hospital_System.Models;

public class PrescriptionTests
{
    [Test]
    public void Trying_to_create_Prescription_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Prescription p = new Prescription(1, name, 0.3f, 4, false);

        Assert.That(p.MedicationName, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_null_name_throws_ArgumentNullException()
    {
        try
        {
            Prescription p = new Prescription(1, null, 0.3f, 4, false);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_specific_Dosage_and_check_if_it_assigned_correctly()
    {
        float dosage = 1.4f;
        Prescription p = new Prescription(1, "Test", dosage, 4, false);

        Assert.That(p.Dosage, Is.EqualTo(dosage));
    }
    
    [Test]
    public void Trying_to_create_Prescription_with_specific_Duration_and_check_if_it_assigned_correctly()
    {
        int duration = 14;
        Prescription p = new Prescription(1, "Test", 1.2f, duration, false);

        Assert.That(p.Duration, Is.EqualTo(duration));
    }
}
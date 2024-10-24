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

            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Patient_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
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
}
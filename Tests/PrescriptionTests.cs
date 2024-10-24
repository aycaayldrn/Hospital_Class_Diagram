namespace Tests;
using Hospital_System.Models;

public class PrescriptionTests
{
    [Test]
    public void Trying_to_create_Prescription()
    {
        Prescription p = new Prescription(1, "Name", 0.3f, 4, false);
        
        Assert.Pass();
    }
    
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
}
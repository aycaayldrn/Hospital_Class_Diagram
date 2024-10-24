namespace Tests;
using Hospital_System.Models;

public class PhysicianTests
{
    [Test]
    public void Trying_to_assign_null_val_to_specialization_should_throw_ArgumentNullException()
    {
        try
        {
            Physician p = new Physician(null);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Physician_with_specific_specialization_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Physician p = new Physician(name);
        Assert.That(p.Specialization, Is.EqualTo(name));
    }
}
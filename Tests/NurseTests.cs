namespace Tests;
using Hospital_System.Models;


public class NurseTests
{
    [Test]
    public void Trying_to_assign_null_val_to_name_should_throw_ArgumentNullException()
    {
        try
        {
            Nurse n = new Nurse(1,null);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Nurse_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Nurse n = new Nurse(1,name);
        Assert.That(n.Name, Is.EqualTo(name));
    }

    [Test]
    public void Trying_to_use_DisplayNurseInfo_function()
    {
        Nurse n = new Nurse(1,"Test");
        
        n.DisplayNurseInfo();
        Assert.Pass();
    }
}
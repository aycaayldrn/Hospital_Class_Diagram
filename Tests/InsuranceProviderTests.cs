namespace Tests;
using Hospital_System.Models;

public class InsuranceProviderTests
{
    [Test]
    public void Trying_to_create_Insurance_Provider()
    {
        Insurance_Provider e = new Insurance_Provider(1,"Test");
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_assign_null_val_to_name_should_throw_ArgumentNullException()
    {
        try
        {
            Insurance_Provider e = new Insurance_Provider(1,null);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_assign_negative_val_to_id_should_throw_ArgumentNullException()
    {
        try
        {
            Insurance_Provider e = new Insurance_Provider(-1,"Test");
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Insurance_Provider_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Insurance_Provider e = new Insurance_Provider(1,name);
        Assert.That(e.Name, Is.EqualTo(name));
    }
}
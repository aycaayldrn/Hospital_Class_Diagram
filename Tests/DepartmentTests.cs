namespace Tests;
using Hospital_System.Models;

public class DepartmentTests
{
    [Test]
    public void Trying_to_set_empty_name_should_catch_Exception()
    {
        try
        {
            Department d = new Department(null);
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Department_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Department d = new Department(name);
        Assert.That(d.Name, Is.EqualTo(name));
    }
    
    
    [Test]
    public void Trying_to_create_Department()
    {
        Department d = new Department("Test");
        Assert.Pass();
    }
}
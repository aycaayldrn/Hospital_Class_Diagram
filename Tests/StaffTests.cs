namespace Tests;
using Hospital_System.Models;

public class StaffTests
{
    [Test]
    public void Trying_to_create_Staff_with_null_name_should_throw_ArgumentNullException()
    {
        try
        {
            Staff s = new Staff(1, null, "test");
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Staff_with_null_position_should_throw_ArgumentNullException()
    {
        try
        {
            Staff s = new Staff(1, "test", null);
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Staff_with_specific_position_and_check_if_it_assigned_correctly()
    {
        String name = "Joe";
        Staff s = new Staff(1, name, "Test");


        Assert.That(s.Name, Is.EqualTo(name));
    }
    
    [Test]
    public void Trying_to_create_Staff_with_specific_Name_and_check_if_it_assigned_correctly()
    {
        String pos = "type1231";
        Staff s = new Staff(1, "test", pos);


        Assert.That(s.Position, Is.EqualTo(pos));
    }
}
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
}
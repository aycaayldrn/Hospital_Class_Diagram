namespace Tests;
using Hospital_System.Models;

public class RoomsTests
{
    [Test]
    public void Trying_to_create_Room_with_null_type_should_throw_ArgumentNullException()
    {
        try
        {
            Room r = new Room(1,null, "test");
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Room_with_null_availability_should_throw_ArgumentNullException()
    {
        try
        {
            Room r = new Room(1,"test",null);
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Room_with_negative_roomNumber_should_throw_ArgumentNullException()
    {
        try
        {
            Room r = new Room(-1,"test","test");
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Number_and_check_if_it_assigned_correctly()
    {
        int number = 12;
        Room r = new Room(number,"test", "test");

        Assert.That(r.Number, Is.EqualTo(number));
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Availability_and_check_if_it_assigned_correctly()
    {
        String availability = "no";
        Room r = new Room(1,"test", availability);

        Assert.That(r.Availability, Is.EqualTo(availability));
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Type_and_check_if_it_assigned_correctly()
    {
        String type = "type";
        Room r = new Room(123,type, "test");

        Assert.That(r.Type, Is.EqualTo(type));
    }
}
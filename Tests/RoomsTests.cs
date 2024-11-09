namespace Tests;
using Hospital_System.Models;

public class RoomsTests
{
    [Test]
    public void Trying_to_create_Room_with_null_type_should_throw_ArgumentNullException()
    {
        try
        {
            Room r = new Room(1,null, "test2");
            
            Assert.Fail("Expected ArgumentException");
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
            Room r = new Room(1,"test1",null);
            
            Assert.Fail("Expected ArgumentException");
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
            
            Assert.Fail("Expected ArgumentException");
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
        Room r = new Room(number,"test3", "test");

        Assert.That(r.Number, Is.EqualTo(number));
        Room.RemoveRoom(r);
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Availability_and_check_if_it_assigned_correctly()
    {
        String availability = "no";
        Room r = new Room(1,"test4", availability);

        Assert.That(r.Availability, Is.EqualTo(availability));
        Room.RemoveRoom(r);
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Type_and_check_if_it_assigned_correctly()
    {
        String type = "type";
        Room r = new Room(123,type, "test5");

        Assert.That(r.Type, Is.EqualTo(type));
        Room.RemoveRoom(r);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Rooms_and_SetAppointments()
    {
        Room.SetRooms(new List<Room>());
        List<Room> lb = new List<Room>{new ( 2,"Test2","test"), new (3,"Test3","test"), new ( 4,"Test4","test")};
        
        Room.SetRooms(lb);
        
        Assert.That(Room.GetRooms(), Is.EqualTo(lb));
        
        Room.SetRooms(new List<Room>());
    }
    
    [Test]
    public void Trying_to_create_same_Room_throws_InvalidOperationException()
    {
        Room r = new Room(1,"Test","test");
        try
        {
            Room r2 = new Room(1,"Test","test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Room_InvalidOperationException_excepted()
    {
        try
        {
            Room.RemoveRoom(new Room());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Rooms_and_save_them_to_file()
    {
        Room.SetRooms(new List<Room>());
        List<Room> la = new List<Room>{new ( 2,"Test2","test"), new (3,"Test3","test"), new ( 4,"Test4","test")};
        
        SerializeToFIle.saveAll();
        
        Room.SetRooms(new List<Room>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Room.GetRooms(), Is.EqualTo(la));
    }
}
namespace Tests;
using Hospital_System.Models;

public class RoomsTests
{
    [Test]
    public void Trying_to_create_Room_with_negative_roomNumber_should_throw_ArgumentNullException()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        try
        {
            Room r = new Room(-1,Room.RoomType.Double,Room.RoomAvailability.Available);
            
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
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        int number = 12;
        Room r = new Room(number,Room.RoomType.Double, Room.RoomAvailability.Available);

        Assert.That(r.Number, Is.EqualTo(number));
        Room.RemoveRoom(r);
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Availability_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        Room.RoomAvailability ra = Room.RoomAvailability.Available;
        Room r = new Room(1,Room.RoomType.ICU, ra);

        Assert.That(r.Availability, Is.EqualTo(ra));
        Room.RemoveRoom(r);
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Type_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        Room.RoomType t = Room.RoomType.ICU;
        Room r = new Room(123,t, Room.RoomAvailability.Available);

        Assert.That(r.Type, Is.EqualTo(t));
        Room.RemoveRoom(r);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Rooms_and_SetAppointments()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        List<Room> lb = new List<Room>{new ( 2,Room.RoomType.Double,Room.RoomAvailability.Available), 
                        new (3,Room.RoomType.Double,Room.RoomAvailability.Available), 
                        new ( 4,Room.RoomType.Double,Room.RoomAvailability.Available)};

        
        Assert.That(Room.GetRooms(), Is.EqualTo(lb));
    }
    
    [Test]
    public void Trying_to_create_same_Room_throws_InvalidOperationException()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        Room r = new Room(1,Room.RoomType.Double,Room.RoomAvailability.Available);
        try
        {
            Room r2 = new Room(1,Room.RoomType.Double,Room.RoomAvailability.Available);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Room.RemoveRoom(r);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Room_InvalidOperationException_excepted()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

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
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }
        
        List<Room> la = new List<Room>{new ( 2,Room.RoomType.Double,Room.RoomAvailability.Available), 
                                       new (3,Room.RoomType.Double,Room.RoomAvailability.Available), 
                                       new ( 4,Room.RoomType.Double,Room.RoomAvailability.Available)};
        
        SerializeToFIle.saveAll();
        
        foreach (Room o in la)
        {
            Room.RemoveRoom(o);
        }
        
        SerializeToFIle.loadAll();

        Assert.That(Room.GetRooms(), Is.EqualTo(la));
    }
}
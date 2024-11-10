namespace Tests;
using Hospital_System.Models;

public class RoomsTests
{
    [Test]
    public void Trying_to_create_Room_with_negative_roomNumber_should_throw_ArgumentNullException()
    {
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
        int number = 12;
        Room r = new Room(number,Room.RoomType.Double, Room.RoomAvailability.Available);

        Assert.That(r.Number, Is.EqualTo(number));
        Room.RemoveRoom(r);
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Availability_and_check_if_it_assigned_correctly()
    {
        Room.RoomAvailability ra = Room.RoomAvailability.Available;
        Room r = new Room(1,Room.RoomType.ICU, ra);

        Assert.That(r.Availability, Is.EqualTo(ra));
        Room.RemoveRoom(r);
    }
    
    [Test]
    public void Trying_to_create_Room_with_specific_Type_and_check_if_it_assigned_correctly()
    {
        Room.RoomType t = Room.RoomType.ICU;
        Room r = new Room(123,t, Room.RoomAvailability.Available);

        Assert.That(r.Type, Is.EqualTo(t));
        Room.RemoveRoom(r);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Rooms_and_SetAppointments()
    {
        Room.SetRooms(new List<Room>());
        List<Room> lb = new List<Room>{new ( 2,Room.RoomType.Double,Room.RoomAvailability.Available), 
                        new (3,Room.RoomType.Double,Room.RoomAvailability.Available), 
                        new ( 4,Room.RoomType.Double,Room.RoomAvailability.Available)};

        Room.SetRooms(lb);
        
        Assert.That(Room.GetRooms(), Is.EqualTo(lb));
        
        Room.SetRooms(new List<Room>());
    }
    
    [Test]
    public void Trying_to_create_same_Room_throws_InvalidOperationException()
    {
        Room r = new Room(1,Room.RoomType.Double,Room.RoomAvailability.Available);
        try
        {
            Room r2 = new Room(1,Room.RoomType.Double,Room.RoomAvailability.Available);
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
        List<Room> la = new List<Room>{new ( 2,Room.RoomType.Double,Room.RoomAvailability.Available), 
                                       new (3,Room.RoomType.Double,Room.RoomAvailability.Available), 
                                       new ( 4,Room.RoomType.Double,Room.RoomAvailability.Available)};
        
        SerializeToFIle.saveAll();
        
        Room.SetRooms(new List<Room>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Room.GetRooms(), Is.EqualTo(la));
    }
}
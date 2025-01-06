namespace Tests;
using Hospital_System.Models;

public class RoomsTests
{
    
    [Test]
    public void Trying_to_create_Room_with_specific_Availability_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        Room.RoomAvailability ra = Room.RoomAvailability.Available;
        Room r = new Room(Room.RoomType.ICU, ra);

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
        Room r = new Room(t, Room.RoomAvailability.Available);

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

        List<Room> lb = new List<Room>{new ( Room.RoomType.Double,Room.RoomAvailability.Available), 
                        new (Room.RoomType.Double,Room.RoomAvailability.Available), 
                        new ( Room.RoomType.Double,Room.RoomAvailability.Available)};

        
        Assert.That(Room.GetRooms(), Is.EqualTo(lb));
    }
    
    [Test]
    public void Trying_to_create_same_Room_throws_InvalidOperationException()
    {
        foreach (var o in Room.GetRooms().ToList())
        {
            Room.RemoveRoom(o);
        }

        Room r = new Room(Room.RoomType.Double,Room.RoomAvailability.Available);
        try
        {
            Room r2 = new Room(Room.RoomType.Double,Room.RoomAvailability.Available);
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
        
        List<Room> la = new List<Room>{new ( Room.RoomType.Double,Room.RoomAvailability.Available), 
                                       new (Room.RoomType.Double,Room.RoomAvailability.Available), 
                                       new ( Room.RoomType.Double,Room.RoomAvailability.Available)};
        
        SerializeToFIle.saveAll();
        
        foreach (Room o in la)
        {
            Room.RemoveRoom(o);
        }
        
        SerializeToFIle.loadAll();
        foreach (var o in Room.GetRooms())
        {
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_assign_Equipment_to_Department()
    {
        Department department = new Department("Test", new Dictionary<int, Room>());
        Room room = new Room( Room.RoomType.Double,Room.RoomAvailability.Available);
        room.assignRoomToDepartment(department);
        foreach (var e in department.GetDepartmentRooms())
        {
            if (e.Equals(room))
            {
                Department.removeDepartment(department);
                Room.RemoveRoom(room);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_assign_Equipment_to_null_Department_should_throw_ArgumentException()
    {
        Room room = new Room( Room.RoomType.Double,Room.RoomAvailability.Available);
        try
        {
            room.assignRoomToDepartment(null);
            Assert.Fail("expected ArgumentException");
        }
        catch (ArgumentException a)
        {
            Room.RemoveRoom(room);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_assign_Room_to_Department_when_it_already_assigned_to_another_should_throw_InvalidOperationException()
    {
        Department department = new Department("Test", new Dictionary<int, Room>());
        Department department2 = new Department("Test1", new Dictionary<int, Room>());
        Room room = new Room(Room.RoomType.Double,Room.RoomAvailability.Available);
        room.assignRoomToDepartment(department);
        try
        {
            room.assignRoomToDepartment(department2);
            Assert.Fail("expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Department.removeDepartment(department2);
            Department.removeDepartment(department);
            Room.RemoveRoom(room);
            Assert.Pass();
        }
    }
}
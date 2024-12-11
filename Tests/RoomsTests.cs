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
    
    [Test]
    public void Trying_to_assign_Equipment_to_Department()
    {
        Department department = new Department("Test");
        Room room = new Room( 2,Room.RoomType.Double,Room.RoomAvailability.Available);
        room.assignRoomToDepartment(department);
        foreach (var e in department.GetDepartmentRooms())
        {
            if (e.Number == room.Number)
            {
                Department.removeDepartment(department);
                Room.RemoveRoom(room);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    // [Test]
    // public void Trying_to_assign_Room_to_Department_and_change_Department()
    // {
    //     Department department = new Department("Test");
    //     Department department2 = new Department("Test1");
    //     Room room = new Room( 2,Room.RoomType.Double,Room.RoomAvailability.Available);
    //     room.assignRoomToDepartment(department);
    //     if (!room.Department.Equals(department))
    //     {
    //         Assert.Fail();
    //     }
    //     room.changeDepartment(department2);
    //
    //     if (department2.Equals(room.Department))
    //     {
    //         Department.removeDepartment(department2);
    //         Department.removeDepartment(department);
    //         Room.RemoveRoom(room);
    //         Assert.Pass();
    //     }else{
    //         Assert.Fail();
    //     }
    // }
    
    // [Test]
    // public void Trying_to_assign_Room_to_Department_and_change_to_null_Department_should_throw_ArgumentException()
    // {
    //     Department department = new Department("Test");
    //     Room room = new Room( 2,Room.RoomType.Double,Room.RoomAvailability.Available);
    //     room.assignRoomToDepartment(department);
    //     if (!room.Department.Equals(department))
    //     {
    //         Assert.Fail();
    //     }
    //
    //     try
    //     {
    //         room.changeDepartment(null);
    //         Assert.Fail("Expected ArgumentException");
    //     }
    //     catch (ArgumentException ae)
    //     {
    //         Department.removeDepartment(department);
    //         Room.RemoveRoom(room);
    //         Assert.Pass();
    //     }
    // }
    
    // [Test]
    // public void Trying_to_assign_Room_to_Department_and_change_to_same_Department_should_throw_InvalidOperationException()
    // {
    //     Department department = new Department("Test");
    //     Room room = new Room( 2,Room.RoomType.Double,Room.RoomAvailability.Available);
    //     room.assignRoomToDepartment(department);
    //     if (!room.Department.Equals(department))
    //     {
    //         Assert.Fail();
    //     }
    //
    //     try
    //     {
    //         room.changeDepartment(department);
    //         Assert.Fail("Expected InvalidOperationException");
    //     }
    //     catch (InvalidOperationException ae)
    //     {
    //         Department.removeDepartment(department);
    //         Room.RemoveRoom(room);
    //         Assert.Pass();
    //     }
    // }
    
    [Test]
    public void Trying_to_assign_Equipment_to_null_Department_should_throw_ArgumentException()
    {
        Room room = new Room( 2,Room.RoomType.Double,Room.RoomAvailability.Available);
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
        Department department = new Department("Test");
        Department department2 = new Department("Test1");
        Room room = new Room( 2,Room.RoomType.Double,Room.RoomAvailability.Available);
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
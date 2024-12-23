namespace Tests;
using Hospital_System.Models;

public class DepartmentTests
{
    [Test]
    public void Trying_to_set_empty_name_should_catch_Exception()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        try
        {
            Department d = new Department(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Department_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        String name = "Test1";
        Department d = new Department(name);
        Assert.That(d.Name, Is.EqualTo(name));
        Department.removeDepartment(d);
    }
    
    
    [Test]
    public void Trying_to_create_Department()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        Department d = new Department("Test1");
        Assert.Pass();
        Department.removeDepartment(d);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Departments_and_SetAppointments()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        
        List<Department> ld = new List<Department>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        Assert.That(Department.GetDepartments(), Is.EqualTo(ld));
    }
    
    [Test]
    public void Trying_to_create_same_Department_throws_InvalidOperationException()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        Department b = new Department("Test1");
        try
        {
            Department b2 = new Department("Test1");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Department.removeDepartment(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_add_Equipment_to_Department_and_then_delete_it()
    {
        Department department = new Department("Test1");
        Equipment equipment = new Equipment(1,"Test2");
        department.addEquipmentToDepartment(equipment);
        foreach (var e in department.GetEquipments())
        {
            if (e.Id == equipment.Id)
            {
                department.removeEquipmentFromDepartment(equipment);
                foreach (var e2 in department.GetEquipments())
                {
                    if (e2.Id == equipment.Id)
                    {
                        Assert.Fail("Equipment has not been deleted");
                    }
                }
                Department.removeDepartment(department);
                Equipment.removeEquipment(equipment);
                Assert.Pass();
            }
        }
        Assert.Fail("Equipment has not been added");
    }
    
    [Test]
    public void Trying_to_add_Equipment_to_Department()
    {
        Department department = new Department("Test");
        Equipment equipment = new Equipment(1,"Test1");
        department.addEquipmentToDepartment(equipment);
        foreach (var e in department.GetEquipments())
        {
            if (e.Id == equipment.Id)
            {
                Equipment.removeEquipment(equipment);
                Department.removeDepartment(department);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Equipments_to_Department()
    {
        Department department = new Department("Test");
        List<Equipment> equipments = new List<Equipment>{new ( 24,"Test1"), new ( 21,"Test2"), new ( 44,"Test3")};
        foreach (var e in equipments)
        {
            department.addEquipmentToDepartment(e);
        }

        foreach (var e in department.GetEquipments())
        {
            if (equipments.Exists(e => equipments.Count==department.GetEquipments().Count))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Department.removeDepartment(department);
        foreach (var e in equipments)
        {
            Equipment.removeEquipment(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Equipment_to_Department_throws_ArgumentNullException()
    {
        Department department = new Department("Test");
        try
        {
            department.addEquipmentToDepartment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Department.removeDepartment(department);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Room_to_Department_and_then_delete_it()
    {
        Department department = new Department("Test1");
        Room room = new Room(1,Room.RoomType.Double, Room.RoomAvailability.Available);
        department.addRoomToDepartment(room);
        foreach (var e in  department.GetDepartmentRooms())
        {
            if (e.Number == room.Number)
            {
                department.removeRoomFromDepartment(room);
                foreach (var e2 in  department.GetDepartmentRooms())
                {
                    if (e2.Number == room.Number)
                    {
                        Assert.Fail("Room has not been deleted");
                    }
                }
                Department.removeDepartment(department);
                Room.RemoveRoom(room);
                Assert.Pass();
            }
        }
        Assert.Fail("Room has not been added");
    }
    
    [Test]
    public void Trying_to_add_Room_to_Department()
    {
        Department department = new Department("Test");
        Room room = new Room(1,Room.RoomType.Double, Room.RoomAvailability.Available);
        department.addRoomToDepartment(room);
        foreach (var e in department.GetDepartmentRooms())
        {
            if (e.Number == room.Number)
            {
                Room.RemoveRoom(room);
                Department.removeDepartment(department);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Room_to_Department()
    {
        Department department = new Department("Test");
        List<Room> rooms = new List<Room>{new (1,Room.RoomType.Double, Room.RoomAvailability.Available), new (2,Room.RoomType.Double, Room.RoomAvailability.Available), new (3,Room.RoomType.Double, Room.RoomAvailability.Available)};
        foreach (var e in rooms)
        {
            department.addRoomToDepartment(e);
        }

        foreach (var e in department.GetEquipments())
        {
            if (rooms.Exists(e => rooms.Count==department.GetEquipments().Count))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Department.removeDepartment(department);
        foreach (var r in rooms)
        {
            Room.RemoveRoom(r);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Room_to_Department_throws_ArgumentNullException()
    {
        Department department = new Department("Test");
        try
        {
            department.addRoomToDepartment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Department.removeDepartment(department);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_same_Room_to_Department_should_throw_InvalidOperationException()
    {
        Department department = new Department("Test");
        Room room = new Room(1,Room.RoomType.Double, Room.RoomAvailability.Available);
        department.addRoomToDepartment(room);
        try
        {
            department.addRoomToDepartment(room);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException oe)
        {
            Department.removeDepartment(department);
            Room.RemoveRoom(room);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Department_InvalidOperationException_excepted()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        try
        {
            Department.removeDepartment(new Department());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Departments_and_save_them_to_file()
    {
        foreach (var o in Department.GetDepartments().ToList())
        {
            Department.removeDepartment(o);
        }
        List<Department> ld = new List<Department>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        SerializeToFIle.saveAll();
        
        foreach (Department o in ld)
        {
            Department.removeDepartment(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Department.GetDepartments(), Is.EqualTo(ld));
    }
    
    [Test]
    public void Trying_to_add_Nurse_to_Department_and_then_delete_it()
    {
        Department department = new Department("Test");
        Nurse nurse = new Nurse(1,"Test2");
        department.addNurseToDepartment(nurse);
        foreach (var e in department.GetNurses())
        {
            if (e.Equals(nurse))
            {
                department.removeNurseFromDepartment(nurse);
                foreach (var e2 in department.GetNurses())
                {
                    if (e2.Equals(nurse))
                    {
                        Assert.Fail();
                    }
                }
                Department.removeDepartment(department);
                Nurse.removeNurse(nurse);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Nurse_to_Department()
    {
        Department department = new Department("Test");
        Nurse nurse = new Nurse(1,"Test2");
        department.addNurseToDepartment(nurse);
        foreach (var e in department.GetNurses())
        {
            if (e.Equals(nurse))
            {
                Department.removeDepartment(department);
                Nurse.removeNurse(nurse);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Nurses_to_Department()
    {
        Department department = new Department("Test");
        List<Nurse> nurses = new List<Nurse>{new (1,"Test2"),
            new (2,"Test2"),
            new (3,"Test2")};
        foreach (var e in nurses)
        {
            department.addNurseToDepartment(e);
        }
    
        foreach (var e in department.GetNurses())
        {
            if (nurses.Contains(e))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Department.removeDepartment(department);
        foreach (var e in nurses)
        {
            Nurse.removeNurse(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Nurse_to_Department_throws_ArgumentNullException()
    {
        Department department = new Department("Test");
        try
        {
            department.addNurseToDepartment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Department.removeDepartment(department);
            Assert.Pass();   
        }
    }
    
    // [Test]
    // public void Trying_to_add_Department_to_Nurse_that_already_has_Department_throws_InvalidOperationException()
    // {
    //     Department department = new Department("Test");
    //     Department department2 = new Department("Test2");
    //     Nurse nurse = new Nurse(1,"Test2");
    //     department.addNurseToDepartment(nurse);
    //     try
    //     {
    //         department2.addNurseToDepartment(nurse);
    //         Assert.Fail("Expected InvalidOperationException");
    //     }
    //     catch (InvalidOperationException argumentException)
    //     {
    //         Department.removeDepartment(department);
    //         Department.removeDepartment(department2);
    //         Nurse.removeNurse(nurse);
    //         Assert.Pass();   
    //     }
    // }
    
    [Test]
    public void Trying_to_add_Nurse_to_Department_and_then_change_it()
    {
        
        Department department = new Department("Test");
        Department department2 = new Department("Test2");
        Nurse nurse = new Nurse(1,"Test2");
        department.addNurseToDepartment(nurse);
        if (department.GetNurses().Contains(nurse))
        {
            nurse.changeDepartment(department2);
            if (department2.GetNurses().Contains(nurse))
            {
                Department.removeDepartment(department);
                Department.removeDepartment(department2);
                Nurse.removeNurse(nurse);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Nurse_to_Department_and_then_change_it_to_the_same_physician_throws_InvalidOperationException()
    {
        Department department = new Department("Test");
        Nurse nurse = new Nurse(1,"Test2");
        department.addNurseToDepartment(nurse);
        if (department.GetNurses().Contains(nurse))
        {
            try
            {
                nurse.changeDepartment(department);
                Assert.Fail("Expected InvalidOperationException");
            }
            catch (InvalidOperationException)
            {
                Department.removeDepartment(department);
                Nurse.removeNurse(nurse);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    // [Test]
    // public void Trying_to_change_Department_Nurse_without_assigning_Department_throws_InvalidOperationException()
    // {
    //     Department department = new Department("Test");
    //     Nurse nurse = new Nurse(1,"Test2");
    //     try
    //     {
    //         nurse.changeDepartment(department);
    //         Assert.Fail("Expected InvalidOperationException");
    //     }
    //     catch (InvalidOperationException)
    //     {
    //         Department.removeDepartment(department);
    //         Nurse.removeNurse(nurse);
    //         Assert.Pass();
    //     }
    //     Assert.Fail();
    // }
    
    [Test]
    public void Trying_to_add_Department_to_Nurse_and_then_try_to_add_same_Department_throws_InvalidOperationException()
    {
        Department department = new Department("Test");
        Nurse nurse = new Nurse(1,"Test2");
        department.addNurseToDepartment(nurse);
        try
        {
            nurse.asssignNurseToDepartment(department);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Department.removeDepartment(department);
            Nurse.removeNurse(nurse);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Department_to_Nurse_and_then_remove_it()
    {
        Department department = new Department("Test");
        Nurse nurse = new Nurse(1,"Test2");
        department.addNurseToDepartment(nurse);
        foreach (var e in department.GetNurses())
        {
            if (e.Equals(nurse) && nurse.Department.Equals(department))
            {
                department.removeNurseFromDepartment(nurse);
                if (department.GetNurses().Contains(nurse) || nurse.Department != null)
                {
                    Assert.Fail();
                }
                Department.removeDepartment(department);
                Nurse.removeNurse(nurse);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Nurse_that_not_exist_in_list_from_Department_should_throw_InvalidOperationException()
    {
        Department department = new Department("Test");
        Nurse nurse = new Nurse(1,"Test2");
        try
        {
            department.removeNurseFromDepartment(nurse);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Department.removeDepartment(department);
            Nurse.removeNurse(nurse);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    
    [Test]
    public void Trying_to_add_Head_Doctor_to_Department_and_then_delete_it()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        doctor.becomeHeadOfDepartment(department);
        if (department.HeadOfDepaartment.Equals(doctor))
        {
            doctor.deleteDoctorFromBeingHead(department);
            if (department.HeadOfDepaartment != null)
            {
                Assert.Fail();
            }
            Department.removeDepartment(department);
            Physician.RemovePhysician(doctor);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Head_Doctor_to_Department()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        doctor.becomeHeadOfDepartment(department);
        if (department.HeadOfDepaartment.Equals(doctor))
        {
            Department.removeDepartment(department);
            Physician.RemovePhysician(doctor);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Head_Doctor_to_null_Department_throws_ArgumentNullException()
    {
        Physician doctor = new Physician(1,"Test1","Test1");
        try
        {
            doctor.becomeHeadOfDepartment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Physician.RemovePhysician(doctor);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Department_to_Head_Doctor_that_already_has_Department_throws_InvalidOperationException()
    {
        Department department = new Department("Test");
        Department department2 = new Department("Test2");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        doctor.becomeHeadOfDepartment(department);
        try
        {
            doctor.becomeHeadOfDepartment(department2);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException argumentException)
        {
            Department.removeDepartment(department);
            Department.removeDepartment(department2);
            Physician.RemovePhysician(doctor);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_remove_Head_Doctor_that_not_exist_from_Department_should_throw_InvalidOperationException()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        try
        {
            doctor.deleteDoctorFromBeingHead(department);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Department.removeDepartment(department);
            Physician.RemovePhysician(doctor);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
        [Test]
    public void Trying_to_add_Doctor_to_Department_and_then_delete_it()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        foreach (var e in department.GetDoctors())
        {
            if (e.Equals(doctor))
            {
                department.removeDoctorFromDepartment(doctor);
                foreach (var e2 in department.GetDoctors())
                {
                    if (e2.Equals(doctor))
                    {
                        Assert.Fail();
                    }
                }
                Department.removeDepartment(department);
                Physician.RemovePhysician(doctor);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Doctor_to_Department()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        foreach (var e in department.GetDoctors())
        {
            if (e.Equals(doctor))
            {
                Department.removeDepartment(department);
                Physician.RemovePhysician(doctor);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Doctors_to_Department()
    {
        Department department = new Department("Test");
        List<Physician> doctors = new List<Physician>{new (4,"Test1","Test1"),
            new (5,"Test1","Test2"),
            new (6,"Test1","Test3")};
        foreach (var e in doctors)
        {
            department.addDoctorToDepartment(e);
        }
    
        foreach (var e in department.GetDoctors())
        {
            if (doctors.Contains(e))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Department.removeDepartment(department);
        foreach (var e in doctors)
        {
            Physician.RemovePhysician(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Doctor_to_Department_throws_ArgumentNullException()
    {
        Department department = new Department("Test");
        try
        {
            department.addDoctorToDepartment(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Department.removeDepartment(department);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Doctor_to_Department_that_already_has_Doctor_throws_InvalidOperationException()
    {
        Department department = new Department("Test");
        Department department2 = new Department("Test2");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        try
        {
            department2.addDoctorToDepartment(doctor);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException argumentException)
        {
            Department.removeDepartment(department);
            Department.removeDepartment(department2);
            Physician.RemovePhysician(doctor);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Doctor_to_Department_and_then_change_it()
    {
        
        Department department = new Department("Test");
        Department department2 = new Department("Test2");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        if (department.GetDoctors().Contains(doctor))
        {
            doctor.changeDepartment(department2);
            if (department2.GetDoctors().Contains(doctor))
            {
                Department.removeDepartment(department);
                Department.removeDepartment(department2);
                Physician.RemovePhysician(doctor);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Doctor_to_Department_and_then_change_it_to_the_same_Department_throws_InvalidOperationException()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        if (department.GetDoctors().Contains(doctor))
        {
            try
            {
                doctor.changeDepartment(department);
                Assert.Fail("Expected InvalidOperationException");
            }
            catch (InvalidOperationException)
            {
                Department.removeDepartment(department);
                Physician.RemovePhysician(doctor);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    // [Test]
    // public void Trying_to_change_Doctors_Department_without_assigning_Department_throws_InvalidOperationException()
    // {
    //     Department department = new Department("Test");
    //     Physician doctor = new Physician(1,"Test1","Test1");
    //     try
    //     {
    //         doctor.changeDepartment(department);
    //         Assert.Fail("Expected InvalidOperationException");
    //     }
    //     catch (InvalidOperationException)
    //     {
    //         Department.removeDepartment(department);
    //         Physician.RemovePhysician(doctor);
    //         Assert.Pass();
    //     }
    //     Assert.Fail();
    // }
    
    [Test]
    public void Trying_to_add_Department_to_Doctor_and_then_try_to_add_same_Department_throws_InvalidOperationException()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        try
        {
            doctor.asssignDoctorToDepartment(department);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Department.removeDepartment(department);
            Physician.RemovePhysician(doctor);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Department_to_Doctor_and_then_remove_it()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        department.addDoctorToDepartment(doctor);
        foreach (var e in department.GetDoctors())
        {
            if (e.Equals(doctor) && doctor.Department.Equals(department))
            {
                department.removeDoctorFromDepartment(doctor);
                if (department.GetDoctors().Contains(doctor) || doctor.Department != null)
                {
                    Assert.Fail();
                }
                Department.removeDepartment(department);
                Physician.RemovePhysician(doctor);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Doctor_that_not_exist_in_list_from_Department_should_throw_InvalidOperationException()
    {
        Department department = new Department("Test");
        Physician doctor = new Physician(1,"Test1","Test1");
        try
        {
            department.removeDoctorFromDepartment(doctor);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Department.removeDepartment(department);
            Physician.RemovePhysician(doctor);
            Assert.Pass();
        }
        Assert.Fail();
    }
}
namespace Tests;
using Hospital_System.Models;

public class StaffTests
{
    [Test]
    public void Trying_to_create_Staff_with_null_name_should_throw_ArgumentNullException()
    {
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        try
        {
            Staff s = new Staff(1, null, "test1");
            
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
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        try
        {
            Staff s = new Staff(1, "test1", null);
            
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
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        String name = "Joe";
        Staff s = new Staff(1, name, "Test2");


        Assert.That(s.Name, Is.EqualTo(name));
        Staff.RemoveStaff(s);
    }
    
    [Test]
    public void Trying_to_create_Staff_with_specific_Name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        String pos = "type1231";
        Staff s = new Staff(1, "test2", pos);


        Assert.That(s.Position, Is.EqualTo(pos));
        Staff.RemoveStaff(s);
    }
    
    
    [Test]
    public void Trying_to_create_List_of_Staffs_and_SetAppointments()
    {
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        List<Staff> lb = new List<Staff>{new (5,"Test2","test"), new (3,"Test3","test"), new (4,"Test4","test")};
        
        Assert.That(Staff.GetStaffMembers(), Is.EqualTo(lb));
    }
    
    [Test]
    public void Trying_to_create_same_Staff_throws_InvalidOperationException()
    {
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        Staff b = new Staff(1,"Test","test");
        try
        {
            Staff b2 = new Staff(1,"Test","test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Staff.RemoveStaff(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Staff_InvalidOperationException_excepted()
    {
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        try
        {
            Staff.RemoveStaff(new Staff());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Staff_and_save_them_to_file()
    {
        foreach (var o in Staff.GetStaffMembers().ToList())
        {
            Staff.RemoveStaff(o);
        }

        List<Staff> la = new List<Staff>{new (5,"Test2","test"), new (3,"Test3","test"), new (4,"Test4","test")};
        
        SerializeToFIle.saveAll();
        
        foreach (Staff o in la.ToList())
        {
            Staff.RemoveStaff(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Staff.GetStaffMembers(), Is.EqualTo(la));
    }
    
    [Test]
    public void Trying_to_add_Shift_to_Staff_and_then_delete_it()
    {
        Staff staff = new Staff(3,"Test2","test");
        Shift shift = new Shift(new DateTime(2019, 01, 01), new DateTime(2020, 01, 01), "Test");
        staff.addShiftToStaff(shift);
        foreach (var e in staff.GetShifts())
        {
            if (e.Equals(shift))
            {
                staff.removeShiftFromStaff(shift);
                foreach (var e2 in staff.GetShifts())
                {
                    if (e2.Equals(shift))
                    {
                        Assert.Fail("Bill has not been deleted");
                    }
                }
                Staff.RemoveStaff(staff);
                Shift.RemoveShift(shift);
                Assert.Pass();
            }
        }
        Assert.Fail("Bill has not been added");
    }
    
    [Test]
    public void Trying_to_add_Shift_to_Staff()
    {
        Staff staff = new Staff(3,"Test2","test");
        Shift shift = new Shift(new DateTime(2019, 01, 01), new DateTime(2020, 01, 01), "Test");
        staff.addShiftToStaff(shift);
        foreach (var e in staff.GetShifts())
        {
            if (e.Equals(shift))
            {
                Shift.RemoveShift(shift);
                Staff.RemoveStaff(staff);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Shift_to_Staff()
    {
        Staff staff = new Staff(2,"Test2","test");
        List<Shift> shift = new List<Shift>{new (new DateTime(2019, 01, 01), new DateTime(2020, 01, 01), "Test"),
                                            new (new DateTime(2019, 01, 01), new DateTime(2021, 01, 01), "Test2"),
                                            new (new DateTime(2019, 01, 01), new DateTime(2024, 01, 01), "Test3")};
        foreach (var e in shift)
        {
            staff.addShiftToStaff(e);
        }
    
        foreach (var e in staff.GetShifts())
        {
            if (shift.Exists(e => shift.Count==staff.GetShifts().Count))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Staff.RemoveStaff(staff);
        foreach (var e in shift)
        {
            Shift.RemoveShift(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Shift_to_Staff_throws_ArgumentNullException()
    {
        Staff staff = new Staff(3,"Test2","test");
        try
        {
            staff.addShiftToStaff(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Staff.RemoveStaff(staff);
            Assert.Pass();   
        }
    }
}
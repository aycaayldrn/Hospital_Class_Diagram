namespace Tests;
using Hospital_System.Models;

public class StaffTests
{
    [Test]
    public void Trying_to_create_Staff_with_null_name_should_throw_ArgumentNullException()
    {
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
        String name = "Joe";
        Staff s = new Staff(1, name, "Test2");


        Assert.That(s.Name, Is.EqualTo(name));
        Staff.RemoveStaff(s);
    }
    
    [Test]
    public void Trying_to_create_Staff_with_specific_Name_and_check_if_it_assigned_correctly()
    {
        String pos = "type1231";
        Staff s = new Staff(1, "test2", pos);


        Assert.That(s.Position, Is.EqualTo(pos));
        Staff.RemoveStaff(s);
    }
    
    
    [Test]
    public void Trying_to_create_List_of_Staffs_and_SetAppointments()
    {
        List<Staff> lb = new List<Staff>{new (2,"Test2","test"), new (3,"Test3","test"), new (4,"Test4","test")};
        
        Staff.SetStaffMembers(lb);
        
        Assert.That(Staff.GetStaffMembers(), Is.EqualTo(lb));
        
        Staff.SetStaffMembers(new List<Staff>());
    }
    
    [Test]
    public void Trying_to_create_same_Staff_throws_InvalidOperationException()
    {
        Staff b = new Staff(1,"Test","test");
        try
        {
            Staff b2 = new Staff(1,"Test","test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Staff_InvalidOperationException_excepted()
    {
        try
        {
            Staff.RemoveStaff(new Staff());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
}
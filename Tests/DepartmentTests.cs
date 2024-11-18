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
        Department d = new Department("Test");
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
        Department b = new Department("Test");
        try
        {
            Department b2 = new Department("Test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Department.removeDepartment(b);
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
}
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
            if (equipments.Exists(e => e.Id == e.Id && equipments.Count==department.GetEquipments().Count))
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
    public void Trying_to_add_same_Equipments_to_Department_should_throw_InvalidOperationException()
    {
        Department department = new Department("Test");
        Equipment equipment = new Equipment(1,"Test");
        department.addEquipmentToDepartment(equipment);
        try
        {
            department.addEquipmentToDepartment(equipment);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException oe)
        {
            Equipment.removeEquipment(equipment);
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
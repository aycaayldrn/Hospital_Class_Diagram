namespace Tests;
using Hospital_System.Models;

[TestFixture]
public class EquipmentTest
{
    [Test]
    public void Trying_to_create_Equipment()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        Equipment e = new Equipment(1,"Test");

        Assert.Pass();
        Equipment.removeEquipment(e);
    }
    
    [Test]
    public void Trying_to_set_MaintenanceHistory_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        Equipment e = new Equipment(1,"Test");

        try
        {
            e.MaintenanceHistory = null;
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException a)
        {
            Assert.Pass();
        }

        Assert.Pass();
        Equipment.removeEquipment(e);
    }
    
    [Test]
    public void Trying_to_assign_null_val_to_type_should_throw_ArgumentException()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        try
        {
            Equipment e = new Equipment(1,null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_assign_negative_val_to_id_should_throw_ArgumentException()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        try
        {
            Equipment e = new Equipment(-1,"Test1");
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Equipment_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        String Tname = "Test2";
        Equipment e = new Equipment(1,Tname);
        Assert.That(e.Type, Is.EqualTo(Tname));
        Equipment.removeEquipment(e);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Equipments_and_SetAppointments()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }
        
        List<Equipment> le = new List<Equipment>{new ( 24,"Test1"), new ( 21,"Test2"), new ( 44,"Test3")};
        
        Assert.That(Equipment.GetEquipments(), Is.EqualTo(le));
    }
    
    [Test]
    public void Trying_to_create_same_Equipment_throws_InvalidOperationException()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        Equipment e = new Equipment(21,"Test1");
        try
        {
            Equipment e2 = new Equipment(21,"Test1");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Equipment.removeEquipment(e);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Equipment_InvalidOperationException_excepted()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }

        try
        {
            Equipment.removeEquipment(new Equipment());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_List_of_Equipments_and_save_them_to_file()
    {
        foreach (var o in Equipment.GetEquipments().ToList())
        {
            Equipment.removeEquipment(o);
        }
        
        List<Equipment> la = new List<Equipment>{new ( 24,"Test1"), new ( 21,"Test2"), new ( 44,"Test3")};
        
        SerializeToFIle.saveAll();
        
        foreach (Equipment o in la)
        {
            Equipment.removeEquipment(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Equipment.GetEquipments(), Is.EqualTo(la));
    }
}
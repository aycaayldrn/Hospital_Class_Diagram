namespace Tests;
using Hospital_System.Models;

[TestFixture]
public class EquipmentTest
{
    [Test]
    public void Trying_to_create_Equipment()
    {
        Equipment e = new Equipment(1,"Test");
        Assert.Pass();
        Equipment.removeEquipment(e);
    }
    
    [Test]
    public void Trying_to_assign_null_val_to_type_should_throw_ArgumentNullException()
    {
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
    public void Trying_to_assign_negative_val_to_id_should_throw_ArgumentNullException()
    {
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
        String Tname = "Test2";
        Equipment e = new Equipment(1,Tname);
        Assert.That(e.Type, Is.EqualTo(Tname));
        Equipment.removeEquipment(e);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Equipments_and_SetAppointments()
    {
        List<Equipment> le = new List<Equipment>{new ( 24,"Test1"), new ( 21,"Test2"), new ( 44,"Test3")};
        
        Equipment.SetEquipments(le);
        
        Assert.That(Equipment.GetEquipments(), Is.EqualTo(le));
        
        Equipment.SetEquipments(new List<Equipment>());
    }
    
    [Test]
    public void Trying_to_create_same_Equipment_throws_InvalidOperationException()
    {
        Equipment e = new Equipment(21,"Test1");
        try
        {
            Equipment e2 = new Equipment(21,"Test1");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Equipment_InvalidOperationException_excepted()
    {
        try
        {
            Equipment.removeEquipment(new Equipment());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
}
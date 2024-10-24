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
    }
    
    [Test]
    public void Trying_to_assign_null_val_to_type_should_throw_ArgumentNullException()
    {
        try
        {
            Equipment e = new Equipment(1,null);
            Assert.Fail("Expected ArgumentNullException");
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
            Equipment e = new Equipment(-1,"Test");
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Equipment_with_specific_name_and_check_if_it_assigned_correctly()
    {
        String Tname = "Test";
        Equipment e = new Equipment(1,Tname);
        Assert.That(e.Type, Is.EqualTo(Tname));
    }
}
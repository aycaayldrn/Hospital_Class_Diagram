namespace Tests;
using Hospital_System.Models;

public class PhysicianTests
{
    [Test]
    public void Trying_to_assign_null_val_to_specialization_should_throw_ArgumentNullException()
    {
        try
        {
            Physician p = new Physician(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Physician_with_specific_specialization_and_check_if_it_assigned_correctly()
    {
        String name = "Test2";
        Physician p = new Physician(name);
        Assert.That(p.Specialization, Is.EqualTo(name));
        Physician.RemovePhysician(p);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Physicians_and_SetAppointments()
    {
        Physician.SetPhysicians(new List<Physician>());
        List<Physician> lp = new List<Physician>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        Physician.SetPhysicians(lp);
        
        Assert.That(Physician.GetPhysicians(), Is.EqualTo(lp));
        
        Physician.SetPhysicians(new List<Physician>());
    }
    
    
    [Test]
    public void Trying_to_create_same_Physician_throws_InvalidOperationException()
    {
        Physician b = new Physician("Test");
        try
        {
            Physician b2 = new Physician("Test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Physician_InvalidOperationException_excepted()
    {
        try
        {
            Physician.RemovePhysician(new Physician());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Physician_and_save_them_to_file()
    {
        Physician.SetPhysicians(new List<Physician>());
        List<Physician> la = new List<Physician>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        SerializeToFIle.saveAll();
        
        Physician.SetPhysicians(new List<Physician>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Physician.GetPhysicians(), Is.EqualTo(la));
    }
}
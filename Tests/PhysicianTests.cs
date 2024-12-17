namespace Tests;
using Hospital_System.Models;

public class PhysicianTests
{
    [Test]
    public void Trying_to_assign_null_val_to_specialization_should_throw_ArgumentNullException()
    {
        foreach (var o in Physician.GetPhysicians().ToList())
        {
            Physician.RemovePhysician(o);
        }

        try
        {
            Physician p = new Physician(0,null,null);
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
        foreach (var o in Physician.GetPhysicians().ToList())
        {
            Physician.RemovePhysician(o);
        }
        
        String name = "Test2";
        Physician p = new Physician(1,name,name);
        Assert.That(p.Specialization, Is.EqualTo(name));
        Physician.RemovePhysician(p);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Physicians_and_SetAppointments()
    {
        foreach (var o in Physician.GetPhysicians().ToList())
        {
            Physician.RemovePhysician(o);
        }
        
        List<Physician> lp = new List<Physician>{new ( 1,"Test1","Test1"), new ( 2,"Test2","Test2"), new ( 3,"Test3","Test3")};
        
        Assert.That(Physician.GetPhysicians(), Is.EqualTo(lp));
    }
    
    
    [Test]
    public void Trying_to_create_same_Physician_throws_InvalidOperationException()
    {
        foreach (var o in Physician.GetPhysicians().ToList())
        {
            Physician.RemovePhysician(o);
        }

        Physician b = new Physician(1,"Test","Test");
        try
        {
            Physician b2 = new Physician(1,"Test","Test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Physician.RemovePhysician(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Physician_InvalidOperationException_excepted()
    {
        foreach (var o in Physician.GetPhysicians().ToList())
        {
            Physician.RemovePhysician(o);
        }

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
        foreach (var o in Physician.GetPhysicians().ToList())
        {
            Physician.RemovePhysician(o);
        }
        
        List<Physician> la = new List<Physician>{new ( 1,"Test1","Test1"), new ( 2,"Test2","Test2"), new ( 3,"Test3","Test3")};
        
        SerializeToFIle.saveAll();
        
        foreach (Physician o in la)
        {
            Physician.RemovePhysician(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Physician.GetPhysicians(), Is.EqualTo(la));
    }
}
namespace Tests;
using Hospital_System.Models;


public class NurseTests
{
    [Test]
    public void Trying_to_assign_null_val_to_name_should_throw_ArgumentNullException()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        try
        {
            Nurse n = new Nurse(1,null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_use_DisplayNurseInfo_function()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        Nurse n = new Nurse(1,"Test2");
        
        n.DisplayNurseInfo();
        Assert.Pass();
        Nurse.removeNurse(n);
    }
    
    [Test]
    public void Trying_to_set_Certifications_to_null_should_throw_ArgumentException()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        Nurse n = new Nurse(1,"Test2");

        try
        {
            n.Certifications = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException a)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Nurse_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        String name = "Test1";
        Nurse n = new Nurse(2,name);
        Assert.That(n.Name, Is.EqualTo(name));
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Nurses_and_SetAppointments()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }
        
        List<Nurse> ln = new List<Nurse>{new ( 24,"Test1"), new ( 21,"Test2"), new ( 44,"Test3")};
        
        Assert.That(Nurse.GetNurses(), Is.EqualTo(ln));
    }
    
    [Test]
    public void Trying_to_create_same_Nurse_throws_InvalidOperationException()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        Nurse b = new Nurse(1,"Test5");
        try
        {
            Nurse b2 = new Nurse(1,"Test5");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Nurse.removeNurse(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Nurse_InvalidOperationException_excepted()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        try
        {
            Nurse.removeNurse(new Nurse());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Nurses_and_save_them_to_file()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }
        
        List<Nurse> la = new List<Nurse>{new ( 24,"Test1"), new ( 21,"Test2"), new ( 44,"Test3")};
        
        SerializeToFIle.saveAll();
        
        foreach (Nurse o in la)
        {
            Nurse.removeNurse(o);
        }
        
        SerializeToFIle.loadAll();
        
        Assert.That(Nurse.GetNurses(), Is.EqualTo(la));
    }
}
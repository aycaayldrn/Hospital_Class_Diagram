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
            Nurse n = new Nurse(1,null, new List<Shift>(){new Shift()});
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

        Nurse n = new Nurse(1,"Test2", new List<Shift>(){new Shift()});
        
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

        Nurse n = new Nurse(1,"Test2", new List<Shift>(){new Shift()});

        try
        {
            n.Certifications = null;
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException a)
        {
            Nurse.removeNurse(n);
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
        Nurse n = new Nurse(2,name, new List<Shift>(){new Shift()});
        Assert.That(n.Name, Is.EqualTo(name));
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Nurses_and_SetAppointments()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }
        
        List<Nurse> ln = new List<Nurse>{new ( 24,"Test1", new List<Shift>(){new Shift()}), new ( 21,"Test2", new List<Shift>(){new Shift()}), new ( 44,"Test3", new List<Shift>(){new Shift()})};
        
        Assert.That(Nurse.GetNurses(), Is.EqualTo(ln));
    }
    
    [Test]
    public void Trying_to_create_same_Nurse_throws_InvalidOperationException()
    {
        foreach (var o in Nurse.GetNurses().ToList())
        {
            Nurse.removeNurse(o);
        }

        Nurse b = new Nurse(1,"Test5", new List<Shift>(){new Shift()});
        try
        {
            Nurse b2 = new Nurse(1,"Test5", new List<Shift>(){new Shift()});
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
        
        List<Nurse> la = new List<Nurse>{new ( 24,"Test1", new List<Shift>(){new Shift()}), new ( 21,"Test2", new List<Shift>(){new Shift()}), new ( 44,"Test3", new List<Shift>(){new Shift()})};
        
        SerializeToFIle.saveAll();
        
        foreach (Nurse o in la)
        {
            Nurse.removeNurse(o);
        }
        
        SerializeToFIle.loadAll();
        foreach (var o in Nurse.GetNurses())
        {
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
}
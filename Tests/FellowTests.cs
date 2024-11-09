namespace Tests;
using Hospital_System.Models;


public class FellowTests
{
    [Test]
    public void Trying_to_assign_null_to_specialization_should_throw_ArgumentNullException()
    {
        try
        {
            Fellow f = new Fellow(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void Trying_to_create_Fellow_with_specific_name_of_specialization_and_check_if_it_assigned_correctly()
    {
        String name = "Test2";
        Fellow f = new Fellow(name);
        
        Assert.That(f.Specialization, Is.EqualTo(name));
        
        Fellow.removeFellow(f);
    }
    
    [Test]
    public void Trying_to_create_Fellow_with_researchProject_name_of_specialization_and_check_if_it_assigned_correctly()
    {
        String name = "Test";
        Fellow f = new Fellow("name",name);
        
        Assert.That(f.ResearchProject, Is.EqualTo(name));
        Fellow.removeFellow(f);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Fellows_and_SetAppointments()
    {
        Fellow.SetFellows(new List<Fellow>());

        List<Fellow> lf = new List<Fellow>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        Fellow.SetFellows(lf);
        
        Assert.That(Fellow.GetFellows(), Is.EqualTo(lf));
        
        Fellow.SetFellows(new List<Fellow>());
    }
    
    [Test]
    public void Trying_to_create_same_Fellow_throws_InvalidOperationException()
    {
        Fellow b = new Fellow("Test1");
        try
        {
            Fellow b2 = new Fellow("Test1");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Fellow_InvalidOperationException_excepted()
    {
        try
        {
            Fellow.removeFellow(new Fellow());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Fellows_and_save_them_to_file()
    {
        Fellow.SetFellows(new List<Fellow>());
        List<Fellow> la = new List<Fellow>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        SerializeToFIle.saveAll();
        
        Fellow.SetFellows(new List<Fellow>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Fellow.GetFellows(), Is.EqualTo(la));
    }
}
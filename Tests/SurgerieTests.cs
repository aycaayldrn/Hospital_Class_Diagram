namespace Tests;
using Hospital_System.Models;

public class SurgerieTests
{
    [Test]
    public void Trying_to_create_Surgerie_with_null_name_should_throw_ArgumentNullException()
    {
        try
        {
            Surgerie s = new Surgerie(null);
            
            Assert.Fail("Expected ArgumentNullException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Surgerie_with_specific_Type_and_check_if_it_assigned_correctly()
    {
        String type = "bents";
        Surgerie s = new Surgerie(type);


        Assert.That(s.Type, Is.EqualTo(type));
    }
    
    
    [Test]
    public void Trying_to_create_List_of_Surgeries_and_SetAppointments()
    {
        Surgerie.SetSurgeries(new List<Surgerie>());
        List<Surgerie> lb = new List<Surgerie>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        Surgerie.SetSurgeries(lb);
        
        Assert.That(Surgerie.GetSurgeries(), Is.EqualTo(lb));
        
        Surgerie.SetSurgeries(new List<Surgerie>());
    }

    [Test]
    public void Trying_to_create_same_Bill_throws_InvalidOperationException()
    {
        Surgerie b = new Surgerie("Test");
    try {
            Surgerie b2 = new Surgerie("Test");
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Surgerie_InvalidOperationException_excepted()
    {   
        try
        {
            Surgerie.RemoveSurgery(new Surgerie());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Surgeries_and_save_them_to_file()
    {
        Surgerie.SetSurgeries(new List<Surgerie>());
        List<Surgerie> la = new List<Surgerie>{new ( "Test1"), new ( "Test2"), new ( "Test3")};
        
        SerializeToFIle.saveAll();
        
        Surgerie.SetSurgeries(new List<Surgerie>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Surgerie.GetSurgeries(), Is.EqualTo(la));
    }
}
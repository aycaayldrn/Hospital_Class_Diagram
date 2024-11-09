namespace Tests;
using Hospital_System.Models;

public class ServiceTests
{
    [Test]
    public void Trying_to_create_Service_with_null_serviceName_should_throw_ArgumentNullException()
    {
        try
        {
            Service s = new Service(null, 100d);
            
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Service_with_negative_price_should_throw_ArgumentNullException()
    {
        try
        {
            Service s = new Service("test1", -100d);
            
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }        
    }
    
    [Test]
    public void Trying_to_create_Service_with_specific_Price_and_check_if_it_assigned_correctly()
    {
        int price = 12;
        Service s = new Service("test2", price);

        Assert.That(s.Price, Is.EqualTo(price));
        Service.RemoveService(s);
    }
    
    [Test]
    public void Trying_to_create_Service_with_specific_Name_and_check_if_it_assigned_correctly()
    {
        String name = "12321421";
        Service s = new Service(name, 123);

        Assert.That(s.Name, Is.EqualTo(name));
        Service.RemoveService(s);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Services_and_SetAppointments()
    {
        Service.SetServices(new List<Service>());
        List<Service> ls = new List<Service>{new ( "Test2", 100d), new ( "Test3", 100d), new ("Test4", 100d)};
        
        Service.SetServices(ls);
        
        Assert.That(Service.GetServices(), Is.EqualTo(ls));
        
        Service.SetServices(new List<Service>());
    }
    
    [Test]
    public void Trying_to_create_same_Service_throws_InvalidOperationException()
    {
        Service b = new Service("Test", 100d);
        try
        {
            Service b2 = new Service("Test", 100d);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Service_InvalidOperationException_excepted()
    {
        try
        {
            Service.RemoveService(new Service());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Services_and_save_them_to_file()
    {
        Service.SetServices(new List<Service>());
        List<Service> la = new List<Service>{new ( "Test2", 100d), new ( "Test3", 100d), new ("Test4", 100d)};
        
        SerializeToFIle.saveAll();
        
        Service.SetServices(new List<Service>());
        
        SerializeToFIle.loadAll();
        
        Assert.That(Service.GetServices(), Is.EqualTo(la));
    }
}
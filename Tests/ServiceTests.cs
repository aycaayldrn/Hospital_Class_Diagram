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
            
            Assert.Fail("Expected ArgumentNullException");
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
            Service s = new Service("test", -100d);
            
            Assert.Fail("Expected ArgumentNullException");
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
        Service s = new Service("test", price);

        Assert.That(s.Price, Is.EqualTo(price));
    }
    
    [Test]
    public void Trying_to_create_Service_with_specific_Name_and_check_if_it_assigned_correctly()
    {
        String name = "12321421";
        Service s = new Service(name, 123);

        Assert.That(s.Name, Is.EqualTo(name));
    }
}
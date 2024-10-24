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
}
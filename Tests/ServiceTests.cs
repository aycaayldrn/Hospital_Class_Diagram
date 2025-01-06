namespace Tests;
using Hospital_System.Models;

public class ServiceTests
{
    [Test]
    public void Trying_to_create_Service_with_null_serviceName_should_throw_ArgumentNullException()
    {
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }

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
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }

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
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }
        
        int price = 12;
        Service s = new Service("test2", price);

        Assert.That(s.Price, Is.EqualTo(price));
        Service.RemoveService(s);
    }
    
    [Test]
    public void Trying_to_create_Service_with_specific_Name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }

        String name = "12321421";
        Service s = new Service(name, 123);

        Assert.That(s.Name, Is.EqualTo(name));
        Service.RemoveService(s);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Services_and_SetAppointments()
    {
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }
        
        List<Service> ls = new List<Service>{new ( "Test2", 100d), new ( "Test3", 100d), new ("Test4", 100d)};
        
        
        Assert.That(Service.GetServices(), Is.EqualTo(ls));
    }
    
    [Test]
    public void Trying_to_create_same_Service_throws_InvalidOperationException()
    {
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }

        Service b = new Service("Test", 100d);
        try
        {
            Service b2 = new Service("Test", 100d);
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Service.RemoveService(b);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Service_InvalidOperationException_excepted()
    {
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }

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
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }
        
        List<Service> la = new List<Service>{new ( "Test2", 100d), new ( "Test3", 100d), new ("Test4", 100d)};
        
        SerializeToFIle.saveAll();
        
        foreach (Service o in la)
        {
            Service.RemoveService(o);
        }
        
        SerializeToFIle.loadAll();
        foreach (var o in Service.GetServices())
        {
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
    
        [Test]
    public void Trying_to_add_Provider_to_Service_and_then_delete_it()
    { 
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        service.assignInsuranceProviderToService(provider);
        if (provider.Services.Contains(service)&&service.Insurance_Providers.Contains(provider)){
            service.removeInsuranceProviderFromService(provider);
            if (provider.Services.Contains(service)||service.Insurance_Providers.Contains(provider))
            {
                Assert.Fail();
            }
            Service.RemoveService(service);
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Provider_to_Service()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        service.assignInsuranceProviderToService(provider);
        if (provider.Services.Contains(service)&&service.Insurance_Providers.Contains(provider)){
            Service.RemoveService(service);
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Providers_to_Service()
    {
        Service service = new Service("Test", 100d);
        List<Insurance_Provider> providers = new List<Insurance_Provider>{new (24,"Test1", new List<Service>(){new Service()}),
            new (25,"Test1", new List<Service>(){new Service()}),
            new (26,"Test1", new List<Service>(){new Service()})};
        foreach (var e in providers)
        {
            service.assignInsuranceProviderToService(e);
        }

        foreach (var e in service.Insurance_Providers)
        {
            if (providers.Contains(e))
            {

            }
            else
            {
                Assert.Fail();
            }
        }
        Service.RemoveService(service);
        foreach (var e in providers)
        {
            Insurance_Provider.removeProvider(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Provider_to_Service_throws_ArgumentException()
    {
        Service service = new Service("Test", 100d);
        try
        {
            service.assignInsuranceProviderToService(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Service.RemoveService(service);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Provider_to_Service_and_then_try_to_add_same_Provider_throws_InvalidOperationException()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        service.assignInsuranceProviderToService(provider);
        try
        {
            service.assignInsuranceProviderToService(provider);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Service.RemoveService(service);
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Provider_that_not_exist_in_list_from_Service_should_throw_InvalidOperationException()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        try
        {
            service.removeInsuranceProviderFromService(provider);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Service.RemoveService(service);
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
            [Test]
    public void Trying_to_add_Bill_to_Service_and_then_delete_it()
    { 
        Bill bill = new Bill(21, 3213, new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        service.assignBillToService(bill);
        if (bill.Services.Contains(service)&&service.Bills.Contains(bill)){
            service.RemoveBillFromService(bill);
            if (bill.Services.Contains(service)||service.Bills.Contains(bill))
            {
                Assert.Fail();
            }
            Service.RemoveService(service);
            Bill.removeBill(bill);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Bill_to_Service()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        service.assignInsuranceProviderToService(provider);
        if (provider.Services.Contains(service)&&service.Insurance_Providers.Contains(provider)){
            Service.RemoveService(service);
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Bills_to_Service()
    {
        Service service = new Service("Test", 100d);
        List<Bill> bills = new List<Bill>{new (21, 3213, new List<Service>(){new Service()}),
            new (22, 3213, new List<Service>(){new Service()}),
            new (23, 3213, new List<Service>(){new Service()})};
        foreach (var e in bills)
        {
            service.assignBillToService(e);
        }

        foreach (var e in service.Bills)
        {
            if (bills.Contains(e))
            {

            }
            else
            {
                Assert.Fail();
            }
        }
        Service.RemoveService(service);
        foreach (var e in bills)
        {
            Bill.removeBill(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Bill_to_Service_throws_ArgumentException()
    {
        Service service = new Service("Test", 100d);
        try
        {
            service.assignBillToService(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Service.RemoveService(service);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Bill_to_Service_and_then_try_to_add_same_Bill_throws_InvalidOperationException()
    {
        Bill bill = new Bill(21, 3213, new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        service.assignBillToService(bill);
        try
        {
            service.assignBillToService(bill);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Service.RemoveService(service);
            Bill.removeBill(bill);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Bill_that_not_exist_in_list_from_Service_should_throw_InvalidOperationException()
    {
        Bill bill = new Bill(21, 3213, new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        try
        {
            service.RemoveBillFromService(bill);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Service.RemoveService(service);
            Bill.removeBill(bill);
            Assert.Pass();
        }
        Assert.Fail();
    }
}
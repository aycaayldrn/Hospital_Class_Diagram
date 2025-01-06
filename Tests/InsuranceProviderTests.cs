namespace Tests;
using Hospital_System.Models;

public class InsuranceProviderTests
{
    [Test]
    public void Trying_to_create_Insurance_Provider()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }

        Insurance_Provider e = new Insurance_Provider(1,"Test2", new List<Service>(){new Service()});
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_create_Insurance_Provider_with_null_Service_throws_ArgumentException()
    {
        try
        {
            Insurance_Provider e = new Insurance_Provider(1, "Test2", null);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_assign_null_val_to_name_should_throw_ArgumentNullException()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }

        try
        {
            Insurance_Provider e = new Insurance_Provider(1,null, new List<Service>());
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_assign_negative_val_to_id_should_throw_ArgumentNullException()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }

        try
        {
            Insurance_Provider e = new Insurance_Provider(-1,"Test1", new List<Service>());
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_create_Insurance_Provider_with_specific_name_and_check_if_it_assigned_correctly()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }

        String name = "Test";
        Insurance_Provider e = new Insurance_Provider(1,name, new List<Service>(){new Service()});
        Assert.That(e.Name, Is.EqualTo(name));
        Insurance_Provider.removeProvider(e);
    }
    
     
    [Test]
    public void Trying_to_create_List_of_Insurance_Providers_and_SetAppointments()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }
        
        List<Insurance_Provider> li = new List<Insurance_Provider>{new ( 24,"Test1", new List<Service>(){new Service()}), new ( 22,"Test2", new List<Service>(){new Service()}), new ( 44,"Test3", new List<Service>(){new Service()})};
        
        Assert.That(Insurance_Provider.GetProvider(), Is.EqualTo(li));
    }
    
    [Test]
    public void Trying_to_create_same_Insurance_Provider_throws_InvalidOperationException()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }

        Insurance_Provider i = new Insurance_Provider(21,"Test", new List<Service>(){new Service()});
        try
        {
            Insurance_Provider i2 = new Insurance_Provider(21,"Test", new List<Service>(){new Service()});
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Insurance_Provider.removeProvider(i);
            Assert.Pass();
        }
    }
    
    [Test]
    public void Trying_to_remove_nonExisting_Insurance_Provider_InvalidOperationException_excepted()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }

        try
        {
            Insurance_Provider.removeProvider(new Insurance_Provider());
            Assert.Fail("Should throw InvalidOperationException");
        }catch(InvalidOperationException o)
        {
            Assert.Pass();
        }
    }
    
        
    [Test]
    public void Trying_to_create_List_of_Insurance_Providers_and_save_them_to_file()
    {
        foreach (var o in Insurance_Provider.GetProvider().ToList())
        {
            Insurance_Provider.removeProvider(o);
        }
        
        List<Insurance_Provider> la = new List<Insurance_Provider>{new ( 24,"Test1", new List<Service>(){new Service()}), new ( 22,"Test2", new List<Service>(){new Service()}), new ( 44,"Test3", new List<Service>(){new Service()})};
        
        SerializeToFIle.saveAll();
        
        foreach (Insurance_Provider o in la)
        {
            Insurance_Provider.removeProvider(o);
        }
        
        SerializeToFIle.loadAll();
        foreach (var o in Insurance_Provider.GetProvider())
        {
            o.AddServiceToProvide(new Service());
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_Service_to_Provider_and_then_delete_it()
    { 
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        provider.AddServiceToProvide(service);
        if (provider.Services.Contains(service)&&service.Insurance_Providers.Contains(provider)){
            provider.RemoveServiceFromProvider(service);
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
    public void Trying_to_add_Service_to_Provider()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        provider.AddServiceToProvide(service);
        if (provider.Services.Contains(service)&&service.Insurance_Providers.Contains(provider)){
            Service.RemoveService(service);
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Services_to_Provider()
    {
        foreach (var o in Service.GetServices().ToList())
        {
            Service.RemoveService(o);
        }
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        List<Service> services = new List<Service>{new ("Test", 100d),
            new ("Test1", 100d),
            new ("Test2", 100d)};
        foreach (var e in services)
        {
            provider.AddServiceToProvide(e);
        }
        services.Add(new Service());

        foreach (var e in provider.Services)
        {
            if (services.Contains(e))
            {

            }
            else
            {
                Assert.Fail();
            }
        }
        services.Remove(new Service());
        Insurance_Provider.removeProvider(provider);
        foreach (var e in services)
        {
            Service.RemoveService(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Service_to_Provider_throws_ArgumentException()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        try
        {
            provider.AddServiceToProvide(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Insurance_Provider.removeProvider(provider);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Service_to_Provider_and_then_try_to_add_same_Service_throws_InvalidOperationException()
    {
        Insurance_Provider provider = new Insurance_Provider(24,"Test1", new List<Service>(){new Service()});
        Service service = new Service("Test", 100d);
        provider.AddServiceToProvide(service);
        try
        {
            provider.AddServiceToProvide(service);
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
}
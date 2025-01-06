using InvalidOperationException = System.InvalidOperationException;

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
        foreach (var o in Physician.GetPhysicians())
        {
            if (!la.Contains(o))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription_and_then_delete_it()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        prescription.assignPrescriptionToPhysycian(physician);
        foreach (var e in physician.GetPrescriptions())
        {
            if (e.Equals(prescription))
            {
                prescription.deletePrescriptionByPhyscian();
                foreach (var e2 in physician.GetPrescriptions())
                {
                    if (e2.Equals(prescription))
                    {
                        Assert.Fail();
                    }
                }
                Prescription.RemovePrescription(prescription);
                Physician.RemovePhysician(physician);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        prescription.assignPrescriptionToPhysycian(physician);
        foreach (var e in physician.GetPrescriptions())
        {
            if (e.Equals(prescription) && prescription.Physician.Equals(physician))
            {
                Physician.RemovePhysician(physician);
                Prescription.RemovePrescription(prescription);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_many_Prescriptions_to_Physician()
    {
        Physician physician = new Physician(1,"Test1","Test1");
        List<Prescription> prescriptions = new List<Prescription>{new (1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()}),
            new (2, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()})};
        foreach (var e in prescriptions)
        {
            e.assignPrescriptionToPhysycian(physician);
        }
    
        foreach (var e in physician.GetPrescriptions())
        {
            if (prescriptions.Contains(e))
            {
                
            }
            else
            {
                Assert.Fail();
            }
        }
        Physician.RemovePhysician(physician);
        foreach (var e in prescriptions)
        {
            Prescription.RemovePrescription(e);
        }
        Assert.Pass();
    }
    
    [Test]
    public void Trying_to_add_null_Prescription_to_Physician_throws_ArgumentNullException()
    {
        Physician physician = new Physician(1,"Test1","Test1");
        try
        {
            physician.addPrescriptiont(null);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException argumentException)
        {
            Physician.RemovePhysician(physician);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription_that_already_has_Physician_throws_InvalidOperationException()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        Physician physician2 = new Physician(2,"Test2","Test2");
        prescription.assignPrescriptionToPhysycian(physician);
        try
        {
            prescription.assignPrescriptionToPhysycian(physician2);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException argumentException)
        {
            Physician.RemovePhysician(physician);
            Physician.RemovePhysician(physician2);
            Prescription.RemovePrescription(prescription);
            Assert.Pass();   
        }
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription_and_then_change_it()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        Physician physician2 = new Physician(2,"Test2","Test2");
        prescription.assignPrescriptionToPhysycian(physician);
        if (prescription.Physician.Equals(physician))
        {
            prescription.changePhysician(physician2);
            if (prescription.Physician.Equals(physician2))
            {
                Physician.RemovePhysician(physician);
                Physician.RemovePhysician(physician2);
                Prescription.RemovePrescription(prescription);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription_and_then_change_it_to_the_same_physician_throws_InvalidOperationException()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        prescription.assignPrescriptionToPhysycian(physician);
        if (prescription.Physician.Equals(physician))
        {
            try
            {
                prescription.changePhysician(physician);
                Assert.Fail("Expected InvalidOperationException");
            }
            catch (InvalidOperationException)
            {
                Physician.RemovePhysician(physician);
                Prescription.RemovePrescription(prescription);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription_and_then_change_it_to_the_null_physician_throws_InvalidOperationException()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        prescription.assignPrescriptionToPhysycian(physician);
        if (prescription.Physician.Equals(physician))
        {
            try
            {
                prescription.changePhysician(null);
                Assert.Fail("Expected InvalidOperationException");
            }
            catch (ArgumentException)
            {
                Physician.RemovePhysician(physician);
                Prescription.RemovePrescription(prescription);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_change_prescrition_physician_without_assigning_physician_throws_InvalidOperationException()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        try
        {
            prescription.changePhysician(physician);
            Physician.RemovePhysician(physician);
            Prescription.RemovePrescription(prescription);
            Assert.Fail("Expected InvalidOperationException"); 
        }catch (InvalidOperationException) 
        {
            Physician.RemovePhysician(physician);
            Prescription.RemovePrescription(prescription);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Prescription_to_Physician_and_then_try_to_add_same_Prescription_throws_InvalidOperationException()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        prescription.assignPrescriptionToPhysycian(physician);
        try
        {
            physician.addPrescriptiont(prescription);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Physician.RemovePhysician(physician);
            Prescription.RemovePrescription(prescription);
            Assert.Pass();
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_add_Physician_to_Prescription_and_then_remove_it()
    {
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        Physician physician = new Physician(1,"Test1","Test1");
        prescription.assignPrescriptionToPhysycian(physician);
        foreach (var e in physician.GetPrescriptions())
        {
            if (e.Equals(prescription) && prescription.Physician.Equals(physician))
            {
                physician.removePrescriptiont(prescription);
                if (physician.GetPrescriptions().Contains(prescription) && prescription.Physician.Equals(null))
                {
                    Assert.Fail();
                }
                Physician.RemovePhysician(physician);
                Prescription.RemovePrescription(prescription);
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    
    [Test]
    public void Trying_to_remove_Prescription_that_not_exist_in_prescriptionslist_from_Physician_should_throw_InvalidOperationException()
    {
        Physician physician = new Physician(1,"Test1","Test1");
        Prescription prescription = new Prescription(1, "Test1", 1.2f, 4, false, new List<Bill>(){new Bill()});
        try
        {
            physician.removePrescriptiont(prescription);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException)
        {
            Physician.RemovePhysician(physician);
            Prescription.RemovePrescription(prescription);
            Assert.Pass();
        }
        Assert.Fail();
    }
}